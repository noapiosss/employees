using System;

namespace Domain.Helpers.Interfaces
{
    public interface IDateTimeHelper
    {
        public string DateTimeToPostresDate(DateTime dateTime);
    }
}