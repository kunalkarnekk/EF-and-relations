using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodingWiki_DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addFluent_BookToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fluent_Books",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ISBN = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BookDetail_Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fluent_Books", x => x.BookId);
                    table.ForeignKey(
                        name: "FK_Fluent_Books_Fluent_BookDetails_BookDetail_Id",
                        column: x => x.BookDetail_Id,
                        principalTable: "Fluent_BookDetails",
                        principalColumn: "BookDetail_Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fluent_Books_BookDetail_Id",
                table: "Fluent_Books",
                column: "BookDetail_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fluent_Books");
        }
    }
}
