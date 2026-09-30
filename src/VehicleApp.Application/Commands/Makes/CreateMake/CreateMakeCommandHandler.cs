using Dapper;
using MediatR;
using VehicleApp.Application.Common.Interfaces;

namespace VehicleApp.Application.Commands.Makes.CreateMake;

public class CreateMakeCommandHandler : IRequestHandler<CreateMakeCommand, int>
{
    private readonly IDbConnectionFactory _connectionFactory;

    public CreateMakeCommandHandler(IDbConnectionFactory connectionFactory)
        => _connectionFactory = connectionFactory;

    public async Task<int> Handle(CreateMakeCommand request, CancellationToken cancellationToken)
    {
        using var connection = _connectionFactory.CreateConnection();

        const string sql = @"
            INSERT INTO VehicleMakes (Name, Abrv)
            VALUES (@Name, @Abrv);
            SELECT CAST(SCOPE_IDENTITY() as int);";

        return await connection.ExecuteScalarAsync<int>(sql, new
        {
            request.Name,
            request.Abrv
        });
    }
}
