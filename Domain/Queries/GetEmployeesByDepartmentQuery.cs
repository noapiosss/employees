using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Contracts.DTO;
using Domain.Base;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Domain.Commands
{
    public class GetEmployeesByDepartmentQuery : IRequest<GetEmployeesByDepartmentQueryResult>
    {
        public int DepartmentId { get; init; }
    }

    public class GetEmployeesByDepartmentQueryResult
    {
        public List<EmployeeDTO> Employees { get; init; }
        public bool Success { get; init; }
        public bool DepartmentExists { get; init; }
    }

    internal class GetEmployeesByDepartmentQueryHandler : BaseHandler<GetEmployeesByDepartmentQuery, GetEmployeesByDepartmentQueryResult>
    {
        private readonly BaseSqlConnection _connection;
        
        public GetEmployeesByDepartmentQueryHandler(BaseSqlConnection connection, ILogger<GetEmployeesByDepartmentQueryHandler> logger) :
            base(logger)
        {
            _connection = connection;
        }

        protected override async Task<GetEmployeesByDepartmentQueryResult> HandleInternal(GetEmployeesByDepartmentQuery request, CancellationToken cancellationToken)
        {
            string departmentExistsQuery =
            $@"
                SELECT EXISTS(SELECT 1 FROM positions WHERE id='{request.DepartmentId}')
            ";

            bool departmentExists = await ExecuteSqlQuery<bool>(_connection, departmentExistsQuery, cancellationToken);

            if (!departmentExists)
            {
                return new()
                {
                    Employees = [],
                    Success = false,
                    DepartmentExists = departmentExists
                };
            }

            string getEmployeesByDepartmentQuery =
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
                FROM department_position
                LEFT JOIN employees ON employees.id=department_position.employee_id
                LEFT JOIN departments ON departments.id=department_position.department_id
                LEFT JOIN positions ON positions.id=department_position.position_id                
                WHERE department_id={request.DepartmentId}
                ORDER BY departments.name
            ";

            List<EmployeeDTO> employees = await ExecuteCollectionSqlQuery<EmployeeDTO>(_connection, getEmployeesByDepartmentQuery, cancellationToken);

            return new()
            {
                Employees = employees,
                Success = false,
                DepartmentExists = true
            };
        }
    }
}