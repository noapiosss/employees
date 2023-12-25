using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Contracts.DTO;
using Domain.Base;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Domain.Commands
{
    public class GetEmployeeByIdQuery : IRequest<GetEmployeeByIdQueryResult>
    {
        public int Id { get; init; }
    }

    public class GetEmployeeByIdQueryResult
    {
        public EmployeeDTO Employee { get; init; }
        public bool Success { get; init; }
        public bool EmployeeExists { get; init; }
    }

    internal class GetEmployeeByIdQueryHandler : BaseHandler<GetEmployeeByIdQuery, GetEmployeeByIdQueryResult>
    {
        private readonly BaseSqlConnection _connection;
        
        public GetEmployeeByIdQueryHandler(BaseSqlConnection connection, ILogger<GetEmployeeByIdQueryHandler> logger) :
            base(logger)
        {
            _connection = connection;
        }

        protected override async Task<GetEmployeeByIdQueryResult> HandleInternal(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            string employeeExistsQuery =
            $@"
                SELECT EXISTS(SELECT 1 FROM employees WHERE id='{request.Id}')
            ";

            bool employeeExists = await ExecuteSqlQuery<bool>(_connection, employeeExistsQuery, cancellationToken);

            if (!employeeExists)
            {
                return new()
                {
                    Employee = new(),
                    Success = false,
                    EmployeeExists = employeeExists
                };
            }

            string getEmployeeByIdQuery = 
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
                WHERE employees.id={request.Id}
            ";

            EmployeeDTO employee = await ExecuteSqlQuery<EmployeeDTO>(_connection, getEmployeeByIdQuery, cancellationToken);

            return new()
            {
                Employee = employee,
                Success = true,
                EmployeeExists = true
            };
        }
    }
}