using Microsoft.EntityFrameworkCore;
using Routine.Api.Entities;
using System;

namespace Routine.Api.Data
{
    public class RoutineDbContext : DbContext
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
                Id = Guid.Parse("7D5C3017-D326-42E0-BAC6-CDCC344FB45E"),
                Name = "Microsoft",
                Introduction = "Great Company"
            }, new Company
            {
                Id = Guid.Parse("85AB45A6-F92F-4188-A767-09DFB1D23C64"),
                Name = "Google",
                Introduction = "Don't bt evil"
            }, new Company
            {
                Id = Guid.Parse("0415E462-208B-4F78-8EE1-C8BE2690CF77"),
                Name = "Alipapa",
                Introduction = "Fubao Company"
            });



            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = Guid.Parse("2CDCE0ED-2391-4159-A22F-35018000EDE7"),
                    CompanyId = Guid.Parse("7D5C3017-D326-42E0-BAC6-CDCC344FB45E"),
                    DateOfBirth = new DateTime(1976, 1, 2),
                    EmployeeNo = "MSFT231",
                    FirstName = "Nick",
                    LastName = "Carter",
                    Gender = Gender.男
                },
                  new Employee
                  {
                      Id = Guid.Parse("7A4CB816-3FED-4FC3-88FF-F6342666D8FD"),
                      CompanyId = Guid.Parse("85AB45A6-F92F-4188-A767-09DFB1D23C64"),
                      DateOfBirth = new DateTime(1996, 1, 2),
                      EmployeeNo = "MSFT232",
                      FirstName = "NSick",
                      LastName = "CSarter",
                      Gender = Gender.女
                  },
                    new Employee
                    {
                        Id = Guid.Parse("B5DD6783-CCB3-42B1-9F43-9678A6FD8C0E"),
                        CompanyId = Guid.Parse("0415E462-208B-4F78-8EE1-C8BE2690CF77"),
                        DateOfBirth = new DateTime(2000, 1, 2),
                        EmployeeNo = "MSFTQ",
                        FirstName = "QNick",
                        LastName = "QCarter",
                        Gender = Gender.男
                    }
                );

        }
    }
}
