using System;

namespace Ex03.GarageLogic
{
    public class ExceptionVehicleAlreadyInSystem : Exception
    {
        public ExceptionVehicleAlreadyInSystem() : base("Vehicle aleady in system.")
        {
        }
    }
}
