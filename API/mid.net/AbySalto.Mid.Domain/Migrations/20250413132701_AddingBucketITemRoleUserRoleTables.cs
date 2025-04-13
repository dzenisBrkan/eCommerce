using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AbySalto.Mid.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddingBucketITemRoleUserRoleTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buckets_Products_ProductId",
                table: "Buckets");

            migrationBuilder.DropIndex(
                name: "IX_Buckets_ProductId",
                table: "Buckets");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Buckets");

            migrationBuilder.AddColumn<int>(
                name: "BucketId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Total",
                table: "Buckets",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "TotalProducts",
                table: "Buckets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalQuantity",
                table: "Buckets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Products_BucketId",
                table: "Products",
                column: "BucketId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Buckets_BucketId",
                table: "Products",
                column: "BucketId",
                principalTable: "Buckets",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Buckets_BucketId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_BucketId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "BucketId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "Buckets");

            migrationBuilder.DropColumn(
                name: "TotalProducts",
                table: "Buckets");

            migrationBuilder.DropColumn(
                name: "TotalQuantity",
                table: "Buckets");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Buckets",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Buckets_ProductId",
                table: "Buckets",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Buckets_Products_ProductId",
                table: "Buckets",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }
    }
}
