using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lesson11.Migrations
{
    /// <inheritdoc />
    public partial class Update_Customer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Customer",
                newName: "LastName");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Customer",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Customer");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Customer",
                newName: "FullName");
        }
    }
}
