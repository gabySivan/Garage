using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
     internal class Truck : Vehicle
    {
        private const int k_NumOfWheels = 16;
        private const float k_MaxAirPressure = 28;
        private bool? m_DangerousGoods;
        private float m_CargoVolume;

        internal Truck(string i_License, Power i_Engine) : base(i_License, k_NumOfWheels, k_NumOfWheels, i_Engine)
        {
        }

        internal float GetCargoVolume
        {
            get
            {
                return m_CargoVolume;
            }      
        }

        internal bool? IsDangerousGoods
        {
            get
            {
                return m_DangerousGoods;
            }

            set
            {
                m_DangerousGoods = value;
            }
        }
        

        internal override Dictionary<string, string> UniqueDetailToAddTToDictionary()
        {
            Dictionary<string, string> uniqueTruckDetails = new Dictionary<string, string>();
            uniqueTruckDetails.Add("\"yes\"if you are carrying dangerous goods otherwise enter anything else", string.Empty);
            uniqueTruckDetails.Add("cargo vloume", string.Empty);
            return uniqueTruckDetails;
        }

        internal override void InitializeUniqueVehicleData(Dictionary<string, string> i_DictionaryVehicleData)
        {
            assignTruckDangerousGoods(i_DictionaryVehicleData);
            assignTruckCargoVolume(i_DictionaryVehicleData);
        }

        private void assignTruckDangerousGoods(Dictionary<string, string> i_DictionaryVehicleData)
        {
            string isDangerousGoods = "\"yes\"if you are carrying dangerous goods otherwise enter anything else";

            if(i_DictionaryVehicleData[isDangerousGoods].CompareTo("yes") == 0)
            {
                m_DangerousGoods = true;
            }
            else
            {
                m_DangerousGoods = false;
            }
        }

        private void assignTruckCargoVolume(Dictionary<string, string> i_DictionaryVehicleData)
        {
            if (float.TryParse(i_DictionaryVehicleData["cargo vloume"], out m_CargoVolume) == true)
            {
                if (m_CargoVolume < 0)
                {
                    throw new ArgumentException("Cargo volume cannot be negative.");
                }
            }
            else
            {
                throw new FormatException("cargo volume must be a float number.");
            }
        }

        internal override Dictionary<string, string> UniqueVehicleDetailsToString()
        {
            Dictionary<string, string> uniqueDetails = new Dictionary<string, string>();
            if (m_DangerousGoods == true)
            {
                uniqueDetails.Add("Is carrying dangerous goods?", "yes");
            }
            else
            {
                uniqueDetails.Add("Is carrying dangerous goods?", "no");
            }

            uniqueDetails.Add("Cargo Volume ", m_CargoVolume.ToString());
            return uniqueDetails;
        }
    }
}
