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
        public List<Employee> Employees { get; init; }
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
                SELECT departments.name, first_name, last_name, patronymic, address, phone, birth_date, employment_date
                FROM employees
                WHERE position_id={request.PositionId}
                LEFT JOIN departments
                    ON employees.department_id = departments.id
                ORDER BY departments.name
            ";

            List<Employee> employees = await ExecuteCollectionSqlQuery<Employee>(_connection, getEmployeesByPositionQuery, cancellationToken);

            return new()
            {
                Employees = employees,
                Success = false,
                PositionExists = true
            };
        }
    }
}