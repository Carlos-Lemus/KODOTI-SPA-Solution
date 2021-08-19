using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Database.Migrations
{
    public partial class InsertDefaultRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "286ad301-88a6-4000-8bc0-f7416467451b", "01d8f813-c23a-4944-8b70-954cf1894fdc", "ApplicationRole", "Admin", "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[] { "150717c6-7200-4c54-bbc2-5c16b47586cf", "33670645-a97f-4649-9aa2-45b71154109b", "ApplicationRole", "Seller", "Seller" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "150717c6-7200-4c54-bbc2-5c16b47586cf");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "286ad301-88a6-4000-8bc0-f7416467451b");
        }
    }
}
