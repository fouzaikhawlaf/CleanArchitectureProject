﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchitecture.FrameworksAndDrivers.Migrations
{
    /// <inheritdoc />
    public partial class AddClientType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Clients",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Clients");
        }
    }
}