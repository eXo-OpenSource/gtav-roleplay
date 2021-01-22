using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exo.Rp.Core.Environment.CarRentTypes;
using AltV.Net.Data;

namespace Exo.Rp.Core.Environment.CarRentTypes
{
    public partial class BikeRental : RentalGroup
    {
        public void LoadPositions()
        {
            vehiclePositions = new List<RentVehiclePosition>
            {

            };
        }
    }
}
