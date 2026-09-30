using MediatR;
using Microsoft.AspNetCore.Mvc;
using VehicleApp.Application.Commands.Makes.CreateMake;
using VehicleApp.Application.Commands.Makes.DeleteMake;
using VehicleApp.Application.Commands.Makes.UpdateMake;
using VehicleApp.Application.Queries.Makes.GetMakeById;
using VehicleApp.Application.Queries.Makes.GetMakes;

namespace VehicleApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MakesController : ControllerBase
{
    private readonly IMediator _mediator;

    public MakesController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetMakesQuery query)
        => Ok(await _mediator.Send(query));

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _mediator.Send(new GetMakeByIdQuery(id));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateMakeCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateMakeCommand command)
    {
        if (id != command.Id)
            return BadRequest(new { error = "ID mismatch" });

        var success = await _mediator.Send(command);
        return success ? NoContent() : NotFound();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _mediator.Send(new DeleteMakeCommand(id));
        return success ? NoContent() : NotFound();
    }
}
