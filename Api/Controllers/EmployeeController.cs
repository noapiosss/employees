using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Api.Controllers.Base;
using Contracts.Database;
using Contracts.Http;
using Domain.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Controllers
{
    [Route("api/employees")]
    public class EmployeeController : BaseController
    {
        private readonly IMediator _mediator;

        public EmployeeController(IMediator mediator,
            ILogger<EmployeeController> logger) : base(logger)
        {
            _mediator = mediator;
        }


        [HttpPost]
        public Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeRequest request, CancellationToken cancellationToken)
        {
            return SafeExecute(async () =>
            {
                Employee employee = new()
                {
                    DepartmentId = request.DepartmentId,
                    PositionId = request.PositionId,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Patronymic = request.Patronymic,
                    Address = request.Address,
                    Phone = request.Phone,
                    BirthDate = request.BirthDate,
                    EmploymentDate = request.EmploymentDate,
                    Salary = request.Salary,
                };

                CreateEmployeeCommand command = new() { Employee = employee };
                CreateEmployeeCommandResult result = await _mediator.Send(command, cancellationToken);
                CreateEmployeeResponse response = new() { Id = result.EmployeeId };

                return Ok(response);

            }, cancellationToken);
        }

        [HttpGet]
        public Task<IActionResult> GetAllEmployees(CancellationToken cancellationToken)
        {
            return SafeExecute(async () =>
            {
                GetAllEmployeesQuery query = new() {  };
                GetAllEmployeesQueryResult result = await _mediator.Send(query, cancellationToken);

                return Ok(result);

            }, cancellationToken);
        }

    }
}