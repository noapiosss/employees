using System.Threading;
using System.Threading.Tasks;
using Api.Controllers.Base;
using Contracts.Http;
using Domain.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Controllers
{
    [Route("api/departments")]
    public class DepartmentController : BaseController
    {
        private readonly IMediator _mediator;

        public DepartmentController(IMediator mediator,
            ILogger<DepartmentController> logger) : base(logger)
        {
            _mediator = mediator;
        }


        [HttpPost]
        public Task<IActionResult> CreateDepartment([FromBody] CreateDepartmentRequest request, CancellationToken cancellationToken)
        {
            return SafeExecute(async () =>
            {
                CreateDepartmentCommand command = new() { Name = request.Name };
                CreateDepartmentCommandResult result = await _mediator.Send(command, cancellationToken);

                if (!result.Success)
                {
                    if (result.DepartmentAlreadyExists)
                    {
                        return ToActionResult(new()
                        {
                            Code = ErrorCode.DepartmentAlreadyExists,
                            Message = "Department already exists"
                        });
                    }
                }

                CreateDepartmentResponse response = new() { Id = result.DepartmentId };

                return Ok(response);

            }, cancellationToken);
        }

        [HttpGet]
        public Task<IActionResult> GetDepartments(CancellationToken cancellationToken)
        {
            return SafeExecute(async () =>
            {
                GetDepartmentsQuery query = new() {  };
                GetDepartmentsQueryResult result = await _mediator.Send(query, cancellationToken);

                return Ok(result);

            }, cancellationToken);
        }

    }
}