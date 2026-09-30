using MediatR;

namespace VehicleApp.Application.Commands.Models.DeleteModel;

public record DeleteModelCommand(int Id) : IRequest<bool>;
