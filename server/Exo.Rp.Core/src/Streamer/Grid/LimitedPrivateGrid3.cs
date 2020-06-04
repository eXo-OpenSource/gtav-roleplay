using System;
using System.Collections.Generic;
using System.Numerics;
using AltV.Net.EntitySync;
using AltV.Net.EntitySync.SpatialPartitions;
using server.Streamer.Entities;

namespace server.Streamer.Grid
{
	public class LimitedPrivateGrid3 : Grid3
	{
		/// <summary>
		/// Comparer for comparing two keys, handling equality as beeing greater
		/// Use this Comparer e.g. with SortedLists or SortedDictionaries, that don't allow duplicate keys
		/// </summary>
		/// <typeparam name="TKey"></typeparam>
		private class DuplicateKeyComparer
			: IComparer<float>
		{
			public int Compare(float x, float y)
			{
				var result = x.CompareTo(y);

				return result == 0 ? 1 : result;
			}
		}

		private static readonly DuplicateKeyComparer _duplicateKeyComparer = new DuplicateKeyComparer();

		private readonly SortedLimitedList<float, IEntity> sortedList;

		public LimitedPrivateGrid3(int maxX, int maxY, int areaSize, int xOffset, int yOffset, int limit) : base(maxX, maxY, areaSize, xOffset, yOffset)
		{
			sortedList = new SortedLimitedList<float, IEntity>(limit, limit, _duplicateKeyComparer);
		}

		private bool CanSeeEntity(int entity, PrivateEntity otherEntity)
		{
			return otherEntity.IsVisible(entity);
		}

		public IList<IEntity> Find(Vector3 position, int dimension, int entityId)
		{
			var posX = position.X + xOffset;
			var posY = position.Y + yOffset;

			var xIndex = (int) Math.Floor(posX / areaSize);

			var yIndex = (int) Math.Floor(posY / areaSize);

			// x2 and y2 only required for complete exact range check

			/*var x2Index = (int) Math.Ceiling(posX / areaSize);
			var y2Index = (int) Math.Ceiling(posY / areaSize);*/

			if (xIndex < 0 || yIndex < 0 || xIndex >= maxXAreaIndex || yIndex >= maxYAreaIndex) return null;

			var areaEntities = entityAreas[xIndex][yIndex];

			sortedList.Clear();

			for (int j = 0, innerLength = areaEntities.Count; j < innerLength; j++)
			{
				var areaEntity = areaEntities[j];
				var entityClientDistance = Vector3.DistanceSquared(areaEntity.Position, position);
				var entity = areaEntities[j];
				if (entityClientDistance <= entity.RangeSquared &&
				    CanSeeOtherDimension(dimension, entity.Dimension) && CanSeeEntity(entityId, (PrivateEntity) entity))
				{
					sortedList.Add(entityClientDistance, areaEntity);
				}
			}

			/*var i = limit;

			using (var enumerator = sortedList.GetEnumerator())
			{
			    while (i-- != 0 && enumerator.MoveNext())
			    {
			        yield return enumerator.Current.Value;
			    }
			}*/

			return sortedList.Values;
		}
	}
}
