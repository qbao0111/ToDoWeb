﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoWeb.Migrations
{
    /// <inheritdoc />
    public partial class addAddress2Column : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address2",
                table: "Student",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address2",
                table: "Student");
        }
    }
}
