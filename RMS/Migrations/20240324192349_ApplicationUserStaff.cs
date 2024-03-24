using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class ApplicationUserStaff : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "user_id",
                schema: "sales",
                table: "staffs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "staff_id",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_staff_id",
                table: "AspNetUsers",
                column: "staff_id",
                unique: true,
                filter: "[staff_id] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_staffs_staff_id",
                table: "AspNetUsers",
                column: "staff_id",
                principalSchema: "sales",
                principalTable: "staffs",
                principalColumn: "staff_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_staffs_staff_id",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_staff_id",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "user_id",
                schema: "sales",
                table: "staffs");

            migrationBuilder.DropColumn(
                name: "staff_id",
                table: "AspNetUsers");
        }
    }
}
