using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendTascly.Migrations
{
    /// <inheritdoc />
    public partial class updateentities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Projects_ProjectId",
                table: "Roles");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Projects_ProjectId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Tasks_PTaskId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "RoleUserProjects");

            migrationBuilder.DropIndex(
                name: "IX_Users_PTaskId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Roles_ProjectId",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "PTaskId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Roles");

            migrationBuilder.RenameColumn(
                name: "ProjectId",
                table: "Users",
                newName: "OrganizationId1");

            migrationBuilder.RenameIndex(
                name: "IX_Users_ProjectId",
                table: "Users",
                newName: "IX_Users_OrganizationId1");

            migrationBuilder.AddColumn<bool>(
                name: "IsSuperAdmin",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "OrganizationId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "AssigneeId",
                table: "Tasks",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "WorkspaceId",
                table: "Projects",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Organizations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Workspaces",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workspaces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Workspaces_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserWorkspace",
                columns: table => new
                {
                    MembersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkspacesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserWorkspace", x => new { x.MembersId, x.WorkspacesId });
                    table.ForeignKey(
                        name: "FK_UserWorkspace_Users_MembersId",
                        column: x => x.MembersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserWorkspace_Workspaces_WorkspacesId",
                        column: x => x.WorkspacesId,
                        principalTable: "Workspaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkspaceUserRoles",
                columns: table => new
                {
                    WorkspaceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkspaceUserRoles", x => new { x.WorkspaceId, x.UserId });
                    table.ForeignKey(
                        name: "FK_WorkspaceUserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkspaceUserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkspaceUserRoles_Workspaces_WorkspaceId",
                        column: x => x.WorkspaceId,
                        principalTable: "Workspaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_OrganizationId",
                table: "Users",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_AssigneeId",
                table: "Tasks",
                column: "AssigneeId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_WorkspaceId",
                table: "Projects",
                column: "WorkspaceId");

            migrationBuilder.CreateIndex(
                name: "IX_UserWorkspace_WorkspacesId",
                table: "UserWorkspace",
                column: "WorkspacesId");

            migrationBuilder.CreateIndex(
                name: "IX_Workspaces_OrganizationId",
                table: "Workspaces",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkspaceUserRoles_RoleId",
                table: "WorkspaceUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkspaceUserRoles_UserId",
                table: "WorkspaceUserRoles",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Workspaces_WorkspaceId",
                table: "Projects",
                column: "WorkspaceId",
                principalTable: "Workspaces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Users_AssigneeId",
                table: "Tasks",
                column: "AssigneeId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Organizations_OrganizationId",
                table: "Users",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Organizations_OrganizationId1",
                table: "Users",
                column: "OrganizationId1",
                principalTable: "Organizations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Workspaces_WorkspaceId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Users_AssigneeId",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Organizations_OrganizationId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Organizations_OrganizationId1",
                table: "Users");

            migrationBuilder.DropTable(
                name: "UserWorkspace");

            migrationBuilder.DropTable(
                name: "WorkspaceUserRoles");

            migrationBuilder.DropTable(
                name: "Workspaces");

            migrationBuilder.DropTable(
                name: "Organizations");

            migrationBuilder.DropIndex(
                name: "IX_Users_OrganizationId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_AssigneeId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Projects_WorkspaceId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "IsSuperAdmin",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AssigneeId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "WorkspaceId",
                table: "Projects");

            migrationBuilder.RenameColumn(
                name: "OrganizationId1",
                table: "Users",
                newName: "ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_OrganizationId1",
                table: "Users",
                newName: "IX_Users_ProjectId");

            migrationBuilder.AddColumn<Guid>(
                name: "PTaskId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectId",
                table: "Roles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "RoleUserProjects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleUserProjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleUserProjects_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleUserProjects_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleUserProjects_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_PTaskId",
                table: "Users",
                column: "PTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_ProjectId",
                table: "Roles",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleUserProjects_ProjectId",
                table: "RoleUserProjects",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleUserProjects_RoleId",
                table: "RoleUserProjects",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleUserProjects_UserId",
                table: "RoleUserProjects",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_Projects_ProjectId",
                table: "Roles",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Projects_ProjectId",
                table: "Users",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Tasks_PTaskId",
                table: "Users",
                column: "PTaskId",
                principalTable: "Tasks",
                principalColumn: "Id");
        }
    }
}
