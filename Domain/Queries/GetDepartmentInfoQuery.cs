using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Contracts.DTO;
using Domain.Base;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Domain.Commands
{
    public class GetDepartmentInfoQuery : IRequest<GetDepartmentInfoQueryResult>
    {
        public int DepartmentId { get; init; }
    }

    public class GetDepartmentInfoQueryResult
    {
        public DepartmentInfoDTO DepartmentInfo { get; init; }
        public bool Success { get; init; }
        public bool DepartmentExists { get; init; }
    }

    internal class GetDepartmentInfoQueryHandler : BaseHandler<GetDepartmentInfoQuery, GetDepartmentInfoQueryResult>
    {
        private readonly BaseSqlConnection _connection;
        
        public GetDepartmentInfoQueryHandler(BaseSqlConnection connection, ILogger<GetDepartmentInfoQueryHandler> logger) :
            base(logger)
        {
            _connection = connection;
        }

        protected override async Task<GetDepartmentInfoQueryResult> HandleInternal(GetDepartmentInfoQuery request, CancellationToken cancellationToken)
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
                    DepartmentInfo = new(),
                    Success = false,
                    DepartmentExists = departmentExists
                };
            }

            string getDepartmentSalaryInfoQuery =
            $@"
                SELECT
                    SUM(employees.salary) as total_salary,
                    MIN(employees.salary) as min_salary,
                    MAX(employees.salary) as max_salary,
                    AVG(employees.salary) as avg_salary
                FROM department_position
                LEFT JOIN employees ON employees.id=department_position.employee_id
                WHERE
                    department_position.employee_id IS NOT NULL AND
                    department_position.department_id={request.DepartmentId}
            ";            
            
            string getDepartmentQuery =
            $@"
                SELECT
                    departments.id as id,
                    departments.name as name
                FROM departments
                WHERE departments.id={request.DepartmentId}
            ";

            string getDepartmentEmployeesQuery =
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
                    positions.name as position_name
                FROM department_position
                JOIN employees ON department_position.employee_id=employees.id
                JOIN positions ON department_position.position_id=positions.id
                JOIN departments ON department_position.department_id=departments.id                
                WHERE
                    department_position.employee_id IS NOT NULL AND
                    department_position.department_id={request.DepartmentId}
            ";

            DepartmentDTO departmentDTO = await ExecuteSqlQuery<DepartmentDTO>(_connection, getDepartmentQuery, cancellationToken);
            DepartmentInfoDTO departmentInfo = await ExecuteSqlQuery<DepartmentInfoDTO>(_connection, getDepartmentSalaryInfoQuery, cancellationToken);
            departmentInfo.Id = departmentDTO.Id;
            departmentInfo.Name = departmentDTO.Name;
            departmentInfo.Eployees = await ExecuteCollectionSqlQuery<EmployeeDTO>(_connection, getDepartmentEmployeesQuery, cancellationToken);

            return new()
            {
                DepartmentInfo = departmentInfo,
                Success = true,
                DepartmentExists = true
            };
        }
    }
}