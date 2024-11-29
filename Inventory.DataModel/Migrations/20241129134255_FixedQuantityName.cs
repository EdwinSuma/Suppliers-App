using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inventory.DataModel.Migrations
{
    /// <inheritdoc />
    public partial class FixedQuantityName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "PurchaseOrderDetailsINV",
                newName: "Qty");

            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                table: "PurchaseOrderDetailsINV",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "PurchaseOrderDetailsINV");

            migrationBuilder.RenameColumn(
                name: "Qty",
                table: "PurchaseOrderDetailsINV",
                newName: "Quantity");
        }
    }
}
