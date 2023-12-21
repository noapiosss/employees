using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Contracts.Database;
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
        public List<Employee> Employees { get; init; }
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
                SELECT position.name, first_name, last_name, patronymic, address, phone, birth_date, employment_date
                FROM employees
                WHERE department_id={request.DepartmentId}
                LEFT JOIN positions
                    ON employees.position_id = positions.id
                ORDER BY position.name
            ";

            List<Employee> employees = await ExecuteCollectionSqlQuery<Employee>(_connection, getEmployeesByDepartmentQuery, cancellationToken);

            return new()
            {
                Employees = employees,
                Success = false,
                DepartmentExists = true
            };
        }
    }
}