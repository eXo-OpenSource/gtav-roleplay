using System;
using System.Collections.Generic;

namespace server.Models.Enums
{
    [Flags]
    public enum TeamPermissions : ulong
    {
        None = 0,

        // Rank
        HirePlayer = 1 << 0,
        PromotePlayer = HirePlayer << 1,
        DemotePlayer = PromotePlayer << 1,
        DismissPlayer = DemotePlayer << 1,

        //vehicle
        EnterVehicle = DismissPlayer << 1,
        DriveVehicle = EnterVehicle << 1,

        //Money
        GiveMoney = DriveVehicle << 1,
        TakeMoney = GiveMoney << 1,

        // Departments
        PromoteDepartment = TakeMoney << 1,
        DemoteDepartment = PromoteDepartment << 1,
        CreateDepartment = DemoteDepartment << 1,
        DeleteDepartment = CreateDepartment << 1,

        


        All = HirePlayer | PromotePlayer | DemotePlayer | 
                    DismissPlayer | EnterVehicle | DriveVehicle | 
                    GiveMoney | TakeMoney | PromoteDepartment | 
                    DemoteDepartment | CreateDepartment | DeleteDepartment,
    }

    public static class TeamPermissionsExtensions
    {
        public static TeamPermissions Set(this TeamPermissions states, TeamPermissions state)
        {
            return states | state;
        }

        public static TeamPermissions UnSet(this TeamPermissions states, TeamPermissions state)
        {
            if ((int)states == 0)
                return states;

            if (states == state)
                return TeamPermissions.None;

            return states & ~state;
        }

        public static IEnumerable<TeamPermissions> GetFlags(this TeamPermissions input)
        {
            foreach (TeamPermissions value in Enum.GetValues(typeof(TeamPermissions)))
                if (input.HasFlag(value))
                    yield return value;
        }
    }
}