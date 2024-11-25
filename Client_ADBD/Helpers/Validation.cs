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
    }   
}

