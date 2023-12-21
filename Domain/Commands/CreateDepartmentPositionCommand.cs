using System.Threading;
using System.Threading.Tasks;
using Domain.Base;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Domain.Commands
{
    public class CreateDepartmentPositionCommand : IRequest<CreateDepartmentPositionCommandResult>
    {
        public int DepartmentId { get; init; }
        public int PositionId { get; init; }
    }

    public class CreateDepartmentPositionCommandResult
    {
        public int DepartmentPositionId { get; init; }
        public bool Success { get; init; }
        public bool DepartmentExists { get; init; }
        public bool PositionExists { get; init; }
    }

    internal class CreateDepartmentPositionCommandHandler : BaseHandler<CreateDepartmentPositionCommand, CreateDepartmentPositionCommandResult>
    {
        private readonly BaseSqlConnection _connection;
        
        public CreateDepartmentPositionCommandHandler(BaseSqlConnection connection, ILogger<CreateDepartmentPositionCommandHandler> logger) :
            base(logger)
        {
            _connection = connection;
        }

        protected override async Task<CreateDepartmentPositionCommandResult> HandleInternal(CreateDepartmentPositionCommand request, CancellationToken cancellationToken)
        {
            string deparmentExistsQuery =
            $@"
                SELECT EXISTS(SELECT 1 FROM departments WHERE id='{request.DepartmentId}')
            ";

            string positionExistsQuery =
            $@"
                SELECT EXISTS(SELECT 1 FROM positions WHERE id='{request.PositionId}')
            ";

            bool deparmentExists = await ExecuteSqlQuery<bool>(_connection, deparmentExistsQuery, cancellationToken);
            bool positionExists = await ExecuteSqlQuery<bool>(_connection, positionExistsQuery, cancellationToken);

            if (!deparmentExists || !positionExists)
            {
                return new()
                {
                    DepartmentPositionId = -1,
                    Success = false,
                    DepartmentExists = deparmentExists,
                    PositionExists = positionExists
                };
            }

            string createDepartmentPositionQuery = 
            $@"
                INSERT INTO department_position (department_id, position_id)
                VALUES ({request.DepartmentId}, {request.DepartmentId})
                RETURNING id
            ";

            int id = await ExecuteSqlQuery<int>(_connection, createDepartmentPositionQuery, cancellationToken);

            return new()
            {
                DepartmentPositionId = id,
                Success = true,
                DepartmentExists = true,
                PositionExists = true
            };
        }
    }
}