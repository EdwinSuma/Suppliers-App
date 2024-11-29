using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inventory.DataModel.Migrations
{
    /// <inheritdoc />
    public partial class AddedTransactions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "PurchaseOrderHeadersINV");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "PurchaseOrderDetailsINV");

            migrationBuilder.RenameColumn(
                name: "Qty",
                table: "PurchaseOrderDetailsINV",
                newName: "Quantity");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "PurchaseOrderDetailsINV",
                newName: "Qty");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "PurchaseOrderHeadersINV",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                table: "PurchaseOrderDetailsINV",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
