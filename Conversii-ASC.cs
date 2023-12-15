using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COD_TemaConversii_ASC
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Introduceti numarul in virgula fixa (folosii . pentru zecimale): ");
            string numarInVirgulaFixa = Console.ReadLine();
            Console.Write("Introduceti baza sursa (2-16): ");
            int bazaSursa = int.Parse(Console.ReadLine());
            Console.Write("Introduceti baza tinta (2-16): ");
            int bazaTinta = int.Parse(Console.ReadLine());
            try
            {
                string numarInBazaTinta = ConvertBase(numarInVirgulaFixa, bazaSursa, bazaTinta);
                Console.WriteLine($"Rezultatul conversiei: {numarInBazaTinta}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare: {ex.Message}");
            }
            Console.ReadLine();
        }
        static string ConvertBase(string numar, int bazaSursa, int bazaTinta)
        {
            if (bazaSursa < 2 || bazaSursa > 16 || bazaTinta < 2 || bazaTinta > 16)
            {
                throw new ArgumentException("Bazele trebuie să fie între 2 și 16.");
            }
            try
            {
                string[] parti = numar.Split('.');
                string parteIntreaga = parti[0];
                string parteZecimala = parti.Length > 1 ? parti[1] : "0";
                double intPartInBase10 = ConvertToBase10(parteIntreaga, bazaSursa);
                double decimalPartInBase10 = ConvertDecimalPartToBase10(parteZecimala, bazaSursa);
                string intPartInBaseTinta = ConvertFromBase10(intPartInBase10, bazaTinta);
                string decimalPartInBaseTinta = ConvertDecimalPartFromBase10(decimalPartInBase10, bazaTinta);
                string numarInBazaTinta = intPartInBaseTinta + (decimalPartInBaseTinta == "0" ? "" : "." + decimalPartInBaseTinta);
                return numarInBazaTinta;
            }
            catch (FormatException)
            {
                throw new ArgumentException("Numărul introdus nu corespunde bazei sursă specificate.");
            }
            Console.ReadLine();
        }
        static double ConvertToBase10(string numar, int baza)
        {
            double result = 0;
            for (int i = numar.Length - 1; i >= 0; i--)
            {
                int cifra = CharToDigit(numar[i]);
                result += cifra * Math.Pow(baza, numar.Length - 1 - i);
            }
            return result;
            Console.ReadLine();
        }
        static double ConvertDecimalPartToBase10(string numar, int baza)
        {
            double result = 0;
            for (int i = 0; i < numar.Length; i++)
            {
                int cifra = CharToDigit(numar[i]);
                result += cifra * Math.Pow(baza, -(i + 1));
            }
            return result;
            Console.ReadLine();
        }
        static string ConvertFromBase10(double numar, int baza)
        {
            if (numar == 0)
            {
                return "0";
            }
            string result = "";
            while (numar > 0)
            {
                double rest = numar % baza;
                result = DigitToChar((int)rest) + result;
                numar /= baza;
            }
            return result;
            Console.ReadLine();
        }
        static string ConvertDecimalPartFromBase10(double numar, int baza)
        {
            string result = "";
            for (int i = 0; i < 6; i++) 
            {
                numar *= baza;
                int cifra = (int)numar;
                result += DigitToChar(cifra);
                numar -= cifra;
            }
            return result;
            Console.ReadLine();
        }
        static int CharToDigit(char cifra)
        {
            if (char.IsDigit(cifra))
            {
                return (int)(cifra - '0');
            }
            else
            {
                return (int)(char.ToUpper(cifra) - 'A') + 10;
            }
            Console.ReadLine();
        }
        static char DigitToChar(int cifra)
        {
            if (cifra >= 0 && cifra <= 9)
            {
                return (char)(cifra + '0');
            }
            else
            {
                return (char)(cifra - 10 + 'A');
            }
            Console.ReadLine();
        }
    }
}