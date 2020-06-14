using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using AltV.Net.EntitySync;
using server.Streamer.Private;

namespace server.Streamer.Entities
{
	public class PrivateEntity : IEntity
	{

		private readonly HashSet<int> _visibleEntities;

		public ulong Id { get; }
        public ulong Type { get; }

        public (ulong, ulong) HashKey { get; }

        private Vector3 position;

        public Vector3 Position
        {
            get => position;
            set => SetPositionInternal(value);
        }

        private bool positionState = false;

        private Vector3 newPosition;

        private int dimension;

        public int Dimension
        {
            get => dimension;
            set => SetDimensionInternal(value);
        }

        private bool dimensionState = false;

        private int newDimension;

        private uint range;

        public uint Range
        {
            get => range;
            set => SetRangeInternal(value);
        }

        public uint RangeSquared { get; private set; }

        private bool rangeState = false;

        private uint newRange;

        private readonly object propertiesMutex = new object();

        private readonly IDictionary<string, object> data;

        private readonly IDictionary<string, object> threadLocalData;

        public IDictionary<string, object> Data => data;

        public IDictionary<string, object> ThreadLocalData => threadLocalData;

        public EntityDataSnapshot DataSnapshot { get; }

        /// <summary>
        /// List of clients that have the entity created.
        /// </summary>
        private readonly HashSet<IClient> clients = new HashSet<IClient>();

        public PrivateEntity(ulong type, Vector3 position, int dimension, uint range) : this(
            Core.GetService<PrivateStreamer>().idProvider.GetNext(), type,
            position, dimension, range, new Dictionary<string, object>())
        {
        }

        public PrivateEntity(ulong type, Vector3 position, int dimension, uint range, IDictionary<string, object> data) : this(
	        Core.GetService<PrivateStreamer>().idProvider.GetNext(), type,
            position, dimension, range, data)
        {
        }

        internal PrivateEntity(ulong id, ulong type, Vector3 position, int dimension, uint range,
            IDictionary<string, object> data)
        {
            Id = id;
            Type = type;
            HashKey = (id, type);
            this.position = position;
            this.dimension = dimension;
            this.range = range;
            RangeSquared = range * range;
            this.data = data;
            DataSnapshot = new EntityDataSnapshot(this);
            threadLocalData = new Dictionary<string, object>(data);
            _visibleEntities = new HashSet<int>();
        }

        public void SetData(string key, object value)
        {
            lock (data)
            {
                data[key] = value;
            }
            Core.GetService<PrivateStreamer>().UpdateEntityData(this, key, value);
        }

        public void ResetData(string key)
        {
            lock (data)
            {
                data.Remove(key);
            }
            Core.GetService<PrivateStreamer>().ResetEntityData(this, key);
        }

        public bool TryGetData(string key, out object value)
        {
            lock (data)
            {
                return data.TryGetValue(key, out value);
            }
        }

        public bool TryGetData<T>(string key, out T value)
        {
            lock (data)
            {
                if (!data.TryGetValue(key, out var currValue))
                {
                    value = default;
                    return false;
                }

                if (!(currValue is T correctValue))
                {
                    value = default;
                    return false;
                }

                value = correctValue;
                return true;
            }
        }

        /// <summary>
        /// Tries to add a client to the list of clients that created this entity.
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public bool TryAddClient(IClient client)
        {
            return clients.Add(client);
        }

        public bool RemoveClient(IClient client)
        {
            return clients.Remove(client);
        }

        public HashSet<IClient> GetClients()
        {
            return clients;
        }

        public void SetPositionInternal(Vector3 currNewPosition)
        {
            lock (propertiesMutex)
            {
                positionState = true;
                newPosition = currNewPosition;
            }
            Core.GetService<PrivateStreamer>().UpdateEntity(this);
        }

        public void SetDimensionInternal(int currNewDimension)
        {
            lock (propertiesMutex)
            {
                dimensionState = true;
                newDimension = currNewDimension;
            }
            Core.GetService<PrivateStreamer>().UpdateEntity(this);
        }

        public void SetRangeInternal(uint currNewRange)
        {
            lock (propertiesMutex)
            {
                rangeState = true;
                newRange = currNewRange;
            }
            Core.GetService<PrivateStreamer>().UpdateEntity(this);
        }

        public (bool, bool, bool) TrySetPropertiesComputing(out Vector3 currNewPosition, out uint currNewRange, out int currNewDimension)
        {
            lock (propertiesMutex)
            {
                var newPositionFound = positionState;
                var newRangeFound = rangeState;
                var newDimensionFound = dimensionState;

                if (!positionState)
                {
                    currNewPosition = default;
                }
                else
                {
                    currNewPosition = newPosition;
                    positionState = false;
                    position = newPosition;
                }

                if (!rangeState)
                {
                    currNewRange = default;
                }
                else
                {
                    currNewRange = newRange;
                    rangeState = false;
                    range = newRange;
                    RangeSquared = range * range;
                }

                if (!dimensionState)
                {
                    currNewDimension = default;
                }
                else
                {
                    currNewDimension = newDimension;
                    dimensionState = false;
                    dimension = newDimension;
                }


                return ValueTuple.Create(newPositionFound, newRangeFound, newDimensionFound);
            }
        }

        public void SetThreadLocalData(string key, object value)
        {
            threadLocalData[key] = value;
        }

        public void ResetThreadLocalData(string key)
        {
            threadLocalData.Remove(key);
        }

        public bool TryGetThreadLocalData(string key, out object value)
        {
            return threadLocalData.TryGetValue(key, out value);
        }

        public virtual byte[] Serialize(IEnumerable<string> changedKeys)
        {
            using var m = new MemoryStream();
            using (var writer = new BinaryWriter(m))
            {
                writer.Write(Id);
                writer.Write(Type);
                writer.Write(position.X);
                writer.Write(position.Y);
                writer.Write(position.Z);
                writer.Write(Range);
                //TODO: serialize data depending on changedKeys
            }

            return m.ToArray();
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override bool Equals(object? obj)
        {
            if (!(obj is PrivateEntity entity)) return false;
            if (entity.Id != Id) return false;
            if (entity.Type != Type) return false;
            if (entity.dimension != dimension) return false;
            if (entity.range != range) return false;
            if (entity.position != position) return false;
            if (entity.data != data) return false;
            return true;
        }

        public bool IsVisible(int entityId)
		{
			return _visibleEntities.Contains(entityId);
		}

		public void AddVisibleEntity(int entityId)
		{
			_visibleEntities.Add(entityId);
		}

		public void RemoveVisibleEntity(int entityId)
		{
			_visibleEntities.Remove(entityId);
		}
	}
}
