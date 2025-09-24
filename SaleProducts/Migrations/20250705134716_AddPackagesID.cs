using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SaleProducts.Migrations
{
    /// <inheritdoc />
    public partial class AddPackagesID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Packages",
                newName: "PackageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PackageId",
                table: "Packages",
                newName: "ProductId");
        }
    }
}
