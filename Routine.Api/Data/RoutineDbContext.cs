using Microsoft.EntityFrameworkCore;
using Routine.Api.Entities;
using System;

namespace Routine.Api.Data
{
    public class RoutineDbContext:DbContext
    {
        public RoutineDbContext(DbContextOptions<RoutineDbContext> options) 
            : base(options) 
        {

        }


        public DbSet<Company> Companyies { get; set; }
        public DbSet<Employee> Employees { get; set; }


        //对实体进行限制=》长度限制等等
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>()
                .Property(x => x.Name).IsRequired().HasMaxLength(100);//IsRequired必填 HasMaxLength最大长度
            modelBuilder.Entity<Company>()
                 .Property(x => x.Introduction).HasMaxLength(500);


            //modelBuilder.Entity<Employee>()
            //   .Property(x => x.)

            modelBuilder.Entity<Employee>()
               .Property(x => x.EmployeeNo).IsRequired().HasMaxLength(10);
            modelBuilder.Entity<Employee>()
               .Property(x => x.FirstName).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Employee>()
              .Property(x => x.LastName).IsRequired().HasMaxLength(50);


            //指明实体关系
            modelBuilder.Entity<Employee>()
              .HasOne(x => x.Company)
              .WithMany(x => x.Employees)
              .HasForeignKey(x => x.CompanyId).OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Company>().HasData(new Company
            {
                Id = Guid.NewGuid(),
                Name="Microsoft",
                Introduction="Great Company"
            }, new Company
            {
                Id = Guid.NewGuid(),
                Name = "Google",
                Introduction = "Don't bt evil"
            }, new Company
            {
                Id = Guid.NewGuid(),
                Name = "Alipapa",
                Introduction = "Fubao Company"
            }) ;

        }
    }
}
