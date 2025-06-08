﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Roomies.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ExtendHouseholdWithDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Households",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Households");
        }
    }
}
