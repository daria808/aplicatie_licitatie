using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;
using System.Diagnostics;
using System.Runtime.InteropServices.ComTypes;
using System.Windows.Controls;
using System.Numerics;

namespace Client_ADBD.Helpers
{
    internal class validation
    {
        public static bool IsValidUsername(string username, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(username))    //gol sau contine spatii
            {
                errorMessage = "Username-ul nu poate fi gol sau sa contina spatii.";
                return false;
            }

            if (username.Length < 5 || username.Length > 20)    //  username (minim 5 caractere, maxim 20)
            {
                errorMessage = "Username-ul trebuie sa aiba intre 5 si 20 de caractere.";
                return false;
            }

            string pattern = @"^[a-zA-Z0-9_-]+$";  // Doar litere, cifre, _ si -
            if (!Regex.IsMatch(username, pattern))
            {
                errorMessage = "Username-ul poate contine doar litere, cifre, _ si -. ";
                return false;
            }

            return true;
        }
        public static bool IsValidPassword(string password, out string errorMessage)
        {
            errorMessage = string.Empty;

            //password=password.Trim();

            if (string.IsNullOrWhiteSpace(password))
            {
                errorMessage = "Parola nu poate fi goala.";
                //Debug.WriteLine("Eroare: Parola nu poate fi goala.");
                return false;
            }

            if (password.Length < 8)
            {
                errorMessage = "Parola trebuie sa aiba cel putin 8 caractere.";
                //Debug.WriteLine("Eroare: Parola nu poate fi goala.");
                return false;
            }

            string pattern = @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d@$!%*?&]+$";  // Cel putin o litera si o cifra
            if (!Regex.IsMatch(password, pattern))
            {
                errorMessage = "Parola trebuie sa contina cel putin o litera si o cifra.";
                //Debug.WriteLine("Eroare: Parola nu poate fi goala.");
                return false;
            }

