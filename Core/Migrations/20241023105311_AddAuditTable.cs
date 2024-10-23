using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurveyBasket.API.Core.Migrations
{
    /// <inheritdoc />
    public partial class AddAuditTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefreshToken");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Polls",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserId",
                table: "Polls",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Polls",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedByUserId",
                table: "Polls",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RevokedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExpireOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => new { x.UserId, x.Id });
                    table.ForeignKey(
                        name: "FK_RefreshTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Polls_CreatedByUserId",
                table: "Polls",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Polls_UpdatedByUserId",
                table: "Polls",
                column: "UpdatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Polls_AspNetUsers_CreatedByUserId",
                table: "Polls",
                column: "CreatedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Polls_AspNetUsers_UpdatedByUserId",
                table: "Polls",
                column: "UpdatedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Polls_AspNetUsers_CreatedByUserId",
                table: "Polls");

            migrationBuilder.DropForeignKey(
                name: "FK_Polls_AspNetUsers_UpdatedByUserId",
                table: "Polls");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropIndex(
                name: "IX_Polls_CreatedByUserId",
                table: "Polls");

            migrationBuilder.DropIndex(
                name: "IX_Polls_UpdatedByUserId",
                table: "Polls");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Polls");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Polls");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Polls");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "Polls");

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpireOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RevokedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => new { x.UserId, x.Id });
                    table.ForeignKey(
                        name: "FK_RefreshToken_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }
    }
}
