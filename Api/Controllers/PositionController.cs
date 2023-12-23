
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
    [Route("api/positions")]
    public class PositionController : BaseController
    {
        private readonly IMediator _mediator;

        public PositionController(IMediator mediator,
            ILogger<PositionController> logger) : base(logger)
        {
            _mediator = mediator;
        }


        [HttpPost]
        public Task<IActionResult> CreatePosition([FromBody] CreatePositionRequest request, CancellationToken cancellationToken)
        {
            return SafeExecute(async () =>
            {
                CreatePositionCommand command = new() { Name = request.Name };
                CreatePositionCommandResult result = await _mediator.Send(command, cancellationToken);

                if (!result.Success)
                {
                    if (result.PositionAlreadyExists)
                    {
                        return ToActionResult(new()
                        {
                            Code = ErrorCode.PositionAlreadyExists,
                            Message = "Position already exists"
                        });
                    }
                }

                CreatePositionResponse response = new() { Id = result.PositionId };

                return Ok(response);

            }, cancellationToken);
        }

        [HttpGet]
        public Task<IActionResult> GetPositions(CancellationToken cancellationToken)
        {
            return SafeExecute(async () =>
            {
                GetPositionsQuery query = new();
                GetPositionsQueryResult result = await _mediator.Send(query, cancellationToken);

                return Ok(result);

            }, cancellationToken);
        }

    }
}