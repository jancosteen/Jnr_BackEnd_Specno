using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reddit_Api.Migrations
{
    public partial class UserCommentVotes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserCommentVotes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4587cbf8-b808-40be-bdc7-84e1f528264d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c11fa764-36d0-477c-bbc7-00a8beabe142");

            migrationBuilder.CreateTable(
                name: "UserCommentVotes",
                columns: table => new
                {
                    UserCommentVoteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CommentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VoteType = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCommentVotes", x => x.UserCommentVoteId);
                    table.ForeignKey(
                        name: "FK_UserCommentVotes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserCommentVotes_Comments_CommentId",
                        column: x => x.CommentId,
                        principalTable: "Comments",
                        principalColumn: "CommentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "61950da7-b4bf-4ccb-8df4-5828b4ec6978", "69c3c11c-4da1-40b7-a953-1387e11b5941", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "911c4838-2b96-42c7-b58e-dc0829004e8a", "e0e96a85-fe69-46b6-be82-007b35a35270", "Manager", "MANAGER" });

            migrationBuilder.CreateIndex(
                name: "IX_UserCommentVotes_CommentId",
                table: "UserCommentVotes",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCommentVotes_UserId",
                table: "UserCommentVotes",
                column: "UserId");
        }
    }
}
