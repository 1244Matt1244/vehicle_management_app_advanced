using Dapper;
using MediatR;
using VehicleApp.Application.Common.Interfaces;
using VehicleApp.Application.Common.Models;

namespace VehicleApp.Application.Queries.Makes.GetMakes;

public class GetMakesQueryHandler : IRequestHandler<GetMakesQuery, PaginatedList<MakeDto>>
{
    private readonly IDbConnectionFactory _connectionFactory;

    public GetMakesQueryHandler(IDbConnectionFactory connectionFactory)
        => _connectionFactory = connectionFactory;

    public async Task<PaginatedList<MakeDto>> Handle(GetMakesQuery request, CancellationToken cancellationToken)
    {
        using var connection = _connectionFactory.CreateConnection();

        var query = "SELECT Id, Name, Abrv FROM VehicleMakes WHERE 1=1";
        var countQuery = "SELECT COUNT(*) FROM VehicleMakes WHERE 1=1";
        var parameters = new DynamicParameters();

        if (!string.IsNullOrWhiteSpace(request.SearchString))
        {
            var searchFilter = " AND (Name LIKE @Search OR Abrv LIKE @Search)";
            query += searchFilter;
            countQuery += searchFilter;
            parameters.Add("Search", $"%{request.SearchString}%");
        }

        var orderBy = request.SortBy?.ToLower() switch
        {
            "name" => "Name",
            "abrv" => "Abrv",
            _ => "Id"
        };

        var direction = request.SortOrder?.ToLower() == "desc" ? "DESC" : "ASC";
        query += $" ORDER BY {orderBy} {direction}";

        query += " OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";
        parameters.Add("Offset", (request.PageIndex - 1) * request.PageSize);
        parameters.Add("PageSize", request.PageSize);

        var totalCount = await connection.ExecuteScalarAsync<int>(countQuery, parameters);
        var items = (await connection.QueryAsync<MakeDto>(query, parameters)).ToList();

        return PaginatedList<MakeDto>.Create(items, totalCount, request.PageIndex, request.PageSize);
    }
}
