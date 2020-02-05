using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Routine.Api.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companyies",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Introduction = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companyies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CompanyId = table.Column<Guid>(nullable: false),
                    EmployeeNo = table.Column<string>(maxLength: 10, nullable: false),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    Gender = table.Column<int>(nullable: false),
                    DateOfBirth = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Companyies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companyies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Companyies",
                columns: new[] { "Id", "Introduction", "Name" },
                values: new object[] { new Guid("7d5c3017-d326-42e0-bac6-cdcc344fb45e"), "Great Company", "Microsoft" });

            migrationBuilder.InsertData(
                table: "Companyies",
                columns: new[] { "Id", "Introduction", "Name" },
                values: new object[] { new Guid("85ab45a6-f92f-4188-a767-09dfb1d23c64"), "Don't bt evil", "Google" });

            migrationBuilder.InsertData(
                table: "Companyies",
                columns: new[] { "Id", "Introduction", "Name" },
                values: new object[] { new Guid("0415e462-208b-4f78-8ee1-c8be2690cf77"), "Fubao Company", "Alipapa" });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_CompanyId",
                table: "Employees",
                column: "CompanyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Companyies");
        }
    }
}
