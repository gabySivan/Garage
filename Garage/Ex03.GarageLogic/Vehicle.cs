using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public enum eVehicleStatus
    {
        InProgress,
        Repaired,
        Paid,
    }


    public abstract class Vehicle
    {
        protected readonly string r_LicenseNumber;
        private readonly List<Wheels> r_VehicleWheels;
        protected string m_OwnerName;
        protected string m_OwnerPhoneNumber;
        protected string m_BrandName;
        protected string m_WheelManuFacturer;
        protected float m_CurrentWheelAirPressure;


     

        protected float m_MaxWheelAirPressure;
        protected int m_NumOfWheels;
        protected eVehicleStatus m_eStatus;
        private Power m_Engine;

        internal Vehicle(string i_LicenseNumber, int i_NumOfWheels, float i_MaxAirPressure, Power i_Engine)
        {
            r_LicenseNumber = i_LicenseNumber;
            m_Engine = i_Engine;
            m_NumOfWheels = i_NumOfWheels;
            m_MaxWheelAirPressure = i_MaxAirPressure;
            r_VehicleWheels = new List<Wheels>();
        }

        internal Power GetEngine
        {
            get
            {
                return m_Engine;
            }
        }

        public float GetMaxPower
        {
            get
            {
                return m_Engine.GetMaxPower;
            }
        }

        internal eVehicleStatus VehicleStatus
        {
            get
            {
                return m_eStatus;
            }

            set
            {
                m_eStatus = value;
            }
        }

        public float GetMaxAirPressure
        {
            get
            {
                return m_MaxWheelAirPressure;
            }
        }

        internal string GetBrandName
        {
            get
            {
                return m_BrandName;
            }
        }

        internal string GetLicenseNumber
        {
            get
            {
                return r_LicenseNumber;
            }
        }

        internal List<Wheels> GetVehicleWheels
        {
            get
            {
                return r_VehicleWheels;
            }
        }

        internal float CurrentAirPressure
        {
            get
            {
                return m_CurrentWheelAirPressure;
            }

            set
            {
                m_CurrentWheelAirPressure = value;
            }
        }

        public static bool operator ==(Vehicle i_vehicle1, Vehicle i_vehicle2)
        {
            return i_vehicle1.Equals(i_vehicle2);
        }

        public static bool operator !=(Vehicle i_vehicle1, Vehicle i_vehicle2)
        {
            return !(i_vehicle1 == i_vehicle2);
        }

        public override int GetHashCode()
        {
            return r_LicenseNumber.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            bool equals = false;

            Vehicle toCompare = obj as Vehicle;
            if (toCompare != null) 
            {
                if (this.r_LicenseNumber.CompareTo(toCompare.r_LicenseNumber) == 0) 
                {
                    equals = true;
                }
            }

            return equals;
        }

        private void SetWheels()
        {
            for (int i = 0; i < m_NumOfWheels; i++)
            {
                r_VehicleWheels.Add(new Wheels(m_WheelManuFacturer, m_CurrentWheelAirPressure, m_MaxWheelAirPressure));
            }
        }

        public Dictionary<string, string> VehicleDetailsToGet()
        {
            Dictionary<string, string> details = new Dictionary<string, string>();
            details.Add("owner's name", string.Empty);
            details.Add("owner's phone number", string.Empty);
            details.Add("vehicle brand name", string.Empty);
            details.Add("wheel manufacturer name", string.Empty);
            details.Add("wheel current air pressure", string.Empty);
            details.Add("current fuel/battery left in vehicle", string.Empty);
            Dictionary<string, string> uniqueDetails = UniqueDetailToAddTToDictionary();

            foreach(var detail in uniqueDetails)
            {
                details.Add(detail.Key, detail.Value);
            }

            return details;
        }

        public bool? IsGasEngine()
        {
            bool? isGasType;
            if(m_Engine.GetEngineType == eEngineType.Electric)
            {
                isGasType = false;
            }
            else
            {
                isGasType = true;
            }

            return isGasType;
        }

        internal abstract Dictionary<string, string> UniqueDetailToAddTToDictionary();

        internal void InitializeVehicleData(Dictionary<string, string> i_DictionaryVehicleData)
        {
           if(i_DictionaryVehicleData["owner's name"].CompareTo(string.Empty) == 0)
            {
                throw new FormatException("Car owner's name cannot be empty.");
            }
           else
            {
                m_OwnerName = i_DictionaryVehicleData["owner's name"];
            }

           if(int.TryParse(i_DictionaryVehicleData["owner's phone number"], out int temp) == false)
            {
                throw new FormatException("phone number can contain only numbers.");
            }
           else
            {
                m_OwnerPhoneNumber = i_DictionaryVehicleData["owner's phone number"];
            }

            if (i_DictionaryVehicleData["vehicle brand name"].CompareTo(string.Empty) == 0)
            {
                throw new FormatException("Vehicle brand cannot be empty.");
            }
            else
            {
                m_BrandName = i_DictionaryVehicleData["vehicle brand name"];
            }

            if (i_DictionaryVehicleData["wheel manufacturer name"].CompareTo(string.Empty) == 0)
            {
                throw new FormatException("Wheel manufacturer name cannot be empty.");
            }
            else
            {
                m_WheelManuFacturer = i_DictionaryVehicleData["wheel manufacturer name"];
            }

            if (isCurrentAirPressureValidValue(i_DictionaryVehicleData) == true)
            {
                float.TryParse(i_DictionaryVehicleData["wheel current air pressure"], out m_CurrentWheelAirPressure);
            }

            if (isCurrentPowerAmountValidValue(i_DictionaryVehicleData) == true)
            {
                float.TryParse(i_DictionaryVehicleData["current fuel/battery left in vehicle"], out float currentPower);
                m_Engine.CurrentPower = currentPower;
            }

            SetWheels();
            InitializeUniqueVehicleData(i_DictionaryVehicleData);
        }

        internal abstract void InitializeUniqueVehicleData(Dictionary<string, string> i_DictionaryVehicleData);

        private bool isCurrentPowerAmountValidValue(Dictionary<string, string> i_DictionaryVehicleData)
        {
            bool validCurrentPowerAmount = false;
            if (float.TryParse(i_DictionaryVehicleData["current fuel/battery left in vehicle"], out float currentPowerAmount) == true)
            {
                if (currentPowerAmount <= m_Engine.GetMaxPower && currentPowerAmount >= 0)
                {
                    validCurrentPowerAmount = true;
                }
                else
                {
                    throw new ArgumentException("Power amount too big or is negative.");
                }
            }
            else
            {
                throw new FormatException("Power left must be a float number.");
            }

            return validCurrentPowerAmount;
        }

        private bool isCurrentAirPressureValidValue(Dictionary<string, string> i_DictionaryVehicleData)
        {
            bool validCurrentAirPressure = false;
            if (float.TryParse(i_DictionaryVehicleData["wheel current air pressure"], out float currentAirPressure) == true)
            {
                if (currentAirPressure <= m_MaxWheelAirPressure && currentAirPressure >= 0)
                {
                    validCurrentAirPressure = true;
                }
                else
                {
                    throw new ArgumentException("Air Pressure too big or is negative.");
                }
            }
            else
            {
                throw new FormatException("Air pressure must be a float number.");
            }

            return validCurrentAirPressure; 
        }

        internal abstract Dictionary<string, string> UniqueVehicleDetailsToString();

        internal Dictionary<string, string> StringVehicleData()
        {
            Dictionary<string, string> details = new Dictionary<string, string>();
            Dictionary<string, string> uniqueDetails;
            details.Add("License number", r_LicenseNumber);
            details.Add("Owner's name", m_OwnerName);
            details.Add("Owner's phone number", m_OwnerPhoneNumber);
            details.Add("Vehicle brand name", m_BrandName);
            details.Add("Wheel manufacturer name", m_WheelManuFacturer);
            details.Add("Wheel current air pressure", m_CurrentWheelAirPressure.ToString());
            details.Add("Vehicle status", m_eStatus.ToString());
            details.Add("Engine type", m_Engine.GetEngineType.ToString());
            if (m_Engine.GetEngineType == eEngineType.Electric)
            {
                details.Add("Electricity left", m_Engine.CurrentPower.ToString());
            }
            else
            {
                details.Add(m_Engine.GetEngineType.ToString() + " left", m_Engine.CurrentPower.ToString());
            }

            uniqueDetails = UniqueVehicleDetailsToString();
            foreach(var detail in uniqueDetails)
            {
                details.Add(detail.Key, detail.Value);
            }

            return details;
        }

        internal class Wheels
        {
            private readonly string r_ManufacturerName;
            private readonly float r_MaxAirPressure;
            private float m_CurerntAirPressure;

            internal Wheels(string i_ManufacturerName, float i_currentAirPressure, float i_MaxAirPressure)
            {
                r_ManufacturerName = i_ManufacturerName;
                m_CurerntAirPressure = i_currentAirPressure;
                r_MaxAirPressure = i_MaxAirPressure;
            }

            internal float GetCurrentAirPressure
            {
                get
                {
                    return m_CurerntAirPressure;
                }
            }

            internal string GetManufacturerName
            {
                get
                {
                    return r_ManufacturerName;
                }
            }

            internal float GetMaxAirPressure
            {
                get
                {
                    return r_MaxAirPressure;
                }
            }

            internal void AddAir(float i_AirToAdd)
            {
                if (m_CurerntAirPressure + i_AirToAdd > r_MaxAirPressure || i_AirToAdd < 0) 
                {
                    throw new ValueOutOfRangeException(0, r_MaxAirPressure - m_CurerntAirPressure);
                }

                m_CurerntAirPressure += i_AirToAdd;
            }
        }
    }
}
