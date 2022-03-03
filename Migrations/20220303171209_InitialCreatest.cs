using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LocalProduceApp.Migrations
{
    public partial class InitialCreatest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserPickupPlace",
                table: "Customer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserPickupPlace",
                table: "Customer",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
