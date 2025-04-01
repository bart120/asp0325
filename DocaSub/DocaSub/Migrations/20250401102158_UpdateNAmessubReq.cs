using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DocaSub.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNAmessubReq : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SubRequests",
                schema: "doca",
                table: "SubRequests");

            migrationBuilder.RenameTable(
                name: "SubRequests",
                schema: "doca",
                newName: "sub_requests",
                newSchema: "doca");

            migrationBuilder.RenameColumn(
                name: "Title",
                schema: "doca",
                table: "sub_requests",
                newName: "titre");

            migrationBuilder.AddPrimaryKey(
                name: "PK_sub_requests",
                schema: "doca",
                table: "sub_requests",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_sub_requests",
                schema: "doca",
                table: "sub_requests");

            migrationBuilder.RenameTable(
                name: "sub_requests",
                schema: "doca",
                newName: "SubRequests",
                newSchema: "doca");

            migrationBuilder.RenameColumn(
                name: "titre",
                schema: "doca",
                table: "SubRequests",
                newName: "Title");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubRequests",
                schema: "doca",
                table: "SubRequests",
                column: "Id");
        }
    }
}
