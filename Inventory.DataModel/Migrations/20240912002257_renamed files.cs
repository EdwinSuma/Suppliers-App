using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Supplier.DataModel.Migrations
{
    /// <inheritdoc />
    public partial class renamedfiles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SuppliersINV",
                columns: table => new
                {
                    SupplierID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: false),
                    Representative = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: false),
                    ContactNo = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuppliersINV", x => x.SupplierID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SuppliersINV");
        }
    }
}
