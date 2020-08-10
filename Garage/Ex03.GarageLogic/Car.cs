using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public enum eNumOfDoors
    {
        Two = 2,
        Three,
        Four,
        Five,
    }

    public enum eColor
    {
        Red,
        White,
        Black,
        Silver,
    }

    internal class Car : Vehicle
    {
        private const int k_NumOfWheels = 4;
        private const float k_MaxAirPressure = 32;
        private eColor m_eColor;
        private eNumOfDoors m_eNumOfDoors;

        public Car(string i_LicenseNumber, Power i_Engine) : base(i_LicenseNumber, k_NumOfWheels, k_MaxAirPressure, i_Engine)
        {
        }

        internal eColor GetColor
        {
            get
            {
                return m_eColor;
            }

            set
            {
                m_eColor = value;
            }
        }

        internal eNumOfDoors GetNumOfDoors
        {
            get
            {
                return m_eNumOfDoors;
            }

            set
            {
                m_eNumOfDoors = value;
            }
        }

        internal override Dictionary<string, string> UniqueDetailToAddTToDictionary()
        {
            Dictionary<string, string> uniqueCarDetails = new Dictionary<string, string>();
            string carColor = string.Format(
@"Car color:
1.Red
2.White
3.Black
4.Silver");
            string numberOfDoors = string.Format(
@"Number of doors:
2
3
4
5");
            uniqueCarDetails.Add(carColor, string.Empty);
            uniqueCarDetails.Add(numberOfDoors, string.Empty);
            return uniqueCarDetails;
        }

        internal override void InitializeUniqueVehicleData(Dictionary<string, string> i_DictionaryVehicleData)
        {
            assignCarColor(i_DictionaryVehicleData);
            assignDoorNumber(i_DictionaryVehicleData);
        }

        private void assignCarColor(Dictionary<string, string> i_DictionaryVehicleData)
        {
            string carColor = string.Format(
@"Car color:
1.Red
2.White
3.Black
4.Silver");
            if (int.TryParse(i_DictionaryVehicleData[carColor], out int colorChoice) == true)
            {
                switch (colorChoice)
                {
                   


                    case 1:
                        m_eColor = eColor.Red;
                        break;
                    case 2:
                        m_eColor = eColor.White;
                        break;
                    case 3:
                        m_eColor = eColor.Black;
                        break;
                    case 4:
                        m_eColor = eColor.Silver;
                        break;
                    default:
                        throw new FormatException("Car color choice must be between 1 and " + Enum.GetNames(typeof(eColor)).Length);
                }
            }
            else
            {
                throw new FormatException("Car color choice must be an integer number.");
            }
        }

        private void assignDoorNumber(Dictionary<string, string> i_DictionaryVehicleData)
        {
            string numberOfDoors = string.Format(
@"Number of doors:
2
3
4
5");
            if (int.TryParse(i_DictionaryVehicleData[numberOfDoors], out int doorNumChoice) == true)
            {
                switch (doorNumChoice)
                {
                   

                    case 2:
                        m_eNumOfDoors = eNumOfDoors.Two;
                        break;
                    case 3:
                        m_eNumOfDoors = eNumOfDoors.Three;
                        break;
                    case 4:
                        m_eNumOfDoors = eNumOfDoors.Four;
                        break;
                    case 5:
                        m_eNumOfDoors = eNumOfDoors.Five;
                        break;
                    default:
                        throw new FormatException("Number of doors choice must be between 1 and " + Enum.GetNames(typeof(eNumOfDoors)).Length);
                }
            }
            else
            {
                throw new FormatException("Number of doors choice must be an integer number.");
            }
        }

        internal override Dictionary<string, string> UniqueVehicleDetailsToString()
        {
            Dictionary<string, string> uniqueDetails = new Dictionary<string, string>();
            uniqueDetails.Add("Car color", m_eColor.ToString());
            uniqueDetails.Add("Number of doors", m_eNumOfDoors.ToString());
            return uniqueDetails;
        }
    }
}
