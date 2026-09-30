using MediatR;
using VehicleApp.Application.Queries.Models.GetModels;

namespace VehicleApp.Application.Queries.Models.GetModelById;

public record GetModelByIdQuery(int Id) : IRequest<ModelDto?>;
