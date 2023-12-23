using System.Threading;
using System.Threading.Tasks;
using Contracts.Database;
using Domain.Base;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Domain.Commands
{
    public class EditEmployeeCommand : IRequest<EditEmployeeCommandResult>
    {
        public Employee Employee { get; init; }
    }

    public class EditEmployeeCommandResult
    {     
        public bool Success { get; init; }
        public bool EmployeeExists { get; init; }
        public bool DepartmentPositionExists { get; init; }
    }

    internal class EditEmployeeCommandHandler : BaseHandler<EditEmployeeCommand, EditEmployeeCommandResult>
    {
        private readonly BaseSqlConnection _connection;
        
        public EditEmployeeCommandHandler(BaseSqlConnection connection,
            ILogger<EditEmployeeCommandHandler> logger) : base(logger)
        {
            _connection = connection;
        }

        protected override async Task<EditEmployeeCommandResult> HandleInternal(EditEmployeeCommand request, CancellationToken cancellationToken)
        {
            string employeeExistsQuery =
            $@"
                SELECT EXISTS(SELECT 1 FROM employees WHERE id='{request.Employee.Id}')
            ";

            string departmentPositionExistsQuery =
            $@"
                SELECT EXISTS(SELECT 1 FROM department_position WHERE id='{request.Employee.DepartmentPositionId}')
            ";

            bool employeeExists = await ExecuteSqlQuery<bool>(_connection, employeeExistsQuery, cancellationToken);
            bool departmentPositionExists = await ExecuteSqlQuery<bool>(_connection, departmentPositionExistsQuery, cancellationToken);

            if (!departmentPositionExists || !employeeExists)
            {
                return new()
                {
                    Success = false,
                    EmployeeExists = employeeExists,
                    DepartmentPositionExists = departmentPositionExists
                };
            }

            string createEmployeeQuery = 
            $@"
                UPDATE employees
                SET
                    first_name='{request.Employee.FirstName}',
                    last_name='{request.Employee.LastName}',
                    patronymic='{request.Employee.Patronymic}',
                    address='{request.Employee.Address}',
                    phone='{request.Employee.Phone}',
                    birth_date='{request.Employee.BirthDate:yyyy-MM-dd}',
                    employment_date='{request.Employee.EmploymentDate:yyyy-MM-dd}',
                    salary='{request.Employee.Salary}'
                WHERE id={request.Employee.Id}
            ";

            await ExecuteSqlQuery(_connection, createEmployeeQuery, cancellationToken);

            return new()
            {
                Success = true,
                EmployeeExists = true,
                DepartmentPositionExists = true
            };
        }
    }
}