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
                    employees.patronymic as patronymic,
                    employees.address as address,
                    employees.phone as phone,
                    employees.birth_date as birth_date,
                    employees.employment_date as employment_date,
                    employees.salary as salary,
                    departments.name as department_name,
                    positions.name as position_name
                FROM employees
                LEFT JOIN department_position ON department_position.employee_id=employees.id
                LEFT JOIN departments ON departments.id=department_position.department_id
                LEFT JOIN positions ON positions.id=department_position.position_id
            ";

            List<EmployeeDTO> employees = await ExecuteCollectionSqlQuery<EmployeeDTO>(_connection, getAllEmployeesQuery, cancellationToken);

            return new()
            {
                Employees = employees
            };
        }
    }
}