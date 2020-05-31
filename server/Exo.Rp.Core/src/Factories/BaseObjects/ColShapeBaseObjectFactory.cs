using System;
using AltV.Net;
using AltV.Net.Elements.Entities;

namespace server.Factories.BaseObjects
{
	public class ColShapeBaseObjectFactory : IBaseObjectFactory<IColShape>
	{
		public IColShape Create(IntPtr baseObjectPointer)
		{
			return new Colshape.Colshape(baseObjectPointer);
		}
	}
}
