using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

using Api.Models;
using Contracts.DTO;
using Contracts.Http;
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

            if (!result.Success)
            {
                if (!result.DepartmentExists)
                {
                    ErrorResponse errorResponse = new()
                    {
                        Code = ErrorCode.DepartmentNotFound,
                        Message = "Department does not exists"
                    };

                    return RedirectToAction("Error", "DepartmentsWeb", new { errorResponse });
                }
            }

            return View(result.DepartmentInfo);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(ErrorResponse errorResponse)
        {
            return View(new ErrorViewModel { ErrorCode = (int)errorResponse.Code / 100, Message = errorResponse.Message });
        }
    }
}