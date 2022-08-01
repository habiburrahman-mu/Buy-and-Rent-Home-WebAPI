using Microsoft.EntityFrameworkCore.Migrations;

namespace BuyandRentHomeWebAPI.Migrations
{
    public partial class UpdateUserTableEmailMobile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Users",
                nullable: false,
                defaultValue: "abc@test.com");

            migrationBuilder.AddColumn<string>(
                name: "Mobile",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Mobile",
                table: "Users");
        }
    }
}
