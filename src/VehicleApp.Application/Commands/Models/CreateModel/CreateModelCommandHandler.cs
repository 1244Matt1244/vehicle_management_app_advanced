using Dapper;
using MediatR;
using VehicleApp.Application.Common.Interfaces;

namespace VehicleApp.Application.Commands.Models.CreateModel;

public class CreateModelCommandHandler : IRequestHandler<CreateModelCommand, int>
{
    private readonly IDbConnectionFactory _connectionFactory;

    public CreateModelCommandHandler(IDbConnectionFactory connectionFactory)
        => _connectionFactory = connectionFactory;

    public async Task<int> Handle(CreateModelCommand request, CancellationToken cancellationToken)
    {
        using var connection = _connectionFactory.CreateConnection();

        const string sql = @"
            INSERT INTO VehicleModels (MakeId, Name, Abrv)
            VALUES (@MakeId, @Name, @Abrv);
            SELECT CAST(SCOPE_IDENTITY() as int);";

        return await connection.ExecuteScalarAsync<int>(sql, new
        {
            request.MakeId,
            request.Name,
            request.Abrv
        });
    }
}
