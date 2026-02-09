using Microsoft.EntityFrameworkCore;
using EmployeeService.Domain.Entities;

namespace EmployeeService.Infrastructure.Persistence;

public sealed class BizFlowDbContext : DbContext
{
    public BizFlowDbContext(DbContextOptions<BizFlowDbContext> options)
        : base(options) { }

    public DbSet<Employee> Employees => Set<Employee>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // ✅ THIS IS THE ONLY THING YOU DO HERE
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(BizFlowDbContext).Assembly
        );
    }
}
