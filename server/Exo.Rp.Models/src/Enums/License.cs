using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exo.Rp.Models.Enums
{
    public enum License
    {
        Greencard = 1,
        Citizenship = 2,
        Car = 3,
        Motorcycle = 4,
        Truck = 5,
        Boat = 6,
        PlaneA = 7,
        PlaneB = 8,
        Add = 1,
        Remove = 0
    }

    public enum LicensePrice
    {
        Greencard = 0,
        Citizenship = 0,
        Car = 5000,
        Motorcycle = 2500,
        Truck = 5000,
        Boat = 3000,
        PlaneA = 75000,
        PlaneB = 150000
    }
}
