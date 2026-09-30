using MediatR;

namespace VehicleApp.Application.Commands.Models.CreateModel;

public record CreateModelCommand(int MakeId, string Name, string Abrv) : IRequest<int>;
