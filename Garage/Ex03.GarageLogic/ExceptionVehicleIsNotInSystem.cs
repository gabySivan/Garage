using System;

namespace Ex03.GarageLogic
{
    public class ExceptionVehicleIsNotInSystem : Exception
    {
        public ExceptionVehicleIsNotInSystem() : base("Vehicle is not in system.")
        {
        }
    }
}
