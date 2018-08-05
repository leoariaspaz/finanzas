using Finanzas.Models;
using System;
using System.Linq;

namespace Finanzas.Lib
{
    public static class Configuration
    {
        private static DateTime _currentDate;
        public static DateTime CurrentDate
        {
            get
            {
                using (var db = new GastosEntities())
                {
                    if (_currentDate == DateTime.MinValue)
                    {
                        var dQuery = db.Database.SqlQuery<DateTime>("SELECT GETDATE()");
                        _currentDate = dQuery.AsEnumerable().First();
                    }
                    return _currentDate;
                }
            }
        }
    }
}
