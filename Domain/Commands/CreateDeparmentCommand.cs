using System.Threading;
using System.Threading.Tasks;
using Domain.Base;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Domain.Commands
{
    public class CreateDepartmentCommand : IRequest<CreateDepartmentCommandResult>
    {
        public string Name { get; init; }
    }

    public class CreateDepartmentCommandResult
    {
        public int DepartmentId { get; init; }
        public bool Success { get; init; }
        public bool DepartmentAlreadyExists { get; init; }
    }

    internal class CreateDepartmentCommandHandler : BaseHandler<CreateDepartmentCommand, CreateDepartmentCommandResult>
    {
        private readonly BaseSqlConnection _connection;
        
        public CreateDepartmentCommandHandler(BaseSqlConnection connection, ILogger<CreateDepartmentCommandHandler> logger) :
            base(logger)
        {
            _connection = connection;
        }

        protected override async Task<CreateDepartmentCommandResult> HandleInternal(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
            string deparmentAlreadyExistsCommand =
            $@"
                SELECT EXISTS(SELECT 1 FROM departments WHERE name='{request.Name}')
            ";

            bool deparmentAlreadyExists = await ExecuteSqlQuery<bool>(_connection, deparmentAlreadyExistsCommand, cancellationToken);

            if (deparmentAlreadyExists)
            {
                return new()
                {
                    DepartmentId = -1,
                    Success = false,
                    DepartmentAlreadyExists = true
                };
            }

            string createDepartmentCommand = 
            $@"
                INSERT INTO departments (name)
                VALUES ('{request.Name}')
                RETURNING id
            ";

            int id = await ExecuteSqlQuery<int>(_connection, createDepartmentCommand, cancellationToken);

            return new()
            {
                DepartmentId = id,
                Success = true,
                DepartmentAlreadyExists = false
            };
        }
    }
}