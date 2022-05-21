using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleShop.Infrastructure.Migrations
{
    public partial class renamedOrderDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address",
                table: "OrderDetails",
                newName: "Street");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Street",
                table: "OrderDetails",
                newName: "Address");
        }
    }
}
