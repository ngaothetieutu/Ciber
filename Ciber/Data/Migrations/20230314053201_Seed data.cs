using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ciber.Data.Migrations
{
    public partial class Seeddata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Price",
                table: "Product",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 1, "Các sản phẩm TV", "Ti Vi" });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 2, "Các sản phẩm Tủ lạnh", "Tủ lạnh" });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 3, "Các sản phẩm máy tính", "Máy tính" });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "CategoryId", "Description", "Name", "Price", "Quantity" },
                values: new object[,]
                {
                    { 1, 1, "TV SamSung SS1", "Ti Vi SamSung", 10000000, 5 },
                    { 2, 1, "TV LG", "Ti Vi LG", 9000000, 4 },
                    { 3, 1, "TV Sony", "Ti Vi Sony", 11000000, 3 },
                    { 4, 2, "Tủ lạnh Panasonic L110", "Tủ lạnh Panasonic", 15000000, 3 },
                    { 5, 2, "Tủ lạnh Panasonic L150", "Tủ lạnh Panasonic", 18000000, 3 },
                    { 6, 2, "Tủ lạnh LG L110", "Tủ lạnh LG", 11000000, 6 },
                    { 7, 3, "Laptop gaming assus", "Laptop Gaming Assus", 11000000, 5 },
                    { 8, 3, "Laptop Dell", "Laptop Dell v12", 14000000, 2 },
                    { 9, 3, "Laptop Lenovo", "Laptop Lenovo", 15000000, 6 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Product",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
