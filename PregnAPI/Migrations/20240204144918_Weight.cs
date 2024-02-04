using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PregnAPI.Migrations
{
    /// <inheritdoc />
    public partial class Weight : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Weights",
                columns: table => new
                {
                    UserMail = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserWeight = table.Column<int>(type: "int", nullable: false),
                    WeightDegree = table.Column<int>(type: "int", nullable: false),
                    Difference = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WeightDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WeightHour = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weights", x => x.UserMail);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Weights");
        }
    }
}
