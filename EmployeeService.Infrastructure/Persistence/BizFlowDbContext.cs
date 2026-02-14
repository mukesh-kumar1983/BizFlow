using Microsoft.EntityFrameworkCore;
using EmployeeService.Domain.Entities;
using EmployeeService.Application.Common.Interfaces;

namespace EmployeeService.Infrastructure.Persistence;

public sealed class BizFlowDbContext : DbContext, IApplicationDbContext
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
