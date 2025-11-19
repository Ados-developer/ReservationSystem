using System.Globalization;

namespace ReservationSystem.Util
{
    public class PriceUtil
    {
        public static string GetPrice(decimal price)
        {
            return NumberToEditorString_2dc(price);
        }
        public static string GetPriceString(decimal price)
        {
            return GetPriceString(NumberToEditorString(price));
        }
        public static string GetPriceString(string price)
        {
            return string.Format("{0} €", price);
        }

        public static string GetPercString(decimal price)
        {
            return string.Format("{0}%", NumberToEditorString(price));
        }

        public static string IntNumberToEditorString(int num)
        {
            return num.ToString();
        }
        public static int IntNumberFromEditorString(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return 0;
            }

            int num = 0;

            if (!int.TryParse(str, out num))
            {
                num = 0;
            }

            return num;
        }

        public static string NumberToEditorString(decimal price)
        {
            string ret = price.ToString(NumberFormatInfo.InvariantInfo).Replace(".", ",");
            if (ret.EndsWith(",00"))
            {
                ret = ret.Substring(0, ret.LastIndexOf(",00"));
            }

            return ret;
        }

        public static string NumberToEditorString_2dc(decimal price)
        {
            string ret = price.ToString("F2").Replace(".", ",");

            return ret;
        }
        public static string NumberToEditorString_0dc(decimal price)
        {
            int roundedPrice = (int)Math.Round(price);
            return roundedPrice.ToString();
        }

        public static decimal NumberFromEditorString(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return 0M;
            }

            decimal price = 0M;

            if (!decimal.TryParse(str.Replace(",", "."), NumberStyles.Any, NumberFormatInfo.InvariantInfo, out price))
            {
                price = 0M;
            }

            return price;
        }
        public static string FormatDecimalString(string input)
        {
            if (decimal.TryParse(input, out decimal result))
            {
                if (result % 1 == 0)
                {
                    return result.ToString("0");
                }
                else
                {
                    return result.ToString("0.00");
                }
            }
            else
            {
                return input;
            }
        }




        public const string DiscountPercRegex = @"^(?:100(?:,0{1,2})?|\d{1,2}(?:,\d{1,2})?)$";

    }
}
