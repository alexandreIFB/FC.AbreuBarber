using Fc.AbreuBarber.Api.ApiModels.Response;
using FC.AbreuBarber.Application.UseCases.Procedure.Common;
using FC.AbreuBarber.Application.UseCases.Procedure.CreateProcedure;
using FC.AbreuBarber.Application.UseCases.Procedure.DeleteProcedure;
using FC.AbreuBarber.Application.UseCases.Procedure.GetProcedure;
using FC.AbreuBarber.Application.UseCases.Procedure.ListProcedures;
using FC.AbreuBarber.Application.UseCases.Procedure.UpdateProcedure;
using FC.AbreuBarber.Domain.SeedWork.SearchableRepository;
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
        [ProducesResponseType( typeof(ApiResponse<ProcedureModelOutput>), StatusCodes.Status201Created) ]
        [ProducesResponseType( typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType( typeof(ProblemDetails), StatusCodes.Status400BadRequest) ]
        public async Task<IActionResult> Create([FromBody] CreateProcedureInput input , CancellationToken cancellationToken)
        {

            var output = await _mediator.Send(input, cancellationToken);

            return CreatedAtAction(nameof(Create), new {output.Id} , new ApiResponse<ProcedureModelOutput>(output));
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<ProcedureModelOutput>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var output = await _mediator.Send(new GetProcedureInput(id), cancellationToken);

            return Ok(new ApiResponse<ProcedureModelOutput>(output));
        }

        [HttpGet]
        [ProducesResponseType(typeof(ProcedureModelOutput), StatusCodes.Status200OK)]
        // [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> List(
            CancellationToken cancellationToken,
            [FromQuery] int? page = null,
            [FromQuery] int? perPage = null,
            [FromQuery] string? search = null,
            [FromQuery] string? sort = null,
            [FromQuery] SearchOrder? dir = null
            )
        {

            var input = new ListProceduresInput();
            if (page is not null) input.Page = page.Value;
            if (perPage is not null) input.PerPage = perPage.Value;
            if (!String.IsNullOrWhiteSpace(search)) input.Search = search;
            if (!String.IsNullOrWhiteSpace(sort)) input.Sort = sort;
            if (dir is not null) input.Dir = dir.Value;

            var output = await _mediator.Send(input, cancellationToken);

            return Ok(output);
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteProcedureInput(id), cancellationToken);

            return NoContent();
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<ProcedureModelOutput>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
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

            return Ok(new ApiResponse<ProcedureModelOutput>(output));
        }
    }
}