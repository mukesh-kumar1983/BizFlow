using EmployeeService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeService.Infrastructure.Persistence.Configurations;

public sealed class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable("Employees");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.FirstName)
               .HasMaxLength(100)
               .IsRequired();

        builder.Property(e => e.LastName)
               .HasMaxLength(100)
               .IsRequired();

        // ✅ Email Value Object mapping
        builder.HasIndex(e => e.Email)
               .IsUnique();

        builder.Property(e => e.RowVersion)
       .IsRowVersion()
       .IsRequired();


        //builder.Property(e => e.CreatedAt)
        //       .IsRequired();
    }
}
