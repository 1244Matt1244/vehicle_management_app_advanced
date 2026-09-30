using MediatR;
using VehicleApp.Application.Common.Models;

namespace VehicleApp.Application.Queries.Models.GetModels;

public record GetModelsQuery(
    int PageIndex = 1,
    int PageSize = 10,
    string? SortBy = null,
    string? SortOrder = null,
    string? SearchString = null,
    int? MakeId = null) : IRequest<PaginatedList<ModelDto>>;

public record ModelDto(int Id, int MakeId, string Name, string Abrv, string MakeName);
