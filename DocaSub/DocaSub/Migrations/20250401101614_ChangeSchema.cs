using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DocaSub.Migrations
{
    /// <inheritdoc />
    public partial class ChangeSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "doca");

            migrationBuilder.RenameTable(
                name: "SubRequests",
                newName: "SubRequests",
                newSchema: "doca");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "SubRequests",
                schema: "doca",
                newName: "SubRequests");
        }
    }
}
