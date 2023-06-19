using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MaterApp.Migrations
{
    /// <inheritdoc />
    public partial class ThirdMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "DateOfBirth", "Email", "FirstName", "Gender", "LastName", "Password", "PasswordHash", "ProfilePhoto", "Salt", "Username" },
                values: new object[,]
                {
                    { 1, null, new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "tom@example.com", "Tom", 0, "Smith", "password1", "", null, "", "Tom" },
                    { 2, null, new DateTime(1985, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "bob@example.com", "Bob", 0, "Johnson", "password2", "", null, "", "Bob" },
                    { 3, null, new DateTime(1995, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "sam@example.com", "Sam", 1, "Brown", "password3", "", null, "", "Sam" }
                });
        }
    }
}
