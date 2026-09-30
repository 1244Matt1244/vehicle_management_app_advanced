using Dapper;
using MediatR;
using VehicleApp.Application.Common.Interfaces;

namespace VehicleApp.Application.Commands.Makes.UpdateMake;

public class UpdateMakeCommandHandler : IRequestHandler<UpdateMakeCommand, bool>
{
    private readonly IDbConnectionFactory _connectionFactory;

    public UpdateMakeCommandHandler(IDbConnectionFactory connectionFactory)
        => _connectionFactory = connectionFactory;

    public async Task<bool> Handle(UpdateMakeCommand request, CancellationToken cancellationToken)
    {
        using var connection = _connectionFactory.CreateConnection();

        const string sql = @"
            UPDATE VehicleMakes
            SET Name = @Name, Abrv = @Abrv
            WHERE Id = @Id;";

        var rowsAffected = await connection.ExecuteAsync(sql, new
        {
            request.Id,
            request.Name,
            request.Abrv
        });

        return rowsAffected > 0;
    }
}
