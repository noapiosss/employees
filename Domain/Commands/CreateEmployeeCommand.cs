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
            string createEmployeeCommand = 
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
                    '{_dateTimeHelper.DateTimeToPostresDate(request.Employee.BirthDate)}',
                    '{_dateTimeHelper.DateTimeToPostresDate(request.Employee.EmploymentDate)}',
                    '{request.Employee.Salary}'
                )
                RETURNING id
            ";

            int id = await ExecuteSqlQuery<int>(_connection, createEmployeeCommand, cancellationToken);

            return new()
            {
                EmployeeId = id
            };
        }
    }
}