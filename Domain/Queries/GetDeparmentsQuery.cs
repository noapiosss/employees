using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Contracts.Database;
using Domain.Base;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Domain.Commands
{
    public class GetDepartmentsQuery : IRequest<GetDepartmentsQueryResult>
    {
    }

    public class GetDepartmentsQueryResult
    {
        public List<Department> Departments { get; init; }
    }

    internal class GetDepartmentsQueryHandler : BaseHandler<GetDepartmentsQuery, GetDepartmentsQueryResult>
    {
        private readonly BaseSqlConnection _connection;
        
        public GetDepartmentsQueryHandler(BaseSqlConnection connection, ILogger<GetDepartmentsQueryHandler> logger) :
            base(logger)
        {
            _connection = connection;
        }

        protected override async Task<GetDepartmentsQueryResult> HandleInternal(GetDepartmentsQuery request, CancellationToken cancellationToken)
        {
            string getDepartmentsQuery =
            $@"
                SELECT *
                FROM departments
            ";

            List<Department> departments = await ExecuteCollectionSqlQuery<Department>(_connection, getDepartmentsQuery, cancellationToken);

            return new()
            {
                Departments = departments
            };
        }
    }
}