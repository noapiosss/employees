using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Contracts.Database;
using Contracts.Http;
using Domain.Base;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Domain.Commands
{
    public class GetFilteredEmployeesQuery : IRequest<GetFilteredEmployeesQueryResult>
    {
        public int DepartmentId { get; init; }
        public int PositionId { get; init; }
        public BoundFilterValues BoundFilterValues { get; init; }
        public string SearchRequest { get; init; }
    }

    public class GetFilteredEmployeesQueryResult
    {
        public ICollection<EmployeeDTO> Employees { get; init; }
    }

    internal class GetFilteredEmployeesQueryHandler : BaseHandler<GetFilteredEmployeesQuery, GetFilteredEmployeesQueryResult>
    {
        private readonly BaseSqlConnection _connection;
        
        public GetFilteredEmployeesQueryHandler(BaseSqlConnection connection, ILogger<GetFilteredEmployeesQueryHandler> logger) :
            base(logger)
        {
            _connection = connection;
        }

        protected override async Task<GetFilteredEmployeesQueryResult> HandleInternal(GetFilteredEmployeesQuery request, CancellationToken cancellationToken)
        {
            DateOnly minBirthDate = DateOnly.FromDateTime(DateTime.Now.AddYears(-request.BoundFilterValues.MaxAge));
            DateOnly maxBirthDate = DateOnly.FromDateTime(DateTime.Now.AddYears(-request.BoundFilterValues.MinAge));

            string getFilteredEmployeesQuery = 
            @$"
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
                FROM department_position
                JOIN employees ON department_position.employee_id=employees.id
                JOIN positions ON department_position.position_id=positions.id
                JOIN departments ON department_position.department_id=departments.id                
                WHERE
                    department_position.employee_id is NOT NULL AND
                    employees.employment_date>='{DateOnly.FromDateTime(request.BoundFilterValues.MinEmploymentDate):yyyy-MM-dd}' AND
                    employees.employment_date<='{DateOnly.FromDateTime(request.BoundFilterValues.MaxEmploymentDate):yyyy-MM-dd}' AND
                    employees.birth_date>='{minBirthDate:yyyy-MM-dd}' AND
                    employees.birth_date<='{maxBirthDate:yyyy-MM-dd}' AND
                    employees.salary>={request.BoundFilterValues.MinSalary} AND
                    employees.salary<={request.BoundFilterValues.MaxSalary}
            ";

            if (request.DepartmentId != 0)
            {
                getFilteredEmployeesQuery += $" AND\n department_position.department_id={request.DepartmentId}";
            }

            if (request.PositionId !=0)
            {
                getFilteredEmployeesQuery += $" AND\n department_position.position_id={request.PositionId}";
            }

            if (!string.IsNullOrWhiteSpace(request.SearchRequest))
            {
                getFilteredEmployeesQuery +=
                $@"
                    AND
                    (
                        employees.first_name LIKE '%{request.SearchRequest}%' OR
                        employees.last_name LIKE '%{request.SearchRequest}%' OR
                        employees.patronymic LIKE '%{request.SearchRequest}%' OR
                        employees.address LIKE '%{request.SearchRequest}%' OR
                        employees.phone LIKE '%{request.SearchRequest}%'
                    )";
            }

            List<EmployeeDTO> employees = await ExecuteCollectionSqlQuery<EmployeeDTO>(_connection, getFilteredEmployeesQuery, cancellationToken);

            return new()
            {
                Employees = employees
            };
        }
    }
}