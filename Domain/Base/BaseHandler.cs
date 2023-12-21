using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace Domain.Base
{
    internal abstract class BaseHandler<TRequest, TResult> : IRequestHandler<TRequest, TResult>
        where TRequest : IRequest<TResult>
    {
        protected readonly ILogger Logger;
        private readonly string _name;

        protected BaseHandler(ILogger logger)
        {
            Logger = logger;
            _name = GetType().Name;
        }

        public async Task<TResult> Handle(TRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Logger.LogDebug($"Start to execute {_name}. Input: {request}");

                TResult result = await HandleInternal(request, cancellationToken);

                Logger.LogDebug($"Executed {_name}. Output: {result}");

                return result;
            }
            catch (Exception e)
            {
                Logger.LogError(e, $"Exception raised. Handler: {_name}. Request: {request}");
                throw;
            }
        }

        protected async Task<T> ExecuteSqlQuery<T>(BaseSqlConnection connection, string sqlQuery, CancellationToken cancellationToken)
        {
            using NpgsqlCommand command = connection.ExecuteCommand(sqlQuery);
            using NpgsqlDataReader reader = await command.ExecuteReaderAsync(cancellationToken);

            return ConvertToObject<T>(reader);
        }

        protected async Task<List<T>> ExecuteCollectionSqlQuery<T>(BaseSqlConnection connection, string sqlQuery, CancellationToken cancellationToken)
        {
            using NpgsqlCommand command = connection.ExecuteCommand(sqlQuery);
            using NpgsqlDataReader reader = await command.ExecuteReaderAsync(cancellationToken);

            return ConvertToList<T>(reader);
        }

        private static List<T> ConvertToList<T>(NpgsqlDataReader rd)
        {
            List<T> result = new();

            while (rd.Read())
            {
                T obj = ConvertToObject<T>(rd);
                result.Add(obj);
            }

            return result;
        }

        private static T ConvertToObject<T> (NpgsqlDataReader rd)
        {
            T obj = Activator.CreateInstance<T>();
            Type type = typeof(T);

            if (type.IsValueType)
            {
                rd.Read();
                return rd.GetFieldValue<T>(0);
            }

            PropertyInfo[] properties = type.GetProperties();

            for(int i = 0; i < rd.FieldCount; ++i)
            {
                string propertyName = rd.GetName(i).Replace("_", "");
                object propertyValue = rd.GetValue(i);

                PropertyInfo propertyInfo = Array.Find(properties, p => p.Name.Equals(propertyName, StringComparison.CurrentCultureIgnoreCase));
                if (propertyInfo is not null && propertyValue != DBNull.Value)
                {
                    Type propertyType = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;

                    object value;
                    if (propertyType == typeof(Guid))
                    {
                        value = Guid.Parse(propertyValue.ToString());;
                    }
                    else
                    {
                        value = Convert.ChangeType(propertyValue, propertyType);
                    }
                    
                    propertyInfo.SetValue(obj, value);
                }
            }

            return obj;
        }

        protected abstract Task<TResult> HandleInternal(TRequest request, CancellationToken cancellationToken);
    }
}