using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Domain.Base;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Domain.Commands
{
    public class GenerateTestDataCommand : IRequest<GenerateTestDataCommandResult>
    {

    }

    public class GenerateTestDataCommandResult
    {

    }

    internal class GenerateTestDataCommandHandler : BaseHandler<GenerateTestDataCommand, GenerateTestDataCommandResult>
    {
        private readonly BaseSqlConnection _connection;
        private readonly Random _random;
        
        public GenerateTestDataCommandHandler(BaseSqlConnection connection, ILogger<GenerateTestDataCommandHandler> logger) :
            base(logger)
        {
            _connection = connection;
            _random = new();
        }

        protected override async Task<GenerateTestDataCommandResult> HandleInternal(GenerateTestDataCommand request, CancellationToken cancellationToken)
        {
            string createDeparmentsQuery =
            @$"
                INSERT INTO departments(name)
                VALUES
                    ('Sales'),
                    ('HR'),
                    ('Accounting'),
                    ('Infrastructures'),
                    ('Marketing'),
                    ('Product Development'),
                    ('Administration Department')
                RETURNING id;
            ";

            List<int> departmentIds = await ExecuteCollectionSqlQuery<int>(_connection, createDeparmentsQuery, cancellationToken);

            string createPositionsQuery =
            @$"
                INSERT INTO positions(name)
                VALUES
                    ('Executive'),
                    ('Manager'),
                    ('Accountant'),
                    ('Customer service'),
                    ('Sales'),
                    ('Advisor'),
                    ('Developer'),
                    ('President')
                RETURNING id;
            ";

            List<int> positionIds = await ExecuteCollectionSqlQuery<int>(_connection, createPositionsQuery, cancellationToken);

            StringBuilder sb = new();
            int employeesCount = departmentIds.Count * positionIds.Count;

            sb.AppendLine("INSERT INTO employees(first_name, last_name, patronymic, address, phone, birth_date, employment_date, salary)");
            sb.AppendLine("VALUES");
            for(int i = 0; i < employeesCount; ++i)
            {
                string firstName = Guid.NewGuid().ToString();
                string lastName = Guid.NewGuid().ToString();
                string patronymic = Guid.NewGuid().ToString();
                string address = Guid.NewGuid().ToString();
                string phone = Guid.NewGuid().ToString();
                DateOnly birthDate = new(_random.Next(1980,2000), _random.Next(1,12), _random.Next(1,25));
                DateOnly employmentDate = new(_random.Next(1980,2000), _random.Next(1,12), _random.Next(1,25));
                decimal salary = (decimal)_random.NextSingle()*_random.Next(10000,100000);

                sb.Append($"('{firstName}', '{lastName}', '{patronymic}', '{address}', '{phone}', '{birthDate:yyyy-MM-dd}', '{employmentDate:yyyy-MM-dd}', {salary})");

                if (i != employeesCount - 1)
                {
                    sb.AppendLine(",");
                }
            }            
            sb.AppendLine("RETURNING id;");

            string createEmployeesQuery = sb.ToString();
            List<int> employeeIds = await ExecuteCollectionSqlQuery<int>(_connection, createEmployeesQuery, cancellationToken);



            sb.Clear();
            sb.AppendLine("INSERT INTO department_position(department_id, position_id, employee_id)");
            sb.AppendLine("VALUES");

            int employeeId = 1;
            for(int i = 0; i < departmentIds.Count; ++i)
            {
                for(int j = 0; j < positionIds.Count; ++j)
                {
                    sb.Append("(");
                    sb.Append($"(SELECT id FROM departments WHERE departments.id={departmentIds[i]}),");
                    sb.Append($"(SELECT id FROM positions WHERE positions.id={positionIds[j]}),");
                    sb.Append($"(SELECT id FROM employees WHERE employees.id={employeeId++})");
                    sb.Append(")");

                    if (i != departmentIds.Count - 1 || j != positionIds.Count - 1)
                    {
                        sb.AppendLine(",");
                    }
                }
            }
            sb.Append(';');

            string createDeparmentPositionQuery = sb.ToString();
            await ExecuteSqlQuery(_connection, createDeparmentPositionQuery, cancellationToken);

            return new();
        }
    }
}