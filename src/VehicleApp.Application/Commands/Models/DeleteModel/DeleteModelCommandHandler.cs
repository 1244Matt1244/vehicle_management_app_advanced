using Dapper;
using MediatR;
using VehicleApp.Application.Common.Interfaces;

namespace VehicleApp.Application.Commands.Models.DeleteModel;

public class DeleteModelCommandHandler : IRequestHandler<DeleteModelCommand, bool>
{
    private readonly IDbConnectionFactory _connectionFactory;

    public DeleteModelCommandHandler(IDbConnectionFactory connectionFactory)
        => _connectionFactory = connectionFactory;

    public async Task<bool> Handle(DeleteModelCommand request, CancellationToken cancellationToken)
    {
        using var connection = _connectionFactory.CreateConnection();

        const string sql = "DELETE FROM VehicleModels WHERE Id = @Id;";

        var rowsAffected = await connection.ExecuteAsync(sql, new { request.Id });

        return rowsAffected > 0;
    }
}
