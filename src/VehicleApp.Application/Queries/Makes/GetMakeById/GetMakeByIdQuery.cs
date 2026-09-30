using MediatR;
using VehicleApp.Application.Queries.Makes.GetMakes;

namespace VehicleApp.Application.Queries.Makes.GetMakeById;

public record GetMakeByIdQuery(int Id) : IRequest<MakeDto?>;
