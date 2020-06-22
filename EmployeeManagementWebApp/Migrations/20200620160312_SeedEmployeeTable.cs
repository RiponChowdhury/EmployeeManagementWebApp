using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeManagementWebApp.Migrations
{
    public partial class SeedEmployeeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Department", "Email", "Mobile", "Name" },
                values: new object[] { 2, 2, "mithun.kar@gmail.com", "01515652129", "Mithun Kar" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
