using FC.AbreuBarber.Application.UseCases.Procedure.Common;
using FC.AbreuBarber.Application.UseCases.Procedure.CreateProcedure;
using FC.AbreuBarber.Application.UseCases.Procedure.DeleteProcedure;
using FC.AbreuBarber.Application.UseCases.Procedure.GetProcedure;
using FC.AbreuBarber.Application.UseCases.Procedure.UpdateProcedure;
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
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var output = await _mediator.Send(new GetProcedureInput(id), cancellationToken);

            return Ok(output);
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(typeof(ProcedureModelOutput), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteProcedureInput(id), cancellationToken);

            return NoContent();
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(ProcedureModelOutput), StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        //[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(
            [FromRoute] Guid id,
            [FromBody] UpdateProcedureInput apiInput, 
            CancellationToken cancellationToken)
        {

            var input = new UpdateProcedureInput(
                id,
                apiInput.Name,
                apiInput.Value,
                apiInput.Description,
                apiInput.IsActive
            );

            var output = await _mediator.Send(input, cancellationToken);

            return Ok(output);
        }
    }
}