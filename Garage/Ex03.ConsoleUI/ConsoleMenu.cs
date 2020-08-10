using System;
using System.Collections.Generic;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    internal class ConsoleMenu
    {
        private Garage m_Garge;

        private ConsoleMenu()
        {
            m_Garge = new Garage();
        }

        internal static void StartGargeProgram()
        {
            ConsoleMenu consoleMenu = new ConsoleMenu();
            int userMenuChoice;
            consoleMenu.PrintMenu();
            string userInput = Console.ReadLine();
            while (userInput.CompareTo("8") != 0)
            {
                if (int.TryParse(userInput, out userMenuChoice) == true)
                {
                    consoleMenu.activateUserInputAction(userMenuChoice);
                }
                else
                {
                    Console.WriteLine("Please enter menu option 1- 8 only.");
                    System.Threading.Thread.Sleep(1500);
                }

                Console.WriteLine("Enter any key to continue.");
                Console.ReadLine();
                Console.Clear();
                consoleMenu.PrintMenu();
                userInput = Console.ReadLine();
            }
        }

        private void printVehicleTypes()
        {
            int i = 1;
            foreach (eTypeOfVehicles typeVehicle in (eTypeOfVehicles[])Enum.GetValues(typeof(eTypeOfVehicles)))
            {
                Console.WriteLine(i + ")" + typeVehicle.ToString());
                i++;
            }
        }

        private void printFuelEngineTypes()
        {
            int i = 1;
            foreach (eEngineType typeEngine in (eEngineType[])Enum.GetValues(typeof(eEngineType)))
            {
                if (typeEngine != eEngineType.Electric)
                {
                    Console.WriteLine(i + ")" + typeEngine.ToString());
                    i++;
                }
            }
        }

        
        private void activateUserInputAction(int i_UserMenuChoice)
        {
            switch (i_UserMenuChoice)
            {
                case 1:
                    addVehicleToGarage();
                    break;
                case 2:
                    showVehicleIDByStatus();
                    break;
                case 3:
                    changeVehicleStatus();
                    break;
                case 4:
                    fillAirInWheels();
                    break;
                case 5:
                    addPower(true);
                    break;
                case 6:
                    addPower(false);
                    break;
                case 7:
                    showAllVehicleData();
                    break;
                case 8:
                    Environment.Exit(-1);
                    break;
                default:
                    Console.WriteLine("Please enter menu option 1- 8 only.");
                    System.Threading.Thread.Sleep(1500);
                    break;
            }
        }

        internal void PrintMenu()
        {
            Console.WriteLine(string.Format(
@"Main Menu:
1. Enter a new vehicle to the garage
2. Show vehicle license number of vehicles in garage
3. Change vehicle status
4. Fill vehicle whells air pressure
5. Fill gas in vehicle
6. Charge electric vehicle
7. Show vehicle details
8. Exit program"));
        }

        private void addVehicleToGarage()
        {
            string licenseNumber = getInputLicenseNumber();
            int numOfVehicleType = getUsersVehicleType();

            Vehicle vehicleToAdd = CreateNewVehicle.CreateVehicle(numOfVehicleType, licenseNumber);
            Dictionary<string, string> vehicleDetails = vehicleToAdd.VehicleDetailsToGet();
            vehicleDetails = fillVehicleDictionaryDetails(vehicleDetails, vehicleToAdd.GetMaxAirPressure, vehicleToAdd.GetMaxPower);

            try
            {
                m_Garge.AddNewVehicle(vehicleToAdd, vehicleDetails);
            }
            catch (ExceptionVehicleAlreadyInSystem)
            {
                Console.WriteLine("Vehicle already in garage, status updated to \"in progress\"");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Vehicle was not added to garage.");
            }
        }

        private int getUsersVehicleType()
        {
            bool goodInput = false;
            Console.WriteLine("Choose type of vehicle to enter:");
            printVehicleTypes();
            string userInputVehicleType = Console.ReadLine();
            int numTypeOfVehicles = Enum.GetNames(typeof(eTypeOfVehicles)).Length;
            int userVehicleIntChoice;

            do
            {
                if (int.TryParse(userInputVehicleType, out userVehicleIntChoice) == true)
                {
                    if (userVehicleIntChoice >= 1 && userVehicleIntChoice <= numTypeOfVehicles)
                    {
                        goodInput = true;
                    }
                    else
                    {
                        Console.WriteLine("Choice must be between 1 and " + numTypeOfVehicles);
                        userInputVehicleType = Console.ReadLine();
                    }
                }
                else
                {
                    Console.WriteLine("Choose Vehicle type by its index.");
                    userInputVehicleType = Console.ReadLine();
                }
            }
            while (goodInput == false);

            return userVehicleIntChoice;
        }

        private string getInputLicenseNumber()
        {
            string licenseNumber;
            Console.WriteLine("Enter vehicle's license number: ");
            licenseNumber = Console.ReadLine();
            while (licenseNumber.CompareTo(string.Empty) == 0)
            {
                Console.WriteLine("license number cannotbe empty.");
                Console.WriteLine("Enter vehicle's license number: ");
                licenseNumber = Console.ReadLine();
            }

            return licenseNumber;
        }

        private Dictionary<string, string> fillVehicleDictionaryDetails(Dictionary<string, string> i_Details, float i_VehicleMaxAirPressure, float i_VehicleMaxPowerAmount)
        {
            bool? goodinput;
            string userInput;
            Dictionary<string, string> userInputeDetails = new Dictionary<string, string>();
            foreach (var detail in i_Details)
            {
                do
                {
                    Console.WriteLine("Enter " + detail.Key);
                    userInput = Console.ReadLine();
                    goodinput = checkUserVehicleDetailInput(userInput, detail, i_VehicleMaxAirPressure, i_VehicleMaxPowerAmount);
                    if (goodinput == true)
                    {
                        userInputeDetails.Add(detail.Key, userInput);
                    }
                }
                while (goodinput == false);
            }

            return userInputeDetails;
        }

        private void showVehicleIDByStatus()
        {
            eVehicleStatus eUserStatus;
            List<string> itemsToPrint = new List<string>();
            Console.WriteLine("If you wish to only show vehicles with a certain status enter \"yes\" otherwise anything else.");
            string userInputChoice = Console.ReadLine();
            if (userInputChoice.CompareTo("yes") == 0)
            {
                eUserStatus = userVehicleStatuseChoice();
                itemsToPrint = m_Garge.ShowLicenseNumOfCarsInGarge(eUserStatus, true);
            }
            else
            {
                itemsToPrint = m_Garge.ShowLicenseNumOfCarsInGarge(eVehicleStatus.InProgress, false);
            }

            if (itemsToPrint.Count != 0)
            {
                printLicenseList(itemsToPrint);
            }
            else
            {
                


                if (userInputChoice.CompareTo("yes") == 0)
                {
                    Console.WriteLine("There are no vehicles with this status in the garage.");
                }
                else
                {
                    Console.WriteLine("There are no vehicles in the garage.");
                }
            }
        }

        private void printLicenseList(List<string> i_LicenseToPrint)
        {
            int i = 1;
            foreach (string license in i_LicenseToPrint)
            {
                Console.WriteLine(string.Format(
@"License of vehicles in garage:
" + i + ")" + license));
            }
        }
        

        private bool checkUserVehicleDetailInput(string i_UserInput, KeyValuePair<string, string> i_Detail, float i_VehiclMaxAirPressure, float i_VehicleMaxPowerAmount)
        {
            bool goodInput = true;
            switch (i_Detail.Key)
            {
                case "owner's name":
                    goodInput = isUserValidName(i_UserInput);
                    break;
                case "owner's phone number":
                    goodInput = isUserInputPhoneNumberValid(i_UserInput);
                    break;
                case "vehicle brand name":
                    if (i_UserInput.CompareTo(string.Empty) == 0)
                    {
                        Console.WriteLine("brand name cannot be empty.");
                        goodInput = false;
                    }

                    break;
                case "wheel manufacturer name":
                    if (i_UserInput.CompareTo(string.Empty) == 0)
                    {
                        Console.WriteLine("wheel manufacturer name cannot be empty.");
                        goodInput = false;
                    }

                    break;
                case "wheel current air pressure":
                    goodInput = isUserWeelsAirPressurValid(i_UserInput, i_VehiclMaxAirPressure);
                  break;
                case "current fuel/battery left in vehicle":
                    goodInput = isInputCurrentPowerValid(i_UserInput, i_VehicleMaxPowerAmount);
                    break;
                default:
                    goodInput = true;
                    break;
            }

            return goodInput;
        }

        private bool isInputCurrentPowerValid(string i_InputCurrenPower, float i_VehicleMaxPowerAmount)
        {
            bool goodInput = true;
            if (i_InputCurrenPower.CompareTo(string.Empty) == 0)
            {
                Console.WriteLine("current power field cannot be empty.");
                goodInput = false;
            }

            if (float.TryParse(i_InputCurrenPower, out float powerLeft) == true)
            {
                if (powerLeft < 0)
                {
                    Console.WriteLine("current power field cannot be negative");
                    goodInput = false;
                }
                else if (powerLeft > i_VehicleMaxPowerAmount)
                {
                    Console.WriteLine("current power field cannot be above the max amount wich is: " + i_VehicleMaxPowerAmount);
                    goodInput = false;
                }
            }
            else
            {
                Console.WriteLine("current power field must be a float number.");
                goodInput = false;
            }

            return goodInput;
        }

        private bool isUserWeelsAirPressurValid(string i_InputAirPressur, float i_VehiclMaxAirPressure)
        {
            bool goodInput = true;
            if (i_InputAirPressur.CompareTo(string.Empty) == 0)
            {
                Console.WriteLine("air pressure field cannot be empty.");
                goodInput = false;
            }
            else if (float.TryParse(i_InputAirPressur, out float airPressure) == true)
            {
                if (airPressure < 0)
                {
                    Console.WriteLine("air pressure cannot be nagative.");
                    goodInput = false;
                }

                if (airPressure > i_VehiclMaxAirPressure)
                {
                    Console.WriteLine("current air pressure cannot be bigget then the max air pressure wich is: " + i_VehiclMaxAirPressure);
                    goodInput = false;
                }
            }
            else
            {
                Console.WriteLine("air pressure must be a float number.");
                goodInput = false;
            }

            return goodInput;
        }

        private bool isUserValidName(string i_UserInputName)
        {
            bool goodInput = false;
            if (i_UserInputName.CompareTo(string.Empty) == 0)
            {
                Console.WriteLine("owner's name cannot be empty.");
                goodInput = false;
            }
            else
            {
                goodInput = true;
            }

            return goodInput;
        }

        private bool isUserInputPhoneNumberValid(string i_UserInputPhoneNumber)
        {
            bool goodInput = true;
            if (i_UserInputPhoneNumber.CompareTo(string.Empty) == 0)
            {
                Console.WriteLine("phone number cannot be empty.");
                goodInput = false;
            }
            else if (int.TryParse(i_UserInputPhoneNumber, out int phoneNum) == false)
            {
                Console.WriteLine("phone number must be an integer number.");
                goodInput = false;
            }

            return goodInput;
        }

        private void changeVehicleStatus()
        {
            if (m_Garge.GetVehiclesInGarage.Count != 0)
            {
                eVehicleStatus eUserStatusChoice;
                Console.WriteLine("Enter vehicle license number.");
                string userInputLicenseNumber = userInputLicenseNumberInGarage();
                eUserStatusChoice = userVehicleStatuseChoice();
                m_Garge.ChangeVehicleStatus(eUserStatusChoice, userInputLicenseNumber);
                Console.WriteLine("Vehicle status updated.");
            }
            else
            {
                Console.WriteLine("There are currently no vehicles in the garage.");
                return;
            }
        }

        private eVehicleStatus userVehicleStatuseChoice()
        {
            string statusNumberString;
            int statusNumberInt;
            bool goodInput = false;
            eVehicleStatus eUserStatusChoice;
            Console.WriteLine("Choose vehicle status");
            Console.WriteLine(string.Format(
@"1.Currently in progress.
2.Fixed but unpaid.
3.Paid"));
            do
            {
                statusNumberString = Console.ReadLine();
                if (int.TryParse(statusNumberString, out statusNumberInt) == true)
                {
                    if (statusNumberInt >= 1 && statusNumberInt <= 3)
                    {
                        goodInput = true;
                    }
                    else
                    {
                        Console.WriteLine("Choice must be between 1 and 3.");
                    }
                }
                else
                {
                    Console.WriteLine("Choice must be an integer number.");
                }
            }
            while (goodInput == false);
            switch (statusNumberInt)
            {
                case 1:
                    eUserStatusChoice = eVehicleStatus.InProgress;
                    break;
                case 2:
                    eUserStatusChoice = eVehicleStatus.Repaired;
                    break;
                default:
                    eUserStatusChoice = eVehicleStatus.Paid;
                    break;
            }

            return eUserStatusChoice;
        }

        private void fillAirInWheels()
        {
            if (m_Garge.GetVehiclesInGarage.Count != 0)
            {
                string licenseNumber = userInputLicenseNumberInGarage();
                m_Garge.FillAirInWheelsToMax(licenseNumber);
                Console.WriteLine("Wheel's of vehicle " + licenseNumber + " air pressure filled to max.");
            }
            else
            {
                Console.WriteLine("There are no vehicles in the garage.");
            }
        }

        private string userInputLicenseNumberInGarage()
        {
            bool validLicenseNumber = false;
            Console.WriteLine("Enter vehicle license number.");
            string userInputLicenseNumber = Console.ReadLine();
            do
            {
                if (m_Garge.GetVehiclesInGarage.ContainsKey(userInputLicenseNumber.GetHashCode()) == true)
                {
                    validLicenseNumber = true;
                }
                else if (userInputLicenseNumber.CompareTo(string.Empty) != 0)
                {
                    Console.WriteLine("There is no vehicle with license number " + userInputLicenseNumber + " in the garage.");
                    Console.WriteLine("Try again:");
                    userInputLicenseNumber = Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Vehicle license cannot be empty.");
                    Console.WriteLine("Try again:");
                    userInputLicenseNumber = Console.ReadLine();
                }
            }
            while (validLicenseNumber == false);
            return userInputLicenseNumber;
        }


        private void addPower(bool i_UserChosenGas)
        {
            if (m_Garge.GetVehiclesInGarage.Count != 0)
            {
                string licenseNumber = userInputLicenseNumberInGarage();
                Vehicle vehicleToFillGas = m_Garge.GetVehiclesInGarage[licenseNumber.GetHashCode()];
                if (vehicleToFillGas.IsGasEngine() == i_UserChosenGas)
                {
                    if (vehicleToFillGas.IsGasEngine() == true)
                    {
                        chargeGasVehicle(licenseNumber);
                    }
                    else
                    {
                        chargeElectricVehicle(licenseNumber);
                    }
                }
                else
                {
                    Console.WriteLine("Vehicle does not use this type of fuel/battery.");
                }
            }
            else
            {
                Console.WriteLine("There are no vehicles in the garage.");
            }
        }

        private void chargeGasVehicle(string i_LicenseNumber)
        {
            float gasAmountToFill;
            string userEngineTypechoice;
            eEngineType eUsersEngineType;
            bool goodInput = false;
            Vehicle vehicleToFillGas = m_Garge.GetVehiclesInGarage[i_LicenseNumber.GetHashCode()];

            printFuelEngineTypes();
            Console.WriteLine("Choose type of fuel to put in vehicle.");
            do
            {
                userEngineTypechoice = Console.ReadLine();
                if (int.TryParse(userEngineTypechoice, out int validEngineChoice) == true)
                {
                    if (validEngineChoice >= 1 && validEngineChoice <= 4)
                    {
                        goodInput = true;
                        gasAmountToFill = usersAmountOfGasToFill();
                        eUsersEngineType = usersEngineType(validEngineChoice);
                       
                        try
                        {
                            m_Garge.FillVehiclePower(vehicleToFillGas, gasAmountToFill, eUsersEngineType);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            Console.WriteLine("Gas tank was not filled.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Choice out of menu bounds.");
                    }
                }
                else
                {
                    Console.WriteLine("Engine choice must be an integer number.");
                }
            }
            while (goodInput == false);
        }

        private float usersAmountOfGasToFill()
        {
            bool goodInput = false;
            float gasAmountToFill;
            Console.WriteLine("How much Gas do you wish to fill?");
            string userInputGasAmount = Console.ReadLine();
            do
            {
                if (float.TryParse(userInputGasAmount, out gasAmountToFill) == false)
                {
                    Console.WriteLine("Amount of gas must be a float number.");
                    userInputGasAmount = Console.ReadLine();
                }
                else
                {
                    goodInput = true;
                }
            }
            while (goodInput == false);
            return gasAmountToFill;
        }

        
        private eEngineType usersEngineType(int i_UserInputNumOfEngineType)
        {
            eEngineType usersEngineType;
            switch (i_UserInputNumOfEngineType)
            {
                case 1:
                    usersEngineType = eEngineType.Octan95;
                    break;
                case 2:
                    usersEngineType = eEngineType.Octan96;
                    break;
                case 3:
                   usersEngineType = eEngineType.Octan98;
                    break;
                default:
                   usersEngineType = eEngineType.Soler;
                    break;
            }

            return usersEngineType;
        }

        private void chargeElectricVehicle(string i_LicenseNumber)
        {
            bool goodInput = false;
            string userInputMinutesToCharge;
            Vehicle vehicleToCharge = m_Garge.GetVehiclesInGarage[i_LicenseNumber.GetHashCode()];
            Console.WriteLine("Enter how many minutes to your wish to charge your vehicle.");
            userInputMinutesToCharge = Console.ReadLine();
            do
            {
                if (float.TryParse(userInputMinutesToCharge, out float minutesToCharge) == true)
                {
                    try
                    {
                        m_Garge.FillVehiclePower(vehicleToCharge, minutesToCharge / 60, eEngineType.Electric);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.WriteLine("Vehicle was not charged.");
                    }

                    goodInput = true;
                }
                else
                {
                    Console.WriteLine("Charge time must be a float number.");
                    userInputMinutesToCharge = Console.ReadLine();
                }
            }
            while (goodInput == false);
        }

        private void showAllVehicleData()
        {
            if (m_Garge.GetVehiclesInGarage.Count != 0)
            {
                string licenseNumber;
                licenseNumber = userInputLicenseNumberInGarage();
                Dictionary<string, string> vehicleDataToPrint = m_Garge.ShowVehiclDetails(licenseNumber);
                foreach (var data in vehicleDataToPrint)
                {
                    Console.WriteLine(data.Key + ": " + data.Value);
                }
            }
            else
            {
                Console.WriteLine("There are no vehicles in the garage.");
            }
        }
    }
}
