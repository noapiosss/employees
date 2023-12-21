using System;
using Domain.Helpers.Interfaces;

namespace Domain.Helpers
{
    public class DateTimeHelper : IDateTimeHelper
    {
        public string DateTimeToPostgresDate(DateTime dateTime)
        {
            string month = dateTime.Month / 10 > 0 ? dateTime.Month.ToString() : $"0{dateTime.Month}";
            string day = dateTime.Day / 10 > 0 ? dateTime.Day.ToString() : $"0{dateTime.Day}";
            return $"{dateTime.Year}-{month}-{day}";
        }
    }
}