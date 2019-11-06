using System;
using AltV.Net;
using AltV.Net.Elements.Entities;

namespace server.Factories.Entity
{
    public class VehicleEntityFactory : IEntityFactory<IVehicle>
    {
        public IVehicle Create(IntPtr entityPointer, ushort id)
        {
            throw new NotImplementedException();
        }
    }
}