using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeUserPassword : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("695f4b69-d2b2-4639-be9b-39ed40ec3a98"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "PasswordHash", "Role", "Username" },
                values: new object[] { new Guid("856c622f-f9ce-4d9b-bfe3-457114548d33"), "cb0bfafccd459d065b94d9ea7ab7473667e48828dc34cc10e72b935303c9cd21", 0, "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("856c622f-f9ce-4d9b-bfe3-457114548d33"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "PasswordHash", "Role", "Username" },
                values: new object[] { new Guid("695f4b69-d2b2-4639-be9b-39ed40ec3a98"), "240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9", 0, "admin" });
        }
    }
}
