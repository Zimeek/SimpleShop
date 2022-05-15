using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleShop.Infrastructure.Migrations
{
    public partial class addedOrdersSizes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Size",
                table: "OrderItems",
                nullable: false
            );

            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
        }
    }
}
