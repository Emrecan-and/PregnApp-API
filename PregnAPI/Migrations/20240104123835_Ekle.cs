using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PregnAPI.Migrations
{
    /// <inheritdoc />
    public partial class Ekle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserMail", "ReglDate", "UserName", "UserPassword", "UserWeight" },
                values: new object[,]
                {
                    { "emmr123@gmail.com", new DateTime(2022, 1, 2, 15, 30, 0, 0, DateTimeKind.Unspecified), "Furkan", "12345", 50 },
                    { "emmrecan.erkus0@gmail.com", new DateTime(2022, 1, 3, 15, 30, 0, 0, DateTimeKind.Unspecified), "Emrecan", "12345", 97 },
                    { "emmrecanreis23@gmail.com", new DateTime(2022, 1, 4, 15, 30, 0, 0, DateTimeKind.Unspecified), "Elmas", "12345", 47 },
                    { "mert.kerim.mert@gmail.com", new DateTime(2022, 4, 3, 15, 30, 0, 0, DateTimeKind.Unspecified), "FurkanKömürlü", "12345", 67 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserMail",
                keyValue: "emmr123@gmail.com");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserMail",
                keyValue: "emmrecan.erkus0@gmail.com");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserMail",
                keyValue: "emmrecanreis23@gmail.com");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserMail",
                keyValue: "mert.kerim.mert@gmail.com");
        }
    }
}
