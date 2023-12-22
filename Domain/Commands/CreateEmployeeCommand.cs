using System.Threading;
using System.Threading.Tasks;
using Contracts.Database;
using Domain.Base;
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
        public bool DepartmentPositionExists { get; init; }
    }

    internal class CreateEmployeeCommandHandler : BaseHandler<CreateEmployeeCommand, CreateEmployeeCommandResult>
    {
        private readonly BaseSqlConnection _connection;
        
        public CreateEmployeeCommandHandler(BaseSqlConnection connection,
            ILogger<CreateEmployeeCommandHandler> logger) : base(logger)
        {
            _connection = connection;
        }

        protected override async Task<CreateEmployeeCommandResult> HandleInternal(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            string departmentPositionExistsQuery =
            $@"
                SELECT EXISTS(SELECT 1 FROM department_position WHERE id='{request.Employee.DepartmentPositionId}')
            ";

            bool departmentPositionExists = await ExecuteSqlQuery<bool>(_connection, departmentPositionExistsQuery, cancellationToken);

            if (!departmentPositionExists)
            {
                return new()
                {
                    EmployeeId = -1,
                    Success = false,
                    DepartmentPositionExists = departmentPositionExists
                };
            }

            string createEmployeeQuery = 
            $@"
                INSERT INTO employees (department_id, position_id, first_name, last_name, patronymic, address, phone, birth_date, employment_date, salary)
                VALUES
                (
                    (SELECT id FROM department_position WHERE id = {request.Employee.DepartmentPositionId}),
                    '{request.Employee.FirstName}',
                    '{request.Employee.LastName}',
                    '{request.Employee.Patronymic}',
                    '{request.Employee.Address}',
                    '{request.Employee.Phone}',
                    '{request.Employee.BirthDate:yyyy-MM-dd}',
                    '{request.Employee.EmploymentDate:yyyy-MM-dd}',
                    '{request.Employee.Salary}'
                )
                RETURNING id
            ";

            int id = await ExecuteSqlQuery<int>(_connection, createEmployeeQuery, cancellationToken);

            return new()
            {
                EmployeeId = id,
                Success = true,
                DepartmentPositionExists = true
            };
        }
    }
}