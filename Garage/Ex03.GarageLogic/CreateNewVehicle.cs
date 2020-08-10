using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public enum eTypeOfVehicles
    {
        GasBike,
        ElectricBike,
        GasCar,
        ElectricCar,
        Truck,
    }

    public class CreateNewVehicle
    {
        private const float k_MaxBikeGasTankSize = 7;
        private const float k_MaxBikeElectricBatterySize = 1.2f;
        private const float k_MaxCarGasTankSize = 60;
        private const float k_MaxCarElectricBatterySize = 2.1f;
        private const float k_MaxTruckGasTankSize = 120;
        private const eEngineType k_Octan95 = eEngineType.Octan95;
        private const eEngineType k_Octan96 = eEngineType.Octan96;
        private const eEngineType k_Soler = eEngineType.Soler;
        private const eEngineType k_Electric = eEngineType.Electric;

        public static Vehicle CreateVehicle(int i_TypeOfVehicles, string i_LicenseNumber)
        {        
            Vehicle vehicleToCreate;
            Power engine;
            switch (i_TypeOfVehicles)
            {
                case 1:
                    engine = new Power(k_MaxBikeGasTankSize, k_Octan95);
                    vehicleToCreate = new Bike(i_LicenseNumber, engine);
                    break;
                case 2:
                    engine = new Power(k_MaxBikeElectricBatterySize, k_Electric);
                    vehicleToCreate = new Bike(i_LicenseNumber, engine);
                    break;
                case 3:
                    engine = new Power(k_MaxCarGasTankSize, k_Octan96);
                    vehicleToCreate = new Car(i_LicenseNumber, engine);
                    break;
                case 4:
                    engine = new Power(k_MaxCarElectricBatterySize, k_Electric);
                    vehicleToCreate = new Car(i_LicenseNumber, engine);
                    break;
                case 5:
                    engine = new Power(k_MaxTruckGasTankSize, k_Soler);
                    vehicleToCreate = new Truck(i_LicenseNumber, engine);
                    break;
                default:
                    throw new ValueOutOfRangeException(1, Enum.GetNames(typeof(eTypeOfVehicles)).Length);
            }

            return vehicleToCreate;
        }
    }
}
