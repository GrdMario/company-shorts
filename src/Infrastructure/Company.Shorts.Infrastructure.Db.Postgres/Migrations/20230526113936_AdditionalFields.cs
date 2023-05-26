#nullable disable

namespace Company.Shorts.Infrastructure.Db.Postgres.Migrations
{
    using System;
    using Microsoft.EntityFrameworkCore.Migrations;

    /// <inheritdoc />
    public partial class AdditionalFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Boolean",
                table: "users",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "Decimal",
                table: "users",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Integer",
                table: "users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "Long",
                table: "users",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Boolean",
                table: "users");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "users");

            migrationBuilder.DropColumn(
                name: "Decimal",
                table: "users");

            migrationBuilder.DropColumn(
                name: "Integer",
                table: "users");

            migrationBuilder.DropColumn(
                name: "Long",
                table: "users");
        }
    }
}
