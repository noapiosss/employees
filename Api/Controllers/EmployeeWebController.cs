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
        public EmployeeWebController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public IActionResult Index(ICollection<EmployeeDTO> employees)
        {
            return View(employees);
        }

        public async Task<IActionResult> Employees(CancellationToken cancellationToken = default)
        {
            GetAllEmployeesQuery query = new();
            GetAllEmployeesQueryResult result = await _mediator.Send(query, cancellationToken);

            return View("Index", result.Employees);
        }        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}