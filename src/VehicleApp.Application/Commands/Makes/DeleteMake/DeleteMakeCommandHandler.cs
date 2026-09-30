using Dapper;
using MediatR;
using VehicleApp.Application.Common.Interfaces;

namespace VehicleApp.Application.Commands.Makes.DeleteMake;

public class DeleteMakeCommandHandler : IRequestHandler<DeleteMakeCommand, bool>
{
    private readonly IDbConnectionFactory _connectionFactory;

    public DeleteMakeCommandHandler(IDbConnectionFactory connectionFactory)
        => _connectionFactory = connectionFactory;

    public async Task<bool> Handle(DeleteMakeCommand request, CancellationToken cancellationToken)
    {
        using var connection = _connectionFactory.CreateConnection();

        const string sql = "DELETE FROM VehicleMakes WHERE Id = @Id;";

        var rowsAffected = await connection.ExecuteAsync(sql, new { request.Id });

        return rowsAffected > 0;
    }
}
