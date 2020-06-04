using System;
using System.Collections.Generic;
using System.Numerics;
using AltV.Net.EntitySync;
using AltV.Net.EntitySync.Events;
using server.Streamer.Entities;
using server.Streamer.Grid;

namespace server.Streamer.Private
{
	public class PrivateStreamer : IManager
	{

		private PrivateEntityThread[] entityThreads;

		private PrivateEntityThreadRepository[] entityThreadRepositories;

		private PrivateClientThreadRepository[] clientThreadRepositories;

		private LimitedPrivateGrid3[] spatialPartitions;

		private IEntityRepository entityRepository;

		private IClientRepository clientRepository;

		private NetworkLayer networkLayer;

		internal IIdProvider<ulong> idProvider;

		internal LinkedList<EntityCreateDelegate> EntityCreateCallbacks = new LinkedList<EntityCreateDelegate>();

		internal LinkedList<EntityRemoveDelegate> EntityRemoveCallbacks = new LinkedList<EntityRemoveDelegate>();

		public PrivateStreamer()
		{

		}

		public void Init(ulong threadCount, int syncRate,
			Func<ulong, IClientRepository, NetworkLayer> createNetworkLayer,
			Func<IEntity, ulong, ulong> entityThreadId,
			Func<ulong, ulong, ulong, ulong> entityIdAndTypeThreadId,
			Func<ulong, LimitedPrivateGrid3> createSpatialPartition, IIdProvider<ulong> idProvider)
		{
			if (threadCount < 1)
			{
				throw new ArgumentException("threadCount must be >= 1");
			}

			if (syncRate < 0)
			{
				throw new ArgumentException("syncRate must be >= 0");
			}

			entityThreads = new PrivateEntityThread[threadCount];
			entityThreadRepositories = new PrivateEntityThreadRepository[threadCount];
			clientThreadRepositories = new PrivateClientThreadRepository[threadCount];
			spatialPartitions = new LimitedPrivateGrid3[threadCount];

			for (ulong i = 0; i < threadCount; i++)
			{
				var clientThreadRepository = new PrivateClientThreadRepository();
				var entityThreadRepository = new PrivateEntityThreadRepository();
				var spatialPartition = createSpatialPartition(i);
				entityThreadRepositories[i] = entityThreadRepository;
				clientThreadRepositories[i] = clientThreadRepository;
				spatialPartitions[i] = spatialPartition;
				entityThreads[i] = new PrivateEntityThread(i, entityThreadRepository, clientThreadRepository, spatialPartition, syncRate,
					OnEntityCreate,
					OnEntityRemove, OnEntityDataChange, OnEntityPositionChange, OnEntityClearCache);
			}

			entityRepository = new PrivateEntityRepository(entityThreadRepositories, entityThreadId, entityIdAndTypeThreadId);
			clientRepository = new PrivateClientRepository(clientThreadRepositories);
			networkLayer = createNetworkLayer(threadCount, clientRepository);
			this.idProvider = idProvider;
		}

		 private void OnEntityCreate(IClient client, IEntity entity, LinkedList<string> changedKeys)
        {
            Dictionary<string, object> data;
            if (changedKeys != null) {
                data = new Dictionary<string, object>();
                var changedKey = changedKeys.First;
                while (changedKey != null)
                {
                    var key = changedKey.Value;
                    if (entity.TryGetThreadLocalData(key, out var value))
                    {
                        data[key] = value;
                    }
                    else
                    {
                        data[key] = null;
                    }
                    changedKey = changedKey.Next;
                }
            }
            else
            {
                data = null;
            }
            networkLayer.SendEvent(client, new EntityCreateEvent(entity, data));

            var callback = EntityCreateCallbacks.First;
            while (callback != null)
            {
                callback.Value(client, entity);
                callback = callback.Next;
            }
        }

        private void OnEntityRemove(IClient client, IEntity entity)
        {
            networkLayer.SendEvent(client, new EntityRemoveEvent(entity));
            var callback = EntityRemoveCallbacks.First;
            while (callback != null)
            {
                callback.Value(client, entity);
                callback = callback.Next;
            }
        }

        private void OnEntityDataChange(IClient client, IEntity entity, LinkedList<string> changedKeys)
        {
            Dictionary<string, object> data;
            if (changedKeys != null) {
                data = new Dictionary<string, object>();
                var changedKey = changedKeys.First;
                while (changedKey != null)
                {
                    var key = changedKey.Value;
                    if (entity.TryGetThreadLocalData(key, out var value))
                    {
                        data[key] = value;
                    }
                    else
                    {
                        data[key] = null;
                    }
                    changedKey = changedKey.Next;
                }
            }
            else
            {
                data = null;
            }
            networkLayer.SendEvent(client, new EntityDataChangeEvent(entity, data));
        }

        private void OnEntityPositionChange(IClient client, IEntity entity, Vector3 newPosition)
        {
            networkLayer.SendEvent(client, new EntityPositionUpdateEvent(entity, newPosition));
        }

        private void OnEntityClearCache(IClient client, IEntity entity)
        {
            networkLayer.SendEvent(client, new EntityClearCacheEvent(entity));
        }

        public void AddEntity(PrivateEntity entity)
        {
            entityRepository.Add(entity);
        }

        public void RemoveEntity(PrivateEntity entity)
        {
            entityRepository.Remove(entity);
            idProvider?.Free(entity.Id);
        }

        public void UpdateEntity(PrivateEntity entity)
        {
            entityRepository.Update(entity);
        }

        public void UpdateEntityData(PrivateEntity entity, string key, object value)
        {
            entityRepository.UpdateData(entity, key, value);
        }

        public void ResetEntityData(PrivateEntity entity, string key)
        {
            entityRepository.ResetData(entity, key);
        }

        public bool TryGetEntity(ulong id, ulong type, out IEntity entity)
        {
            return entityRepository.TryGet(id, type, out entity);
        }

        public IEnumerable<IEntity> GetAllEntities()
        {
            return entityRepository.GetAll();
        }

        public List<IEntity> FindEntities(Vector3 position, int dimension)
        {
            var foundEntities = new List<IEntity>();
            for (int i = 0, length = entityThreads.Length; i < length; i++)
            {
                lock (clientThreadRepositories[i].Mutex)
                {
                    foundEntities.AddRange(spatialPartitions[i].Find(position, dimension));
                }
            }

            return foundEntities;
        }

        public void Stop()
        {
            foreach (var entityThread in entityThreads)
            {
                entityThread.Stop();
            }
        }
	}
}
