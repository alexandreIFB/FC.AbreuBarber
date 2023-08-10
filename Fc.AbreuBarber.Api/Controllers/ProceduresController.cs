using FC.AbreuBarber.Application.UseCases.Procedure.Common;
using FC.AbreuBarber.Application.UseCases.Procedure.CreateProcedure;
using FC.AbreuBarber.Application.UseCases.Procedure.GetProcedure;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Fc.AbreuBarber.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProceduresController : ControllerBase
    {

        private readonly IMediator _mediator;

        public ProceduresController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType( typeof(ProcedureModelOutput), StatusCodes.Status201Created) ]
        [ProducesResponseType( typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType( typeof(ProblemDetails), StatusCodes.Status400BadRequest) ]
        public async Task<IActionResult> Create([FromBody] CreateProcedureInput input , CancellationToken cancellationToken)
        {

            var output = await _mediator.Send(input, cancellationToken);

            return CreatedAtAction(nameof(Create), new {output.Id} , output);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ProcedureModelOutput), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var output = await _mediator.Send(new GetProcedureInput(id), cancellationToken);

            return Ok(output);
        }
    }
}