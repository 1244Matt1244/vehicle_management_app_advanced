using MediatR;

namespace VehicleApp.Application.Commands.Makes.UpdateMake;

public record UpdateMakeCommand(int Id, string Name, string Abrv) : IRequest<bool>;
