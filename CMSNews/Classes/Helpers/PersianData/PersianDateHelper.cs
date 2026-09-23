

using System;
using System.Globalization;

namespace CMSNews.Classes.Helpers.PersianDate
{
    public static class PersianDateHelper
    {
        public static string ToPersianDate(
            DateTime date,
            PersianDateFormat format)
        {
            PersianCalendar pc = new PersianCalendar();

            string[] months =
            {
                "فروردین",
                "اردیبهشت",
                "خرداد",
                "تیر",
                "مرداد",
                "شهریور",
                "مهر",
                "آبان",
                "آذر",
                "دی",
                "بهمن",
                "اسفند"
            };

            int year = pc.GetYear(date);
            int month = pc.GetMonth(date);
            int day = pc.GetDayOfMonth(date);

            // حالت اول: 1405/06/29
            if (format == PersianDateFormat.Short)
            {
                return $"{year}/{month:00}/{day:00}";
            }

            // حالت دوم: 29 شهریور 1405
            if (format == PersianDateFormat.Long)
            {
                return $"{day} {months[month - 1]} {year}";
            }

            // حالت سوم: 29 شهریور 1405 - 14:35
            if (format == PersianDateFormat.WithTime)
            {
                return $"{day} {months[month - 1]} {year} - {date:HH:mm}";
            }

            return "";
        }
    }
}






//using System;
//using System.Collections.Generic;
//using System.Globalization;
//using System.Linq;
//using System.Web;

//namespace CMSNews.Classes.Helpers
//{
//    public class PersianDateHelper
//    {
//        public static string ToPersianDate(DateTime date)
//        {
//            PersianCalendar pc = new PersianCalendar();

//            return $"{pc.GetYear(date)}/{pc.GetMonth(date):00}/{pc.GetDayOfMonth(date):00}";
//        }
//    }
//}