using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private Dictionary<int, Vehicle> m_VehicleCollection;

        public Garage()
        {
            m_VehicleCollection = new Dictionary<int, Vehicle>();         
        }

        public Dictionary<int, Vehicle> GetVehiclesInGarage
        {
            get
            {
                return m_VehicleCollection;
            }
        }

        public void AddNewVehicle(Vehicle i_VehicleToAdd, Dictionary<string, string> i_VehicleData)
        {
            if (m_VehicleCollection.ContainsKey(i_VehicleToAdd.GetHashCode()) == true)
            {
                m_VehicleCollection[i_VehicleToAdd.GetHashCode()].VehicleStatus = eVehicleStatus.InProgress;
                throw new ExceptionVehicleAlreadyInSystem();
            }

            i_VehicleToAdd.InitializeVehicleData(i_VehicleData);
            m_VehicleCollection.Add(i_VehicleToAdd.GetHashCode(), i_VehicleToAdd);
        }

        public List<string> ShowLicenseNumOfCarsInGarge(eVehicleStatus i_eStatus, bool i_ShowByStatus)
        {
            List<string> vehiclesToShow = new List<string>();

            if (i_ShowByStatus == false)
            {
                foreach(KeyValuePair<int, Vehicle> license in m_VehicleCollection)
                { 
                    vehiclesToShow.Add(license.Value.GetLicenseNumber);
                }
            }
            else
            {
                foreach (KeyValuePair<int, Vehicle> license in m_VehicleCollection)
                {
                    if(license.Value.VehicleStatus == i_eStatus)
                    {
                        vehiclesToShow.Add(license.Value.GetLicenseNumber);
                    }                    
                }
            }

            return vehiclesToShow;
        }

        public void ChangeVehicleStatus(eVehicleStatus i_eVehicleStatus, string i_LicenseNumber)
        {
           if(m_VehicleCollection.ContainsKey(i_LicenseNumber.GetHashCode()) == true)
            {
                m_VehicleCollection[i_LicenseNumber.GetHashCode()].VehicleStatus = i_eVehicleStatus;
            }
           else
            {
                throw new ExceptionVehicleIsNotInSystem();
            }
        }

        public void FillAirInWheelsToMax(string i_LicenseNumber)
        {
            float vehicleMaxAirPressure = m_VehicleCollection[i_LicenseNumber.GetHashCode()].GetMaxAirPressure;
            if (m_VehicleCollection.ContainsKey(i_LicenseNumber.GetHashCode()) == true)
            {
                foreach(Vehicle.Wheels wheel in m_VehicleCollection[i_LicenseNumber.GetHashCode()].GetVehicleWheels)
                {
                    wheel.AddAir(vehicleMaxAirPressure - wheel.GetCurrentAirPressure);
                }

                m_VehicleCollection[i_LicenseNumber.GetHashCode()].CurrentAirPressure = vehicleMaxAirPressure;
            }
            else
            {
                throw new ExceptionVehicleIsNotInSystem();
            }
        }

        public void FillVehiclePower(Vehicle i_Vehicle, float i_AmountOfPowerToAdd, eEngineType i_UserChoiceEngineType)
        {
            if (i_Vehicle.GetEngine.GetEngineType == i_UserChoiceEngineType)
            {
                i_Vehicle.GetEngine.AddPower(i_AmountOfPowerToAdd);
            }
            else
            {
                throw new ArgumentException("Fuel type does not match vehicle");
            }
        }

        public Dictionary<string, string> ShowVehiclDetails(string i_LicenseNumber)
        {
            if (m_VehicleCollection.ContainsKey(i_LicenseNumber.GetHashCode()) == true)
            {
                return m_VehicleCollection[i_LicenseNumber.GetHashCode()].StringVehicleData();
            }
            else
            {
                throw new ExceptionVehicleIsNotInSystem();
            }
        }
    }
}
