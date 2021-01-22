using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AltV.Net.Data;

namespace Exo.Rp.Core.Environment.CarRentTypes
{
    public class RentVehiclePosition
    {
        public Position Pos { get; }
        public Rotation Rot { get; }
        public string Name { get; }
        public RentVehiclePosition(string vehicleName, Position pos, Rotation rot)
        {
            Name = vehicleName;
            Pos = pos;
            Rot = rot;
        }
    }
}
