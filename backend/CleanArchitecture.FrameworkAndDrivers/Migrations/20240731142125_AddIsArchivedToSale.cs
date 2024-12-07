﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchitecture.FrameworksAndDrivers.Migrations
{
    /// <inheritdoc />
    public partial class AddIsArchivedToSale : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsArchived",
                table: "Sales",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsArchived",
                table: "Sales");
        }
    }
}
