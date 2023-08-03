using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Company.Shorts.Infrastructure.Db.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class AddingName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "files",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "files");
        }
    }
}
