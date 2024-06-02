using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookShop.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserDetails_Addresses_UserId",
                table: "UserDetails");

            migrationBuilder.CreateIndex(
                name: "IX_UserDetails_AddressId",
                table: "UserDetails",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserDetails_Addresses_AddressId",
                table: "UserDetails",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserDetails_Addresses_AddressId",
                table: "UserDetails");

            migrationBuilder.DropIndex(
                name: "IX_UserDetails_AddressId",
                table: "UserDetails");

            migrationBuilder.AddForeignKey(
                name: "FK_UserDetails_Addresses_UserId",
                table: "UserDetails",
                column: "UserId",
                principalTable: "Addresses",
                principalColumn: "Id");
        }
    }
}
