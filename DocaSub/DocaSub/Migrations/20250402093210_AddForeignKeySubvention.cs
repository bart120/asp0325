using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DocaSub.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignKeySubvention : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubventionId",
                schema: "doca",
                table: "sub_requests",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateIndex(
                name: "IX_sub_requests_SubventionId",
                schema: "doca",
                table: "sub_requests",
                column: "SubventionId");

            migrationBuilder.AddForeignKey(
                name: "FK_sub_requests_Subventions_SubventionId",
                schema: "doca",
                table: "sub_requests",
                column: "SubventionId",
                principalSchema: "doca",
                principalTable: "Subventions",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_sub_requests_Subventions_SubventionId",
                schema: "doca",
                table: "sub_requests");

            migrationBuilder.DropIndex(
                name: "IX_sub_requests_SubventionId",
                schema: "doca",
                table: "sub_requests");

            migrationBuilder.DropColumn(
                name: "SubventionId",
                schema: "doca",
                table: "sub_requests");
        }
    }
}
