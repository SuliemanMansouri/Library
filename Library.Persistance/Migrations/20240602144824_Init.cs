using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Library.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(24)", maxLength: 24, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ISBN = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    NumberOfPages = table.Column<short>(type: "smallint", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuthorBook",
                columns: table => new
                {
                    AuthorsId = table.Column<int>(type: "int", nullable: false),
                    BooksId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorBook", x => new { x.AuthorsId, x.BooksId });
                    table.ForeignKey(
                        name: "FK_AuthorBook_Authors_AuthorsId",
                        column: x => x.AuthorsId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuthorBook_Books_BooksId",
                        column: x => x.BooksId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Email", "Name", "Phone" },
                values: new object[,]
                {
                    { 1, "johndoe@example.com", "John Doe", "123-456-7890" },
                    { 2, "janesmith@example.com", "Jane Smith", "234-567-8901" },
                    { 3, "alicejohnson@example.com", "Alice Johnson", "345-678-9012" },
                    { 4, "bobbrown@example.com", "Bob Brown", "456-789-0123" },
                    { 5, "charliedavis@example.com", "Charlie Davis", "567-890-1234" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "ISBN", "Language", "NumberOfPages", "Title" },
                values: new object[,]
                {
                    { 1, "978-3-16-148410-0", "English", (short)320, "Book One" },
                    { 2, "978-1-56619-909-4", "Spanish", (short)250, "Book Two" },
                    { 3, "978-0-12345-678-9", "French", (short)150, "Book Three" },
                    { 4, "978-0-54321-987-6", "German", (short)450, "Book Four" },
                    { 5, "978-1-23456-789-0", "English", (short)200, "Book Five" },
                    { 6, "978-1-11111-222-2", "Spanish", (short)180, "Book Six" },
                    { 7, "978-2-22222-333-3", "French", (short)220, "Book Seven" },
                    { 8, "978-3-33333-444-4", "German", (short)300, "Book Eight" },
                    { 9, "978-4-44444-555-5", "English", (short)320, "Book Nine" },
                    { 10, "978-5-55555-666-6", "Spanish", (short)400, "Book Ten" },
                    { 11, "978-6-66666-777-7", "French", (short)150, "Book Eleven" },
                    { 12, "978-7-77777-888-8", "German", (short)450, "Book Twelve" },
                    { 13, "978-8-88888-999-9", "English", (short)200, "Book Thirteen" },
                    { 14, "978-9-99999-000-0", "Spanish", (short)180, "Book Fourteen" },
                    { 15, "978-1-01010-101-1", "French", (short)220, "Book Fifteen" },
                    { 16, "978-2-02020-202-2", "German", (short)300, "Book Sixteen" },
                    { 17, "978-3-03030-303-3", "English", (short)320, "Book Seventeen" },
                    { 18, "978-4-04040-404-4", "Spanish", (short)400, "Book Eighteen" },
                    { 19, "978-5-05050-505-5", "French", (short)150, "Book Nineteen" },
                    { 20, "978-6-06060-606-6", "German", (short)450, "Book Twenty" }
                });

            migrationBuilder.InsertData(
                table: "AuthorBook",
                columns: new[] { "AuthorsId", "BooksId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 11 },
                    { 1, 16 },
                    { 2, 3 },
                    { 2, 4 },
                    { 2, 12 },
                    { 2, 17 },
                    { 3, 5 },
                    { 3, 6 },
                    { 3, 13 },
                    { 3, 18 },
                    { 4, 7 },
                    { 4, 8 },
                    { 4, 14 },
                    { 4, 19 },
                    { 5, 9 },
                    { 5, 10 },
                    { 5, 15 },
                    { 5, 20 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuthorBook_BooksId",
                table: "AuthorBook",
                column: "BooksId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthorBook");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Books");
        }
    }
}
