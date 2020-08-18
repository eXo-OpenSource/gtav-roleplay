using System;
using AltV.Net;
using AltV.Net.Elements.Entities;

namespace Exo.Rp.Core.Factories.Entities
{
    public class VehicleEntityFactory : IEntityFactory<IVehicle>
    {
        public IVehicle Create(IntPtr entityPointer, ushort id)
        {
            return new Vehicle(entityPointer, id);
        }
    }
}