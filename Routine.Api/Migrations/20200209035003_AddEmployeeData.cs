using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Routine.Api.Migrations
{
    public partial class AddEmployeeData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CompanyId", "DateOfBirth", "EmployeeNo", "FirstName", "Gender", "LastName" },
                values: new object[] { new Guid("d847cb79-a94f-486a-a41d-34155923e402"), new Guid("7d5c3017-d326-42e0-bac6-cdcc344fb45e"), new DateTime(1976, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "MSFT231", "Nick", 1, "Carter" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CompanyId", "DateOfBirth", "EmployeeNo", "FirstName", "Gender", "LastName" },
                values: new object[] { new Guid("7e714c45-0d71-46cd-be81-e8dbc41c1a91"), new Guid("85ab45a6-f92f-4188-a767-09dfb1d23c64"), new DateTime(1996, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "MSFT232", "NSick", 2, "CSarter" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CompanyId", "DateOfBirth", "EmployeeNo", "FirstName", "Gender", "LastName" },
                values: new object[] { new Guid("14caa6cd-7631-47ed-aa4d-c0f06c5b551f"), new Guid("0415e462-208b-4f78-8ee1-c8be2690cf77"), new DateTime(2000, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "MSFTQ", "QNick", 1, "QCarter" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("14caa6cd-7631-47ed-aa4d-c0f06c5b551f"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("7e714c45-0d71-46cd-be81-e8dbc41c1a91"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("d847cb79-a94f-486a-a41d-34155923e402"));
        }
    }
}
