using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Services.Migrations
{
    /// <inheritdoc />
    public partial class AuditableEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "LogGuilds");

            migrationBuilder.AddColumn<DateTime>(
                name: "EntityCreatedAt",
                table: "LogUsers",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EntityModifiedAt",
                table: "LogUsers",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EntityCreatedAt",
                table: "LogGuilds",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EntityModifiedAt",
                table: "LogGuilds",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EntityCreatedAt",
                table: "LogCommands",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EntityModifiedAt",
                table: "LogCommands",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EntityCreatedAt",
                table: "LogChannels",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EntityModifiedAt",
                table: "LogChannels",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EntityCreatedAt",
                table: "LogUsers");

            migrationBuilder.DropColumn(
                name: "EntityModifiedAt",
                table: "LogUsers");

            migrationBuilder.DropColumn(
                name: "EntityCreatedAt",
                table: "LogGuilds");

            migrationBuilder.DropColumn(
                name: "EntityModifiedAt",
                table: "LogGuilds");

            migrationBuilder.DropColumn(
                name: "EntityCreatedAt",
                table: "LogCommands");

            migrationBuilder.DropColumn(
                name: "EntityModifiedAt",
                table: "LogCommands");

            migrationBuilder.DropColumn(
                name: "EntityCreatedAt",
                table: "LogChannels");

            migrationBuilder.DropColumn(
                name: "EntityModifiedAt",
                table: "LogChannels");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "LogGuilds",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
