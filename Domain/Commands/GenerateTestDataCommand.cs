using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Domain.Base;
using MediatR;
using Microsoft.Extensions.Logging;
using PersonGenerator;

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
            int employeesCount = 200;

            PersonGenerator.GeneratorSettings settings = new GeneratorSettings()
            {
                Language = Languages.English,
                FirstName = true,
                MiddleName = true,
                LastName = true,
                Email = true
            };
            PersonGenerator.PersonGenerator personGenerator = new PersonGenerator.PersonGenerator(settings);
            List<Person> p = personGenerator.Generate(employeesCount);

            sb.AppendLine("INSERT INTO employees(first_name, last_name, patronymic, address, phone, birth_date, employment_date, salary)");
            sb.AppendLine("VALUES");
            for(int i = 0; i < employeesCount; ++i)
            {
                

                string firstName = p[i].FirstName;
                string lastName = p[i].LastName;
                string patronymic = p[i].MiddleName;
                string address = p[i].Email;
                string phone = $"044-{_random.Next(100,999)}-{_random.Next(10,99)}-{_random.Next(10,99)}";
                DateOnly birthDate = new(_random.Next(1980,2000), _random.Next(1,12), _random.Next(1,25));
                DateOnly employmentDate = new(_random.Next(1980,2000), _random.Next(1,12), _random.Next(1,25));
                decimal salary = (decimal)Math.Round(_random.NextSingle()*_random.Next(10000,100000), 2);

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

            for(int i = 0; i < employeeIds.Count; ++i)
            {
                sb.Append("(");
                    sb.Append($"(SELECT id FROM departments WHERE departments.id={departmentIds[_random.Next(departmentIds.Count)]}),");
                    sb.Append($"(SELECT id FROM positions WHERE positions.id={positionIds[_random.Next(positionIds.Count)]}),");
                    sb.Append($"(SELECT id FROM employees WHERE employees.id={employeeIds[i]})");
                    sb.Append(")");

                    if (i != employeeIds.Count - 1)
                    {
                        sb.AppendLine(",");
                    }
            }
            sb.Append(';');

            string createDeparmentPositionQuery = sb.ToString();
            await ExecuteSqlQuery(_connection, createDeparmentPositionQuery, cancellationToken);

            return new();
        }
    }
}