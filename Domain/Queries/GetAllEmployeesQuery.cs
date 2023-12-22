using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Contracts.Database;
using Domain.Base;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Domain.Commands
{
    public class GetAllEmployeesQuery : IRequest<GetAllEmployeesQueryResult>
    {
    }

    public class GetAllEmployeesQueryResult
    {
        public ICollection<EmployeeDTO> Employees { get; init; }
    }

    internal class GetAllEmployeesQueryHandler : BaseHandler<GetAllEmployeesQuery, GetAllEmployeesQueryResult>
    {
        private readonly BaseSqlConnection _connection;
        
        public GetAllEmployeesQueryHandler(BaseSqlConnection connection, ILogger<GetAllEmployeesQueryHandler> logger) :
            base(logger)
        {
            _connection = connection;
        }

        protected override async Task<GetAllEmployeesQueryResult> HandleInternal(GetAllEmployeesQuery request, CancellationToken cancellationToken)
        {
            string getAllEmployeesQuery = 
            $@"
                SELECT
                    employees.id as id,
                    employees.first_name as first_name,
                    employees.last_name as last_name,
                    employyes.patronymic as patronymic,
                    employyes.address as address,
                    employyes.phone as phone,
                    employyes.birth_date as birth_date,
                    employyes.employment_date as employment_date,
                    employyes.salary as salary,
                    departments.name as department_name,
                    positions.name as position_name
                FROM employees
                LEFT JOIN departments_position ON departments_position.id=employees.department_position_id
                LEFT JOIN departments ON departments.id=departments_position.department_id
                LEFT JOIN positions ON positions.id=departments_position.position_id
            ";

            List<EmployeeDTO> employees = await ExecuteCollectionSqlQuery<EmployeeDTO>(_connection, getAllEmployeesQuery, cancellationToken);

            return new()
            {
                Employees = employees
            };
        }
    }
}