using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Contracts.DTO;
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
        public List<PositionDTO> Positions { get; init; }
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
            string getDepartmentsQuery =
            $@"
                SELECT *
                FROM positions
            ";

            List<PositionDTO> positions = await ExecuteCollectionSqlQuery<PositionDTO>(_connection, getDepartmentsQuery, cancellationToken);

            return new()
            {
                Positions = positions
            };
        }
    }
}