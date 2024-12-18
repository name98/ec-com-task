using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EdCom.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    order = table.Column<int>(type: "integer", nullable: false),
                    date_created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_categories", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "purchases",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    price = table.Column<decimal>(type: "numeric", nullable: false),
                    category_id = table.Column<Guid>(type: "uuid", nullable: false),
                    date_of_purchase = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    comment = table.Column<string>(type: "text", nullable: true),
                    date_created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_purchases", x => x.id);
                    table.ForeignKey(
                        name: "fk_purchases_categories_category_id",
                        column: x => x.category_id,
                        principalTable: "categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "id", "date_created", "order", "title" },
                values: new object[,]
                {
                    { new Guid("6548bc25-0cb2-4fec-be40-d296ab261ec9"), new DateTime(2024, 12, 18, 0, 0, 0, 0, DateTimeKind.Utc), 500, "Entertainment" },
                    { new Guid("94efe18c-e82c-48a1-b4ee-96921b3f2ea0"), new DateTime(2024, 12, 18, 0, 0, 0, 0, DateTimeKind.Utc), 200, "Transport" },
                    { new Guid("d14e7c25-5351-47d6-a614-96efa7497dc2"), new DateTime(2024, 12, 18, 0, 0, 0, 0, DateTimeKind.Utc), 100, "Food" },
                    { new Guid("d5d150a6-0b9b-4d44-aed9-ae42cca848b5"), new DateTime(2024, 12, 18, 0, 0, 0, 0, DateTimeKind.Utc), 400, "Internet" },
                    { new Guid("e2a18bf7-01e3-47cf-baf7-944e8b759198"), new DateTime(2024, 12, 18, 0, 0, 0, 0, DateTimeKind.Utc), 300, "Mobile Network" }
                });

            migrationBuilder.CreateIndex(
                name: "ix_categories_title",
                table: "categories",
                column: "title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_purchases_category_id",
                table: "purchases",
                column: "category_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "purchases");

            migrationBuilder.DropTable(
                name: "categories");
        }
    }
}
