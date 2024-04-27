using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Extensions
{
    public static class DateTimeExtensions
    {
        public static int GetAge(this DateTime? BirthdateTime)
        {
            if (BirthdateTime==null)
            {
                return 0;
            }
            
            var today = DateTime.Today;

            DateTime BirthdateTimeval = BirthdateTime.Value;
            
            var age = today.Year - BirthdateTimeval.Year;

          // Go back to the year in which the person was born in case of a leap year
           if (BirthdateTimeval.Date > today.AddYears(-age)) age--;
           return age;
        }
    }
}