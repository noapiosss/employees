using System;

namespace Domain.Helpers.Interfaces
{
    public interface IDateTimeHelper
    {
        public string DateTimeToPostgresDate(DateTime dateTime);
    }
}