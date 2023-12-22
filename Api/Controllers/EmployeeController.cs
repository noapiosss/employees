using System;
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
                    DepartmentPositionId = request.DepartmentPositionId,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Patronymic = request.Patronymic,
                    Address = request.Address,
                    Phone = request.Phone,
                    BirthDate = DateOnly.FromDateTime(request.BirthDate),
                    EmploymentDate = DateOnly.FromDateTime(request.EmploymentDate),
                    Salary = request.Salary,
                };

                CreateEmployeeCommand command = new() { Employee = employee };
                CreateEmployeeCommandResult result = await _mediator.Send(command, cancellationToken);

                if (!result.Success)
                {
                    if (!result.DepartmentPositionExists)
                    {
                        return ToActionResult(new()
                        {
                            Code = ErrorCode.DepartmentPositionNotFound,
                            Message = "Department or position in department does not exists"
                        });
                    }
                }

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

        [HttpGet("/filters")]
        public Task<IActionResult> GetFilters(CancellationToken cancellationToken)
        {
            return SafeExecute(async () =>
            {
                GetFiltersQuery query = new() {  };
                GetFiltersQueryResult result = await _mediator.Send(query, cancellationToken);

                return Ok(result);

            }, cancellationToken);
        }

        [HttpGet("/filtered")]
        public Task<IActionResult> GetFilteredEmployees(CancellationToken cancellationToken)
        {
            return SafeExecute(async () =>
            {
                GetFilteredEmployeesQuery query = new() {  };
                GetFilteredEmployeesQueryResult result = await _mediator.Send(query, cancellationToken);

                return Ok(result);

            }, cancellationToken);
        }

        [HttpGet("/positions/{positionId}")]
        public Task<IActionResult> GetAllEmployeesByPosition(int positionId, CancellationToken cancellationToken)
        {
            return SafeExecute(async () =>
            {
                GetEmployeesByPositionQuery query = new() { PositionId = positionId };
                GetEmployeesByPositionQueryResult result = await _mediator.Send(query, cancellationToken);

                if (!result.Success)
                {
                    if (!result.PositionExists)
                    {
                        return ToActionResult(new()
                        {
                            Code = ErrorCode.PositionNotFound,
                            Message = "Position does not exist"
                        });
                    }
                }

                return Ok(result.Employees);

            }, cancellationToken);
        }

        [HttpGet("/departments/{departmentId}")]
        public Task<IActionResult> GetAllEmployeesByDepartment(int departmentId, CancellationToken cancellationToken)
        {
            return SafeExecute(async () =>
            {
                GetEmployeesByDepartmentQuery query = new() { DepartmentId = departmentId };
                GetEmployeesByDepartmentQueryResult result = await _mediator.Send(query, cancellationToken);

                if (!result.Success)
                {
                    if (!result.DepartmentExists)
                    {
                        return ToActionResult(new()
                        {
                            Code = ErrorCode.DepartmentNotFound,
                            Message = "Department does not exist"
                        });
                    }
                }

                return Ok(result.Employees);

            }, cancellationToken);
        }

        [HttpGet("/test")]
        public Task<IActionResult> Test(CancellationToken cancellationToken)
        {
            return SafeExecute(async () =>
            {
                GenerateTestDataCommand command = new();
                GenerateTestDataCommandResult result = await _mediator.Send(command, cancellationToken);
                
                return Ok();

            }, cancellationToken);
        }

    }
}