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
        public List<Employee> Employees { get; init; }
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
                SELECT *
                FROM emplyees
            ";

            List<Employee> employees = await ExecuteCollectionSqlQuery<Employee>(_connection, getAllEmployeesQuery, cancellationToken);

            return new()
            {
                Employees = employees
            };
        }
    }
}