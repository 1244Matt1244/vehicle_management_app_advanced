using MediatR;
using Microsoft.AspNetCore.Mvc;
using VehicleApp.Application.Commands.Models.CreateModel;
using VehicleApp.Application.Commands.Models.DeleteModel;
using VehicleApp.Application.Commands.Models.UpdateModel;
using VehicleApp.Application.Queries.Models.GetModelById;
using VehicleApp.Application.Queries.Models.GetModels;

namespace VehicleApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ModelsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ModelsController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetModelsQuery query)
        => Ok(await _mediator.Send(query));

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _mediator.Send(new GetModelByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateModelCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateModelCommand command)
    {
        if (id != command.Id)
            return BadRequest(new { error = "ID mismatch" });

        var success = await _mediator.Send(command);
        return success ? NoContent() : NotFound();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _mediator.Send(new DeleteModelCommand(id));
        return success ? NoContent() : NotFound();
    }
}
