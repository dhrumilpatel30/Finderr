using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Finderr.Migrations
{
    /// <inheritdoc />
    public partial class _5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserProfile",
                columns: table => new
                {
                    user_id = table.Column<string>(type: "varchar(255)", nullable: false),
                    IdentityUserId = table.Column<string>(type: "varchar(255)", nullable: false),
                    name = table.Column<string>(type: "varchar(50)", nullable: false),
                    occupation = table.Column<string>(type: "varchar(100)", nullable: false),
                    current_address = table.Column<string>(type: "varchar(150)", nullable: false),
                    profile_privacy = table.Column<string>(type: "varchar(20)", nullable: false),
                    profile_picture_reference = table.Column<string>(type: "varchar(150)", nullable: false),
                    facebook_link = table.Column<string>(type: "varchar(255)", nullable: true),
                    twitter_link = table.Column<string>(type: "varchar(255)", nullable: true),
                    linkedin_link = table.Column<string>(type: "varchar(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfile", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "GroupMember",
                columns: table => new
                {
                    GroupMemberId = table.Column<string>(type: "text", nullable: false),
                    is_admin = table.Column<string>(type: "varchar(10)", nullable: false),
                    join_date = table.Column<DateOnly>(type: "date", nullable: false),
                    UserProfileId = table.Column<string>(type: "varchar(255)", nullable: false),
                    GroupId = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupMember", x => x.GroupMemberId);
                    table.ForeignKey(
                        name: "FK_GroupMember_Group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Group",
                        principalColumn: "group_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupMember_UserProfile_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfile",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupMember_GroupId",
                table: "GroupMember",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupMember_UserProfileId",
                table: "GroupMember",
                column: "UserProfileId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupMember");

            migrationBuilder.DropTable(
                name: "UserProfile");
        }
    }
}
