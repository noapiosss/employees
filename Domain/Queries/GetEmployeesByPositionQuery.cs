using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Contracts.Database;
using Domain.Base;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Domain.Commands
{
    public class GetEmployeesByPositionQuery : IRequest<GetEmployeesByPositionQueryResult>
    {
        public int PositionId { get; init; }
    }

    public class GetEmployeesByPositionQueryResult
    {
        public List<EmployeeDTO> Employees { get; init; }
        public bool Success { get; init; }
        public bool PositionExists { get; init; }
    }

    internal class GetEmployeesByPositionQueryHandler : BaseHandler<GetEmployeesByPositionQuery, GetEmployeesByPositionQueryResult>
    {
        private readonly BaseSqlConnection _connection;
        
        public GetEmployeesByPositionQueryHandler(BaseSqlConnection connection, ILogger<GetEmployeesByPositionQueryHandler> logger) :
            base(logger)
        {
            _connection = connection;
        }

        protected override async Task<GetEmployeesByPositionQueryResult> HandleInternal(GetEmployeesByPositionQuery request, CancellationToken cancellationToken)
        {
            string positionExistsQuery =
            $@"
                SELECT EXISTS(SELECT 1 FROM positions WHERE id='{request.PositionId}')
            ";

            bool positionExists = await ExecuteSqlQuery<bool>(_connection, positionExistsQuery, cancellationToken);

            if (!positionExists)
            {
                return new()
                {
                    Employees = [],
                    Success = false,
                    PositionExists = positionExists
                };
            }


            string getEmployeesByPositionQuery =
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
                WHERE position_id={request.PositionId}
                ORDER BY departments.name
            ";

            List<EmployeeDTO> employees = await ExecuteCollectionSqlQuery<EmployeeDTO>(_connection, getEmployeesByPositionQuery, cancellationToken);

            return new()
            {
                Employees = employees,
                Success = false,
                PositionExists = true
            };
        }
    }
}