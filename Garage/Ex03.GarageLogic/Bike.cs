using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public enum eLicenseType
    {
        A,
        A1,
        AA,
        B,
    }

    internal class Bike : Vehicle
   {
        private const int k_NumOfWheels = 2;
        private const int k_MaxAirPressure = 30;
        private eLicenseType m_eLicenseType;

        public Bike(string i_LicenseNumber, Power i_Engine) : base(i_LicenseNumber, k_NumOfWheels, k_MaxAirPressure, i_Engine)
        {
        }

      internal eLicenseType GetLicenseType
      {
            get
            {
                return m_eLicenseType;
            }

            set
            {
                m_eLicenseType = value;
            }
       }

       internal override Dictionary<string, string> UniqueDetailToAddTToDictionary()
       {
           Dictionary<string, string> uniqueBikeDetails = new Dictionary<string, string>();
           string licenseType = string.Format(
@"License Type
1.A
2.A1
3.AA
4.B");
           uniqueBikeDetails.Add(licenseType, string.Empty);
           return uniqueBikeDetails;
       }

        internal override void InitializeUniqueVehicleData(Dictionary<string, string> i_DictionaryVehicleData)
        {
            string licenseType = string.Format(
@"License Type
1.A
2.A1
3.AA
4.B");
            if (int.TryParse(i_DictionaryVehicleData[licenseType], out int licenseChoice) == true)
            {
                switch (licenseChoice)
                {
                   


                    case 1:
                        m_eLicenseType = eLicenseType.A;
                        break;
                    case 2:
                        m_eLicenseType = eLicenseType.A1;
                        break;
                    case 3:
                        m_eLicenseType = eLicenseType.AA;
                        break;
                    case 4:
                        m_eLicenseType = eLicenseType.B;
                        break;
                    default:
                        throw new FormatException("Choice must be between 1 and " + Enum.GetNames(typeof(eLicenseType)).Length);
                }
            }
            else
            {
                throw new FormatException("Choice must be an integer number.");
            }
        }

        internal override Dictionary<string, string> UniqueVehicleDetailsToString()
        {
            Dictionary<string, string> uniqueDetails = new Dictionary<string, string>();
            uniqueDetails.Add("License type", m_eLicenseType.ToString());
            return uniqueDetails;
        }
    }
}
