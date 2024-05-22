using UnityEngine;
using UnityEngine.UI;

namespace Utilities
{
    public static class GameUtility
    {
        public static void ChangeAlphaImage(Image image, float alpha)
        {
            Color c = image.color;
            c.a = alpha;
            image.color = c;
        }
        public static string FormatFloatToReadableString(float value, bool isDecimal = false, bool isTwoDigit = false)
        {
            float number = value;
            bool hasDecimalPart = (number % 1) != 0;

            if(number < 100f && isDecimal && hasDecimalPart)
            {
                return number.ToString(isTwoDigit ? "0.0" : "0.00").Replace(",",".");
            }
            if (number < 1000)
            {
                return ((int)number).ToString();
            }
            string result = ((int)number).ToString();
            if (result.Contains(","))
            {
                result = result.Substring(0, 4);
                result = result.Replace(",", string.Empty);
            }
            else
            {
                result = result.Substring(0, 3);
            }
            do
            {
                number /= 1000;
            }
            while (number >= 1000);

            number = (int)number;
            if (value >= 1000000000000000)
            {
                result = result + "Q";
            }
            else if (value >= 1000000000000)
            {
                result = result + "T";
            }
            else if (value >= 1000000000)
            {
                result = result + "B";
            }
            else if (value >= 1000000)
            {
                result = result + "M";
            }
            else if (value >= 1000)
            {
                result = result + "K";
            }
            if (((int)number).ToString().Length > 0 && ((int)number).ToString().Length < 3)
            {
                result = result.Insert(((int)number).ToString().Length, ".");
            }
            return result;
        }
    }
}