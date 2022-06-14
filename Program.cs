using System;
using System.Text.RegularExpressions;

namespace CustomDateAddition
{
    class Program
    {
        static int m2, d2;

        static void Main(string[] args)
        {
            Regex regex = new Regex(@"(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((18|19|20)\d\d))$");
            Console.WriteLine("Please enter the date in dd/MM/yyyy format :");
            string inputDate = Console.ReadLine();
            bool isValid = regex.IsMatch(inputDate.Trim());

            if (!isValid)
            {
                Console.WriteLine("Oops you entered the date in wrong format!...Please enter in given format ex: 03/05/2015 instead of 3/5/2015.");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Please enter the days that you want to add :");
                int days = Convert.ToInt32(Console.ReadLine());
                string[] splitStr = inputDate.Split('/');
                int[] splitDate = Array.ConvertAll(splitStr, s => int.Parse(s));
                AddDays(splitDate[0], splitDate[1], splitDate[2], days);
            }
        }

        static bool IsLeapYear(int y)
        {
            if (y % 100 != 0 && y % 4 == 0 || y % 400 == 0)
                return true;

            return false;
        }

        static int OffsetDays(int d, int m, int y)
        {
            int offset = d;

            if (m - 1 == 11)
                offset += 335;
            if (m - 1 == 10)
                offset += 304;
            if (m - 1 == 9)
                offset += 273;
            if (m - 1 == 8)
                offset += 243;
            if (m - 1 == 7)
                offset += 212;
            if (m - 1 == 6)
                offset += 181;
            if (m - 1 == 5)
                offset += 151;
            if (m - 1 == 4)
                offset += 120;
            if (m - 1 == 3)
                offset += 90;
            if (m - 1 == 2)
                offset += 59;
            if (m - 1 == 1)
                offset += 31;

            if (IsLeapYear(y) && m > 2)
                offset += 1;

            return offset;
        }

        static void RevoffsetDays(int offset, int y)
        {
            int[] month = { 0, 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

            if (IsLeapYear(y))
                month[2] = 29;
            int i;
            for (i = 1; i <= 12; i++)
            {
                if (offset <= month[i])
                    break;
                offset = offset - month[i];
            }

            d2 = offset;
            m2 = i;
        }

        static void AddDays(int d1, int m1, int y1, int x)
        {
            int offset1 = OffsetDays(d1, m1, y1);
            int remDays = IsLeapYear(y1) ? (366 - offset1) : (365 - offset1);

            int y2, offset2 = 0;
            if (x <= remDays)
            {
                y2 = y1;
                offset2 = offset1 + x;
            }

            else
            {
                x -= remDays;
                y2 = y1 + 1;
                int y2days = IsLeapYear(y2) ? 366 : 365;
                while (x >= y2days)
                {
                    x -= y2days;
                    y2++;
                    y2days = IsLeapYear(y2) ? 366 : 365;
                }
                offset2 = x;
            }
            RevoffsetDays(offset2, y2);
            if (m2 == 12 || d2 == 31)
            {
                d2--;
            }
            
            Console.WriteLine(d2 + "/" + m2 + "/" + y2);
            Console.ReadLine();
        }
    }
}
