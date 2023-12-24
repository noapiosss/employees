using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

using Api.Models;
using Contracts.DTO;
using Domain.Commands;
using MediatR;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class EmployeeWebController : Controller
    {
        private readonly IMediator _mediator;
        private readonly int _perPage = 20;
        public EmployeeWebController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public IActionResult Index(ICollection<EmployeeDTO> employees)
        {
            return View(employees);
        }

        public async Task<IActionResult> Employees([FromQuery] int page = 1, CancellationToken cancellationToken = default)
        {
            GetAllEmployeesQuery query = new()
            {
                Page = page,
                PerPage = _perPage
            };

            GetAllEmployeesQueryResult result = await _mediator.Send(query, cancellationToken);

            return View("Index", result);
        }

        public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken = default)
        {
            GetEmployeeByIdQuery employeeByIdQuery = new() { Id = id };
            GetEmployeeByIdQueryResult employeeResult = await _mediator.Send(employeeByIdQuery, cancellationToken);

            GetDepartmentsQuery departmentsQuery = new();
            GetDepartmentsQueryResult departmentsResult = await _mediator.Send(departmentsQuery, cancellationToken);

            GetPositionsQuery positionsQuery = new();
            GetPositionsQueryResult positionsResult = await _mediator.Send(positionsQuery, cancellationToken);

            EditEmployeeModel model = new()
            {
                Employee = employeeResult.Employee,
                AllDepartments = departmentsResult.Departments,
                AllPositions = positionsResult.Positions
            };

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}