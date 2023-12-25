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
    public class GetAllEmployeesQuery : IRequest<GetAllEmployeesQueryResult>
    {
        public int Page { get; init; }
        public int PerPage { get; init; }
    }

    public class GetAllEmployeesQueryResult
    {
        public ICollection<EmployeeDTO> Employees { get; init; }
        public int PagesCount { get; init; }
        public int Page { get; init; }
        public int PerPage { get; init; }
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
                ORDER BY employees.first_name
                OFFSET {(request.Page - 1) * request.PerPage}
                LIMIT {request.PerPage}
            ";

            List<EmployeeDTO> employees = await ExecuteCollectionSqlQuery<EmployeeDTO>(_connection, getAllEmployeesQuery, cancellationToken);

            string getEmployeesCountQuery = 
            $@"
                SELECT COUNT(*)
                FROM employees
            ";

            int employeesCount = await ExecuteSqlQuery<int>(_connection, getEmployeesCountQuery, cancellationToken);
            int pagesCount = employeesCount / request.PerPage;
            if (employeesCount % request.PerPage > 0)
            {
                ++pagesCount;
            }

            return new()
            {
                Employees = employees,
                Page = request.Page,
                PerPage = request.PerPage,
                PagesCount = pagesCount
            };
        }
    }
}