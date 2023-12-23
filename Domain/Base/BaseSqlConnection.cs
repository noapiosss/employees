using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using Npgsql;

namespace Domain.Base
{
    public class BaseSqlConnection : IDisposable
    {
        private readonly string _connectionString;
        private NpgsqlConnection _connection;

        public BaseSqlConnection(string connectionString)
        {
            _connectionString = connectionString;
            _connection = new(_connectionString);
            _connection.Open();
        }

        public async Task Open(CancellationToken cancellationToken)
        {
            await _connection.OpenAsync(cancellationToken);
        }

        public NpgsqlCommand ExecuteCommand(string sqlQuery)
        {
            return new(sqlQuery, _connection);
        }

        public void Dispose()
        {
            if (_connection is not null && _connection.State == ConnectionState.Open)
            {
                _connection.Close();
                _connection.Dispose();
            }
        }
    }
}