            return true;
        }
        public static bool IsValidEmail(string email, out string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                errorMessage = "Email-ul nu poate fi goala.";
                //Debug.WriteLine("Eroare: Parola nu poate fi goala.");
                return false;
            }

            errorMessage = string.Empty;

            string pattern = @"^[a-zA-Z0-9._]+@[a-zA-Z]+\.[a-zA-Z]{2,}$";
            Regex regex = new Regex(pattern);

            if (regex.IsMatch(email))
            {
                return true;
            }
            else
            {
                errorMessage = "Email-ul nu are un format valid.";
                return false;
            }
        }
        public static bool IsValidName(string name, out string errorMessage)
        {
            errorMessage = string.Empty;

            string pattern = @"^[A-Z][a-z]+$";
            Regex regex = new Regex(pattern);

            if (string.IsNullOrWhiteSpace(name))
            {
                errorMessage = "Numele nu poate fi gol.";
                return false;
            }

            if (!char.IsUpper(name[0]))
            {
                errorMessage = "Prima literă a numelui trebuie să fie majusculă.";
                return false;
            }

            if (regex.IsMatch(name))
            {
                return true;
            }
            else
            {
                errorMessage = "Numele poate conține doar litere, fără spații.";
                return false;
            }
        }

        public static string IsValidAuctionName(string auctionName)
        {
            if (string.IsNullOrWhiteSpace(auctionName))
            {
                return "Numele licitației este obligatoriu.";
            }

            return string.Empty;
        }
        public static string IsValidAuctionType(string auctionType)
        {
            if (string.IsNullOrWhiteSpace(auctionType))
            {
                return "Tipul licitației este obligatoriu.";
            }

            return string.Empty;
        }
        public static string IsValidStartDate(DateTime startDate, ref bool isValid)
        {
            if (startDate == default)
            {

                isValid = false;
                return "Data de început este obligatorie.";
            }

            return string.Empty;
        }
        public static string IsValidEndDate(DateTime endDate, ref bool isValid)
        {
            if (endDate == default)
            {

                isValid = false;
                return "Data de sfârșit este obligatorie.";
            }

            return string.Empty;
        }

        public static string IsValidTime(DateTime startDate, string startHour, string startMinute, DateTime endDate, string endHour, string endMinute, ref DateTime startDateTime, ref DateTime endDateTime, ref bool isValid)
        {


            if (string.IsNullOrWhiteSpace(startHour) || string.IsNullOrWhiteSpace(startMinute) ||
                string.IsNullOrWhiteSpace(endHour) || string.IsNullOrWhiteSpace(endMinute))
            {
                return "Ora și minutele trebuie selectate.";

            }


            if (isValid)
            {
                startDateTime = startDate.AddHours(int.Parse(startHour)).AddMinutes(int.Parse(startMinute));
                endDateTime = endDate.AddHours(int.Parse(endHour)).AddMinutes(int.Parse(endMinute));

                if (endDateTime <= startDateTime)
                {
                    return "Data de sfârșit trebuie să fie după data de început.";
                }
            }

            return string.Empty;
        }

        public static string IsValidImagePath(string imagePath)
        {
            if (string.IsNullOrEmpty(imagePath))
            {

                return "Calea imaginii este necompletată.";
            }

            return string.Empty;
        }

        public static string IsValidLocation(string location)
        {

            if (string.IsNullOrEmpty(location))
            {

                return "Câmpul corespunzător locației este necompletat.";
            }

            return string.Empty;
        }

        public static string IsValidDescription(string description)
        {

            if (string.IsNullOrEmpty(description))
            {

                return "Câmpul corespunzător descrierii este necompletat.";
            }

            if (description.Length >= 700)
            {
                return "Descrierea poate avea maxim 700 de caractere.";
            }

            return string.Empty;
        }

        public static bool IsValidAuction(string AuctionNameError, string AuctionTypeError, string StartDateError, string EndDateError, string TimeError, string ImagePathError, string DescriptionError, string LocationError)
        {
            if (string.IsNullOrEmpty(AuctionNameError) && string.IsNullOrEmpty(AuctionTypeError) &&
              string.IsNullOrEmpty(StartDateError) && string.IsNullOrEmpty(EndDateError) && string.IsNullOrEmpty(TimeError) &&
              string.IsNullOrEmpty(ImagePathError) && string.IsNullOrEmpty(LocationError) && string.IsNullOrEmpty(DescriptionError))
            {
                return true;
            }

            return false;
        }

        public static string IsValidProducttName(string productName)
        {
            if (string.IsNullOrEmpty(productName))
            {

                return "Numele produsului este necompletat.";
            }

            return string.Empty;
        }

        public static string AreValidImagePaths(string[] imagePaths)
        {
            string error = "Este nevoie să încărcați cel puțin o cale de imagine.";

            foreach (string element in imagePaths)
            {
                if (!string.IsNullOrEmpty(element))
                {
                    error = string.Empty;
                }
            }

            return error;

        }

        public static string IsValidStartPrice(decimal price)
        {
            if (price <= 0)
                return "Valoarea prețului de start este invalidă.";

            return string.Empty;
        }

        public static string IsValidListPrice(decimal price)
        {
            if (price <= 0)
                return "Valoarea prețului de listă este invalidă.";

            return string.Empty;
        }

        public static string IsValidInvDate(DateTime date)
        {
            if(date == default)
            {
                return "Data de inventariere este obligatorie.";
            }

            return string.Empty;
        }

        public static bool IsValidPost(string ProductNameError, string ImagePathError, string DescriptionError, string ListPriceError, string StartPriceError, string InvDateError, string ProductControlError)
        {
            if (string.IsNullOrEmpty(ProductNameError) && string.IsNullOrEmpty(ListPriceError) &&
              string.IsNullOrEmpty(StartPriceError) && string.IsNullOrEmpty(InvDateError) &&
              string.IsNullOrEmpty(ImagePathError) && string.IsNullOrEmpty(ProductControlError) && string.IsNullOrEmpty(DescriptionError))
            {
                return true;
            }

            return false;
        }
    }
}

