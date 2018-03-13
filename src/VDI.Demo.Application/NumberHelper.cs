using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace VDI.Demo
{
    internal static class NumberHelper
    {
        internal static string IndoFormat(decimal num)
        {
            var numberFormatInfo = new NumberFormatInfo();
            numberFormatInfo.NumberDecimalSeparator = ",";
            numberFormatInfo.NumberGroupSeparator = ".";
            return num.ToString("N", numberFormatInfo);
        }

        internal static string Terbilang(this decimal y)
        {
            return TerbilangCore(y);
        }

        internal static string Terbilang(this decimal? y)
        {
            return TerbilangCore(y);
        }

        private static string TerbilangCore(this decimal? y)
        {
            string[] bilangan = { "", "Satu", "Dua", "Tiga", "Empat", "Lima", "Enam", "Tujuh", "Delapan", "Sembilan", "Sepuluh", "Sebelas" };
            string temp = "";

            if (y == null)
            {
                return "-";
            }

            long x = Convert.ToInt64(y);

            if (x < 12)
            {
                temp = " " + bilangan[x];
            }
            else if (x < 20)
            {
                temp = Terbilang(x - 10).ToString() + " Belas";
            }
            else if (x < 100)
            {
                temp = Terbilang(x / 10) + " Puluh" + Terbilang(x % 10);
            }
            else if (x < 200)
            {
                temp = " Seratus" + Terbilang(x - 100);
            }
            else if (x < 1000)
            {
                temp = Terbilang(x / 100) + " Ratus" + Terbilang(x % 100);
            }
            else if (x < 2000)
            {
                temp = " Seribu" + Terbilang(x - 1000);
            }
            else if (x < 1000000)
            {
                temp = Terbilang(x / 1000) + " Ribu" + Terbilang(x % 1000);
            }
            else if (x < 1000000000)
            {
                temp = Terbilang(x / 1000000) + " Juta" + Terbilang(x % 1000000);
            }
            else if (x < 1000000000000)
            {
                temp = Terbilang(x / 1000000000) + " Miliar" + Terbilang(x % 1000000000);
            }
            else if (x < 1000000000000000)
            {
                temp = Terbilang(x / 1000000000000) + " Triliun" + Terbilang(x % 1000000000000);
            }

            return temp;
        }
    }
}
