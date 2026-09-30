using Microsoft.EntityFrameworkCore;
using VehicleApp.Application.Common.Interfaces;
using VehicleApp.Domain.Entities;

namespace VehicleApp.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<VehicleMake> Makes => Set<VehicleMake>();
    public DbSet<VehicleModel> Models => Set<VehicleModel>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<VehicleMake>(entity =>
        {
            entity.ToTable("VehicleMakes");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Abrv).IsRequired().HasMaxLength(10);
        });

        modelBuilder.Entity<VehicleModel>(entity =>
        {
            entity.ToTable("VehicleModels");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Abrv).IsRequired().HasMaxLength(10);
            entity.HasOne(e => e.Make)
                  .WithMany(m => m.Models)
                  .HasForeignKey(e => e.MakeId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
