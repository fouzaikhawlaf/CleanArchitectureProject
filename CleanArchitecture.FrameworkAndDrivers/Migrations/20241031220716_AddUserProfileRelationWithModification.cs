using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchitecture.FrameworkAndDrivers.Migrations
{
    /// <inheritdoc />
    public partial class AddUserProfileRelationWithModification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Employee",
                table: "UserProfiles",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Employee",
                table: "UserProfiles");
        }
    }
}
