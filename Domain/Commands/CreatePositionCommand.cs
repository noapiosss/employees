using System.Threading;
using System.Threading.Tasks;
using Domain.Base;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Domain.Commands
{
    public class CreatePositionCommand : IRequest<CreatePositionCommandResult>
    {
        public string Name { get; init; }
    }

    public class CreatePositionCommandResult
    {
        public int PositionId { get; init; }
        public bool Success { get; init; }
        public bool PositionAlreadyExists { get; init; }
    }

    internal class CreatePositionCommandHandler : BaseHandler<CreatePositionCommand, CreatePositionCommandResult>
    {
        private readonly BaseSqlConnection _connection;
        
        public CreatePositionCommandHandler(BaseSqlConnection connection, ILogger<CreatePositionCommandHandler> logger) :
            base(logger)
        {
            _connection = connection;
        }

        protected override async Task<CreatePositionCommandResult> HandleInternal(CreatePositionCommand request, CancellationToken cancellationToken)
        {
            string deparmentAlreadyExistsQuery =
            $@"
                SELECT EXISTS(SELECT 1 FROM positions WHERE name='{request.Name}')
            ";

            bool deparmentAlreadyExists = await ExecuteSqlQuery<bool>(_connection, deparmentAlreadyExistsQuery, cancellationToken);

            if (deparmentAlreadyExists)
            {
                return new()
                {
                    PositionId = -1,
                    Success = false,
                    PositionAlreadyExists = true
                };
            }

            string createPositionQuery = 
            $@"
                INSERT INTO positions (name)
                VALUES ('{request.Name}')
                RETURNING id
            ";

            int id = await ExecuteSqlQuery<int>(_connection, createPositionQuery, cancellationToken);

            return new()
            {
                PositionId = id,
                Success = true,
                PositionAlreadyExists = false
            };
        }
    }
}