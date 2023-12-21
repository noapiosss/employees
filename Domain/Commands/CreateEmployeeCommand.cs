using System.Threading;
using System.Threading.Tasks;
using Contracts.Database;
using Domain.Base;
using Domain.Helpers.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Domain.Commands
{
    public class CreateEmployeeCommand : IRequest<CreateEmployeeCommandResult>
    {
        public Employee Employee { get; init; }
    }

    public class CreateEmployeeCommandResult
    {
        public int EmployeeId { get; init; }        
        public bool Success { get; init; }
        public bool DepartmentExists { get; init; }
        public bool PositionExists { get; init; }
    }

    internal class CreateEmployeeCommandHandler : BaseHandler<CreateEmployeeCommand, CreateEmployeeCommandResult>
    {
        private readonly BaseSqlConnection _connection;
        private readonly IDateTimeHelper _dateTimeHelper;
        
        public CreateEmployeeCommandHandler(BaseSqlConnection connection,
            IDateTimeHelper dateTimeHelper,
            ILogger<CreateEmployeeCommandHandler> logger) : base(logger)
        {
            _connection = connection;
            _dateTimeHelper = dateTimeHelper;
        }

        protected override async Task<CreateEmployeeCommandResult> HandleInternal(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            string deparmentExistsQuery =
            $@"
                SELECT EXISTS(SELECT 1 FROM departments WHERE id='{request.Employee.DepartmentId}')
            ";

            string positionExistsQuery =
            $@"
                SELECT EXISTS(SELECT 1 FROM positions WHERE id='{request.Employee.PositionId}')
            ";

            bool deparmentExists = await ExecuteSqlQuery<bool>(_connection, deparmentExistsQuery, cancellationToken);
            bool positionExists = await ExecuteSqlQuery<bool>(_connection, positionExistsQuery, cancellationToken);

            if (!deparmentExists || !positionExists)
            {
                return new()
                {
                    EmployeeId = -1,
                    Success = false,
                    DepartmentExists = deparmentExists,
                    PositionExists = positionExists
                };
            }

            string createEmployeeQuery = 
            $@"
                INSERT INTO employees (department_id, position_id, first_name, last_name, patronymic, address, phone, birth_date, employment_date, salary)
                VALUES
                (
                    (SELECT id FROM departments WHERE id = {request.Employee.DepartmentId}),
                    (SELECT id FROM positions WHERE id = {request.Employee.PositionId}),
                    '{request.Employee.FirstName}',
                    '{request.Employee.LastName}',
                    '{request.Employee.Patronymic}',
                    '{request.Employee.Address}',
                    '{request.Employee.Phone}',
                    '{_dateTimeHelper.DateTimeToPostgresDate(request.Employee.BirthDate)}',
                    '{_dateTimeHelper.DateTimeToPostgresDate(request.Employee.EmploymentDate)}',
                    '{request.Employee.Salary}'
                )
                RETURNING id
            ";

            int id = await ExecuteSqlQuery<int>(_connection, createEmployeeQuery, cancellationToken);

            return new()
            {
                EmployeeId = id,
                Success = true,
                DepartmentExists = true,
                PositionExists = true
            };
        }
    }
}