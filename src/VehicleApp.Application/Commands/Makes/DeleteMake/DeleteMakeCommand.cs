using MediatR;

namespace VehicleApp.Application.Commands.Makes.DeleteMake;

public record DeleteMakeCommand(int Id) : IRequest<bool>;
