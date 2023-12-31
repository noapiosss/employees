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
        public int DepartmentId { get; init; }
        public int PositionId { get; init; }
    }

    public class EditEmployeeCommandResult
    {     
        public bool Success { get; init; }
        public bool EmployeeExists { get; init; }
        public bool DepartmentExists { get; init; }
        public bool PositionExists { get; init; }
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
                SELECT EXISTS(SELECT 1 FROM employees WHERE id={request.Employee.Id})
            ";

            string departmentExistsQuery =
            $@"
                SELECT EXISTS(SELECT 1 FROM departments WHERE id={request.DepartmentId})
            ";

            string positionExistsQuery =
            $@"
                SELECT EXISTS(SELECT 1 FROM positions WHERE id={request.PositionId})
            ";

            bool employeeExists = await ExecuteSqlQuery<bool>(_connection, employeeExistsQuery, cancellationToken);
            bool departmentExists = await ExecuteSqlQuery<bool>(_connection, departmentExistsQuery, cancellationToken);
            bool positionExists = await ExecuteSqlQuery<bool>(_connection, positionExistsQuery, cancellationToken);

            if (!employeeExists || !departmentExists || !positionExists)
            {
                return new()
                {
                    Success = false,
                    EmployeeExists = employeeExists,
                    DepartmentExists = departmentExists,
                    PositionExists = positionExists
                };
            }

            string getCurrentDepartmentPositionQuery = 
            $@"
                SELECT 
                    department_id,
                    position_id,
                    employee_id
                FROM department_position
                WHERE
                    employee_id={request.Employee.Id}
            ";

            DepartmentPosition currentDepartmentPosition = await ExecuteSqlQuery<DepartmentPosition>(_connection, getCurrentDepartmentPositionQuery, cancellationToken);
            
            if (request.DepartmentId != currentDepartmentPosition.DepartmentId || request.PositionId != currentDepartmentPosition.PositionId)
            {
                string removeFromPerviousPositionQuery = 
                $@"
                    UPDATE department_position
                    SET
                        employee_id=NULL
                    WHERE
                        department_id={currentDepartmentPosition.DepartmentId} AND
                        position_id={currentDepartmentPosition.PositionId} AND
                        employee_id={request.Employee.Id}
                ";

                await ExecuteSqlQuery(_connection, removeFromPerviousPositionQuery, cancellationToken);

                string vacancyExistsQuery = 
                $@"
                    SELECT EXISTS
                    (
                        SELECT 1
                        FROM department_position
                        WHERE
                            department_id={request.DepartmentId} AND
                            position_id={request.PositionId} AND
                            employee_id=NULL
                    )
                ";

                bool vacancyExists = await ExecuteSqlQuery<bool>(_connection, vacancyExistsQuery, cancellationToken);

                if (vacancyExists)
                {
                    string getVacncyIdQuery =
                    $@"
                        SELECT id
                        FROM department_position
                        WHERE
                            department_id={request.DepartmentId} AND
                            position_id={request.PositionId} AND
                            employee_id=NULL
                        LIMIT 1
                    ";

                    int vacancyId = await ExecuteSqlQuery<int>(_connection, getVacncyIdQuery, cancellationToken);

                    string setEmployeeForDepartmentPositioQuery =
                    $@"
                        INSERT INTO department_position(employee_id)
                        VALUES ({request.Employee.Id})
                        WHERE id={vacancyId}
                    ";
                    
                    await ExecuteSqlQuery(_connection, setEmployeeForDepartmentPositioQuery, cancellationToken);
                }
                else
                {
                    string createDepartmentPositioQuery =
                    $@"
                        INSERT INTO department_position(department_id, position_id, employee_id)
                        VALUES ({request.DepartmentId}, {request.PositionId}, {request.Employee.Id})
                    ";
                    
                    await ExecuteSqlQuery(_connection, createDepartmentPositioQuery, cancellationToken);
                }
            }

            string editEmployeeQuery = 
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

            await ExecuteSqlQuery(_connection, editEmployeeQuery, cancellationToken);

            return new()
            {
                Success = true,
                EmployeeExists = true,
                DepartmentExists = true,
                PositionExists = true
            };
        }
    }
}