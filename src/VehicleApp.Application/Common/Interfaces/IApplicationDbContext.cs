using Microsoft.EntityFrameworkCore;
using VehicleApp.Domain.Entities;

namespace VehicleApp.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<VehicleMake> Makes { get; }
    DbSet<VehicleModel> Models { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
