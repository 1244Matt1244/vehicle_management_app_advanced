using MediatR;

namespace VehicleApp.Application.Commands.Makes.CreateMake;

public record CreateMakeCommand(string Name, string Abrv) : IRequest<int>;
