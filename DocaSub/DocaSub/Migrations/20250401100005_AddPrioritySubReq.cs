using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DocaSub.Migrations
{
    /// <inheritdoc />
    public partial class AddPrioritySubReq : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Priority",
                table: "SubRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Priority",
                table: "SubRequests");
        }
    }
}
