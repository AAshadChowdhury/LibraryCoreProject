using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryCoreProject.Migrations
{
    public partial class first : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    catid = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Categoryname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.catid);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Bookcode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Bookname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    catid = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    cost = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    rate = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    picture = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Bookcode);
                    table.ForeignKey(
                        name: "FK_Books_Categories_catid",
                        column: x => x.catid,
                        principalTable: "Categories",
                        principalColumn: "catid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_catid",
                table: "Books",
                column: "catid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
