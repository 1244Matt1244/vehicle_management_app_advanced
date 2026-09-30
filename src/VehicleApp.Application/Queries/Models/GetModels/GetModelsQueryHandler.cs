using Dapper;
using MediatR;
using VehicleApp.Application.Common.Interfaces;
using VehicleApp.Application.Common.Models;

namespace VehicleApp.Application.Queries.Models.GetModels;

public class GetModelsQueryHandler : IRequestHandler<GetModelsQuery, PaginatedList<ModelDto>>
{
    private readonly IDbConnectionFactory _connectionFactory;

    public GetModelsQueryHandler(IDbConnectionFactory connectionFactory)
        => _connectionFactory = connectionFactory;

    public async Task<PaginatedList<ModelDto>> Handle(GetModelsQuery request, CancellationToken cancellationToken)
    {
        using var connection = _connectionFactory.CreateConnection();

        var baseQuery = @"
            FROM VehicleModels m
            INNER JOIN VehicleMakes mk ON m.MakeId = mk.Id
            WHERE 1=1";
        var parameters = new DynamicParameters();

        if (!string.IsNullOrWhiteSpace(request.SearchString))
        {
            baseQuery += " AND (m.Name LIKE @Search OR m.Abrv LIKE @Search)";
            parameters.Add("Search", $"%{request.SearchString}%");
        }

        if (request.MakeId.HasValue)
        {
            baseQuery += " AND m.MakeId = @MakeId";
            parameters.Add("MakeId", request.MakeId.Value);
        }

        var orderBy = request.SortBy?.ToLower() switch
        {
            "name" => "m.Name",
            "abrv" => "m.Abrv",
            "make" => "mk.Name",
            _ => "m.Id"
        };
        var direction = request.SortOrder?.ToLower() == "desc" ? "DESC" : "ASC";

        var countSql = $"SELECT COUNT(*) {baseQuery}";
        var dataSql = $@"
            SELECT m.Id, m.MakeId, m.Name, m.Abrv, mk.Name AS MakeName
            {baseQuery}
            ORDER BY {orderBy} {direction}
            OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";

        parameters.Add("Offset", (request.PageIndex - 1) * request.PageSize);
        parameters.Add("PageSize", request.PageSize);

        var totalCount = await connection.ExecuteScalarAsync<int>(countSql, parameters);
        var items = (await connection.QueryAsync<ModelDto>(dataSql, parameters)).ToList();

        return PaginatedList<ModelDto>.Create(items, totalCount, request.PageIndex, request.PageSize);
    }
}
