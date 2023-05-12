using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Girteka.AggregationApp.Migrations
{
    /// <inheritdoc />
    public partial class testmigratio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Electricities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tinklas = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pavadinimas = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tipas = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Numeris = table.Column<int>(type: "int", nullable: false),
                    PPlus = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PlT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PMinus = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Electricities", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Electricities");
        }
    }
}
