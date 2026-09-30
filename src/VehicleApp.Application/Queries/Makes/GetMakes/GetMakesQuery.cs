using MediatR;
using VehicleApp.Application.Common.Models;

namespace VehicleApp.Application.Queries.Makes.GetMakes;

public record GetMakesQuery(
    int PageIndex = 1,
    int PageSize = 10,
    string? SortBy = null,
    string? SortOrder = null,
    string? SearchString = null) : IRequest<PaginatedList<MakeDto>>;

public record MakeDto(int Id, string Name, string Abrv);
