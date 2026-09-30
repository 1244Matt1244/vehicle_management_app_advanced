using Dapper;
using MediatR;
using VehicleApp.Application.Common.Interfaces;
using VehicleApp.Application.Queries.Makes.GetMakes;

namespace VehicleApp.Application.Queries.Makes.GetMakeById;

public class GetMakeByIdQueryHandler : IRequestHandler<GetMakeByIdQuery, MakeDto?>
{
    private readonly IDbConnectionFactory _connectionFactory;

    public GetMakeByIdQueryHandler(IDbConnectionFactory connectionFactory)
        => _connectionFactory = connectionFactory;

    public async Task<MakeDto?> Handle(GetMakeByIdQuery request, CancellationToken cancellationToken)
    {
        using var connection = _connectionFactory.CreateConnection();

        const string sql = "SELECT Id, Name, Abrv FROM VehicleMakes WHERE Id = @Id;";

        return await connection.QuerySingleOrDefaultAsync<MakeDto>(sql, new { request.Id });
    }
}
