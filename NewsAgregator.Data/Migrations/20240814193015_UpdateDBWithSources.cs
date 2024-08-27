using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsAgregator.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDBWithSources : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Percipittaion",
                table: "Weathers",
                newName: "Percipitaion");

            migrationBuilder.AddColumn<Guid>(
                name: "SourceId",
                table: "Newses",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SourceUrl",
                table: "Newses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Sources",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaseUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RssUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sources", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Newses_SourceId",
                table: "Newses",
                column: "SourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Newses_Sources_SourceId",
                table: "Newses",
                column: "SourceId",
                principalTable: "Sources",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Newses_Sources_SourceId",
                table: "Newses");

            migrationBuilder.DropTable(
                name: "Sources");

            migrationBuilder.DropIndex(
                name: "IX_Newses_SourceId",
                table: "Newses");

            migrationBuilder.DropColumn(
                name: "SourceId",
                table: "Newses");

            migrationBuilder.DropColumn(
                name: "SourceUrl",
                table: "Newses");

            migrationBuilder.RenameColumn(
                name: "Percipitaion",
                table: "Weathers",
                newName: "Percipittaion");
        }
    }
}
