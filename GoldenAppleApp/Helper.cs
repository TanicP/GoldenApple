using System;
using System.Collections.Generic;
using System.Text;

namespace GoldenAppleApp
{
    public class Helper
    {
        public static void WriteHeader(string headerText)
        {
            Console.WriteLine(string.Format("{0," + ((Console.WindowWidth / 2) +
            headerText.Length / 2) + "}", headerText));
        }
        public static bool ValidateYorN(string entry)
        {
            bool result = false;
            if (entry.ToLower() == "y" || entry.ToLower() == "n")
            {
                result = true;
            }
            return result;
        }
        public static bool ValidateTorF(string entry)
        {
            bool result = false;
            if (entry.ToLower() == "t" || entry.ToLower() == "f")
            {
                result = true;
            }
            return result;
        }
    }
}
