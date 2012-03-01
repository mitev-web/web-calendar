using System;
using System.Linq;

namespace WebCalendar.DAL
{
    public class DAO
    {
        public static WebCalendarEntities db;

        static DAO()
        {
            db = new WebCalendarEntities();
        }
    }
}