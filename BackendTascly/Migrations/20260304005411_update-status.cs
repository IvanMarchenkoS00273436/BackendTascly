using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendTascly.Migrations
{
    /// <inheritdoc />
    public partial class updatestatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "NextStatusId",
                table: "TaskStatuses",
                type: "smallint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaskStatuses_NextStatusId",
                table: "TaskStatuses",
                column: "NextStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskStatuses_TaskStatuses_NextStatusId",
                table: "TaskStatuses",
                column: "NextStatusId",
                principalTable: "TaskStatuses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskStatuses_TaskStatuses_NextStatusId",
                table: "TaskStatuses");

            migrationBuilder.DropIndex(
                name: "IX_TaskStatuses_NextStatusId",
                table: "TaskStatuses");

            migrationBuilder.DropColumn(
                name: "NextStatusId",
                table: "TaskStatuses");
        }
    }
}
