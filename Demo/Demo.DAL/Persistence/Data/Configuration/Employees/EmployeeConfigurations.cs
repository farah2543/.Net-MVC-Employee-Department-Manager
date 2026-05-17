using Demo.DAL.Entities.Common.Enums;
using Demo.DAL.Entities.Employees;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Persistence.Data.Configuration.Employees
{
    internal class EmployeeConfigurations : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(E => E.Name).HasColumnType("varchar(50)").IsRequired();
            builder.Property(E => E.Address).HasColumnType("varchar(50)");
            builder.Property(E => E.salary).HasColumnType("decimal(8,2)");

            builder.Property(E => E.Gender)
                .HasConversion(
                (gender) => gender.ToString(),
                (gender) => (Gender) Enum.Parse(typeof(Gender),gender)
                );

            builder.Property(E => E.EmployeeType)
              .HasConversion(
              (employeeType) => employeeType.ToString(),
              (employeeType) => (EmployeeType)Enum.Parse(typeof(EmployeeType), employeeType)
              );

            builder.Property(D => D.LastModifiedOn).HasComputedColumnSql("GETDATE()");
            builder.Property(D => D.CreateOn).HasDefaultValueSql("GETDATE()");


        }
    }
}
