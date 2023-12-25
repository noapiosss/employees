using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Api.Models;
using Contracts.Database;
using Contracts.DTO;
using Contracts.Http;
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

        public IActionResult Index(EmployeesViewModel employeesViewModel)
        {
            return View(employeesViewModel);
        }

        public async Task<IActionResult> Employees([FromQuery] int page = 1, CancellationToken cancellationToken = default)
        {
            GetAllEmployeesQuery query = new()
            {
                Page = page,
                PerPage = _perPage
            };

            GetAllEmployeesQueryResult result = await _mediator.Send(query, cancellationToken);

            GetFiltersQuery filtersQuery = new();
            GetFiltersQueryResult filtersResult = await _mediator.Send(filtersQuery, cancellationToken);

            BoundValues boundValues = new()
            {
                BoundFilterValues = filtersResult.BoundFilterValues,
                Departmens = filtersResult.Departmens.ToList(),
                Positions = filtersResult.Positions.ToList()
            };

            EmployeesViewModel model = new()
            {
                Employees = result.Employees.ToList(),
                BoundValues = boundValues,
                Page = result.Page,
                PerPage = result.PerPage,
                PagesCount = result.PagesCount              
            };

            return View("Index", model);
        }

        [HttpPost]
        public async Task<IActionResult> Employees([FromForm] EmployeesViewModel request, [FromQuery] int page = 1, CancellationToken cancellationToken = default)
        {
            GetFilteredEmployeesQuery query = new()
            {
                DepartmentId = request.Filters.DepartmentId,
                PositionId = request.Filters.PositionId,
                BoundFilterValues = request.Filters.BoundFilterValues,
                SearchRequest = request.Filters.SearchRequest,
                Page = page,
                PerPage = _perPage
            };

            GetFilteredEmployeesQueryResult result = await _mediator.Send(query, cancellationToken);

            GetFiltersQuery filtersQuery = new();
            GetFiltersQueryResult filtersResult = await _mediator.Send(filtersQuery, cancellationToken);

            BoundValues boundValues = new()
            {
                BoundFilterValues = filtersResult.BoundFilterValues,
                Departmens = filtersResult.Departmens.ToList(),
                Positions = filtersResult.Positions.ToList()
            };

            EmployeesViewModel model = new()
            {
                Employees = result.Employees.ToList(),
                BoundValues = boundValues,
                Page = result.Page,
                PerPage = result.PerPage,
                PagesCount = result.PagesCount              
            };

            return View("Index", model);
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

        [HttpPost]
        public async Task<IActionResult> Edit([FromForm] Employee employee, int departmentId, int positionId, CancellationToken cancellationToken)
        {
            EditEmployeeCommand command = new()
            {
                Employee = employee,
                DepartmentId = departmentId,
                PositionId = positionId
            };
            
            EditEmployeeCommandResult result = await _mediator.Send(command, cancellationToken);

            return RedirectToAction("Details", "DepartmentsWeb", new { departmentId });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}