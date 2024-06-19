using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Services.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LogGuilds",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    IconUrl = table.Column<string>(type: "text", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogGuilds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LogUsers",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: true),
                    GlobalName = table.Column<string>(type: "text", nullable: true),
                    AvatarUrl = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LogChannels",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: true),
                    LogGuildId = table.Column<decimal>(type: "numeric(20,0)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogChannels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LogChannels_LogGuilds_LogGuildId",
                        column: x => x.LogGuildId,
                        principalTable: "LogGuilds",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LogGuildUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    LogUserId = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    LogGuildId = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    GuildNickname = table.Column<string>(type: "text", nullable: true),
                    JoinedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogGuildUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LogGuildUsers_LogGuilds_LogGuildId",
                        column: x => x.LogGuildId,
                        principalTable: "LogGuilds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LogGuildUsers_LogUsers_LogUserId",
                        column: x => x.LogUserId,
                        principalTable: "LogUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LogCommands",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    LogUserId = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    LogChannelId = table.Column<decimal>(type: "numeric(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogCommands", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LogCommands_LogChannels_LogChannelId",
                        column: x => x.LogChannelId,
                        principalTable: "LogChannels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LogCommands_LogUsers_LogUserId",
                        column: x => x.LogUserId,
                        principalTable: "LogUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LogChannels_LogGuildId",
                table: "LogChannels",
                column: "LogGuildId");

            migrationBuilder.CreateIndex(
                name: "IX_LogCommands_LogChannelId",
                table: "LogCommands",
                column: "LogChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_LogCommands_LogUserId",
                table: "LogCommands",
                column: "LogUserId");

            migrationBuilder.CreateIndex(
                name: "IX_LogGuildUsers_LogGuildId",
                table: "LogGuildUsers",
                column: "LogGuildId");

            migrationBuilder.CreateIndex(
                name: "IX_LogGuildUsers_LogUserId",
                table: "LogGuildUsers",
                column: "LogUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LogCommands");

            migrationBuilder.DropTable(
                name: "LogGuildUsers");

            migrationBuilder.DropTable(
                name: "LogChannels");

            migrationBuilder.DropTable(
                name: "LogUsers");

            migrationBuilder.DropTable(
                name: "LogGuilds");
        }
    }
}
