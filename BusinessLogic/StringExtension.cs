using System.Globalization;
using System.Text.RegularExpressions;

namespace DensityOfWaterAlcoholSolution.BusinessLogic
{
    public static class StringExtension
    {
        public static double DoubleParseAdvanced(this string strToParse, char decimalSymbol = ',')
        {
            string tmp = Regex.Match(strToParse, @"([-]?[0-9]+)([\s])?([0-9]+)?[." + decimalSymbol + "]?([0-9 ]+)?([0-9]+)?").Value;

            if (tmp.Length > 0 && strToParse.Contains(tmp))
            {
                var currDecSeparator =  CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

                tmp = tmp.Replace(".", currDecSeparator).Replace(decimalSymbol.ToString(), currDecSeparator);

                return double.Parse(tmp);
            }

            return 0;
        }
    }
}
