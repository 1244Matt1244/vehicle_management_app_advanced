using System.Data;
using VehicleApp.Application.Common.Interfaces;

namespace VehicleApp.IntegrationTests;

public class FakeDbConnectionFactory : IDbConnectionFactory
{
    public IDbConnection CreateConnection() => throw new NotImplementedException(
        "Database access should not occur in these tests.");
}
