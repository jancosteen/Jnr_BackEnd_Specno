using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reddit_Api.Migrations
{
    public partial class UserCommentVotes_back : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "UserCommentVotes",
                columns: table => new
                {
                    UserCommentVoteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VoteType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CommentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCommentVotes", x => x.UserCommentVoteId);
                    table.ForeignKey(
                        name: "FK_UserCommentVotes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_UserCommentVotes_Comments_CommentId",
                        column: x => x.CommentId,
                        principalTable: "Comments",
                        principalColumn: "CommentId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserCommentVotes_CommentId",
                table: "UserCommentVotes",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCommentVotes_UserId",
                table: "UserCommentVotes",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserCommentVotes");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "43c72bf5-f657-420d-963d-b66eed15edb4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9cadcd00-6472-4023-83f0-262024e8ec72");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4587cbf8-b808-40be-bdc7-84e1f528264d", "29c7c16e-3090-4246-bef1-732477913304", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c11fa764-36d0-477c-bbc7-00a8beabe142", "f640e77d-2034-4a0b-8fc6-c2adbe647d93", "Manager", "MANAGER" });
        }
    }
}
