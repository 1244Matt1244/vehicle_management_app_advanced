using Dapper;
using MediatR;
using VehicleApp.Application.Common.Interfaces;
using VehicleApp.Application.Queries.Models.GetModels;

namespace VehicleApp.Application.Queries.Models.GetModelById;

public class GetModelByIdQueryHandler : IRequestHandler<GetModelByIdQuery, ModelDto?>
{
    private readonly IDbConnectionFactory _connectionFactory;

    public GetModelByIdQueryHandler(IDbConnectionFactory connectionFactory)
        => _connectionFactory = connectionFactory;

    public async Task<ModelDto?> Handle(GetModelByIdQuery request, CancellationToken cancellationToken)
    {
        using var connection = _connectionFactory.CreateConnection();

        const string sql = @"
            SELECT m.Id, m.MakeId, m.Name, m.Abrv, mk.Name AS MakeName
            FROM VehicleModels m
            INNER JOIN VehicleMakes mk ON m.MakeId = mk.Id
            WHERE m.Id = @Id;";

        return await connection.QuerySingleOrDefaultAsync<ModelDto>(sql, new { request.Id });
    }
}
