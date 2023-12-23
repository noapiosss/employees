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
    public class DepartmentsWebController : Controller
    {
        private readonly IMediator _mediator;
        public DepartmentsWebController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public IActionResult Index(ICollection<DepartmentDTO> departments)
        {
            return View(departments);
        }

        public async Task<IActionResult> Departments(CancellationToken cancellationToken = default)
        {
            GetDepartmentsQuery query = new();
            GetDepartmentsQueryResult result = await _mediator.Send(query, cancellationToken);

            return View("Index", result.Departments);
        }

        public async Task<IActionResult> Details(int departmentId, CancellationToken cancellationToken = default)
        {
            GetDepartmentInfoQuery query = new() { DepartmentId = departmentId };
            GetDepartmentInfoQueryResult result = await _mediator.Send(query, cancellationToken);

            return View(result.DepartmentInfo);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}