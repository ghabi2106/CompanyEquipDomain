using System.Globalization;

namespace Library.Converter
{
    public class DecimalConverter
    {
        public static decimal StringToDecimal(string decimalString)
        {
            decimal result = 0;
            try
            {
                result = decimal.Parse(decimalString, CultureInfo.InvariantCulture.NumberFormat);
            }
            catch { }

            return result;
        }

        public static string DecimalToString(decimal number)
        {
            string result = "";
            try
            {
                result = number.ToString(CultureInfo.InvariantCulture.NumberFormat);
            }
            catch { }

            return result;
        }
    }
}
