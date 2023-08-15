using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FC.AbreuBarber.EndToEndTests.Extensions.DateTime
{
    internal static class DateTimeExtensions
    {
        public static System.DateTime TrimMillisseconds(
            this System.DateTime dateTime
        )
        {
            return new System.DateTime(
                dateTime.Year,
                dateTime.Month,
                dateTime.Day,
                dateTime.Hour,
                dateTime.Minute,
                dateTime.Second,
                0,
                dateTime.Kind
            );
        }
    }
}
