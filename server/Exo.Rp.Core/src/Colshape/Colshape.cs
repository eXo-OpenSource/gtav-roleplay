using System;
using AltV.Net.Elements.Entities;

namespace server.Colshape
{
	public class Colshape : AltV.Net.Elements.Entities.ColShape
	{

		public delegate void ColShapeEnter(IEntity entity);
		public delegate void ColShapeExit(IEntity entity);

		public event ColShapeEnter OnColShapeEnter;
		public event ColShapeExit OnColShapeExit;

		public Colshape(IntPtr nativePointer) : base(nativePointer)
		{
		}

		public void TriggerEnter(IEntity entity)
		{
			OnColShapeEnter?.Invoke(entity);
		}

		public void TriggerExit(IEntity entity)
		{
			OnColShapeExit?.Invoke(entity);
		}
	}
}
