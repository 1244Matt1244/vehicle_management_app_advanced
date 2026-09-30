using MediatR;

namespace VehicleApp.Application.Commands.Models.UpdateModel;

public record UpdateModelCommand(int Id, int MakeId, string Name, string Abrv) : IRequest<bool>;
