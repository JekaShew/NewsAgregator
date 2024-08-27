using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsAgregator.Data.Migrations
{
    /// <inheritdoc />
    public partial class SomeChangesInNews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Newses",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Newses");
        }
    }
}
