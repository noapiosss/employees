using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Contracts.Database;
using Domain.Base;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Domain.Commands
{
    public class GetPositionsQuery : IRequest<GetPositionsQueryResult>
    {
    }

    public class GetPositionsQueryResult
    {
        public List<Department> Positions { get; init; }
    }

    internal class GetPositionsQueryHandler : BaseHandler<GetPositionsQuery, GetPositionsQueryResult>
    {
        private readonly BaseSqlConnection _connection;
        
        public GetPositionsQueryHandler(BaseSqlConnection connection, ILogger<GetPositionsQueryHandler> logger) :
            base(logger)
        {
            _connection = connection;
        }

        protected override async Task<GetPositionsQueryResult> HandleInternal(GetPositionsQuery request, CancellationToken cancellationToken)
        {
            string getDepartmentsCommand =
            $@"
                SELECT *
                FROM positions
            ";

            List<Department> positions = await ExecuteCollectionSqlQuery<Department>(_connection, getDepartmentsCommand, cancellationToken);

            return new()
            {
                Positions = positions
            };
        }
    }
}