using Dapper;
using MediatR;
using VehicleApp.Application.Common.Interfaces;

namespace VehicleApp.Application.Commands.Models.UpdateModel;

public class UpdateModelCommandHandler : IRequestHandler<UpdateModelCommand, bool>
{
    private readonly IDbConnectionFactory _connectionFactory;

    public UpdateModelCommandHandler(IDbConnectionFactory connectionFactory)
        => _connectionFactory = connectionFactory;

    public async Task<bool> Handle(UpdateModelCommand request, CancellationToken cancellationToken)
    {
        using var connection = _connectionFactory.CreateConnection();

        const string sql = @"
            UPDATE VehicleModels
            SET MakeId = @MakeId, Name = @Name, Abrv = @Abrv
            WHERE Id = @Id;";

        var rowsAffected = await connection.ExecuteAsync(sql, new
        {
            request.Id,
            request.MakeId,
            request.Name,
            request.Abrv
        });

        return rowsAffected > 0;
    }
}
