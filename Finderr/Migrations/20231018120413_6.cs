using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Finderr.Migrations
{
    /// <inheritdoc />
    public partial class _6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupMember_UserProfile_UserProfileId",
                table: "GroupMember");

            migrationBuilder.DropTable(
                name: "UserProfile");

            migrationBuilder.AlterColumn<string>(
                name: "UserProfileId",
                table: "GroupMember",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)");

            migrationBuilder.AddColumn<string>(
                name: "current_address",
                table: "AspNetUsers",
                type: "varchar(150)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "facebook_link",
                table: "AspNetUsers",
                type: "varchar(255)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "linkedin_link",
                table: "AspNetUsers",
                type: "varchar(255)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "AspNetUsers",
                type: "varchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "occupation",
                table: "AspNetUsers",
                type: "varchar(100)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "profile_picture_reference",
                table: "AspNetUsers",
                type: "varchar(150)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "profile_privacy",
                table: "AspNetUsers",
                type: "varchar(20)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "twitter_link",
                table: "AspNetUsers",
                type: "varchar(255)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupMember_AspNetUsers_UserProfileId",
                table: "GroupMember",
                column: "UserProfileId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupMember_AspNetUsers_UserProfileId",
                table: "GroupMember");

            migrationBuilder.DropColumn(
                name: "current_address",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "facebook_link",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "linkedin_link",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "name",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "occupation",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "profile_picture_reference",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "profile_privacy",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "twitter_link",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "UserProfileId",
                table: "GroupMember",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateTable(
                name: "UserProfile",
                columns: table => new
                {
                    user_id = table.Column<string>(type: "varchar(255)", nullable: false),
                    current_address = table.Column<string>(type: "varchar(150)", nullable: false),
                    facebook_link = table.Column<string>(type: "varchar(255)", nullable: true),
                    IdentityUserId = table.Column<string>(type: "varchar(255)", nullable: false),
                    linkedin_link = table.Column<string>(type: "varchar(255)", nullable: true),
                    name = table.Column<string>(type: "varchar(50)", nullable: false),
                    occupation = table.Column<string>(type: "varchar(100)", nullable: false),
                    profile_picture_reference = table.Column<string>(type: "varchar(150)", nullable: false),
                    profile_privacy = table.Column<string>(type: "varchar(20)", nullable: false),
                    twitter_link = table.Column<string>(type: "varchar(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfile", x => x.user_id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_GroupMember_UserProfile_UserProfileId",
                table: "GroupMember",
                column: "UserProfileId",
                principalTable: "UserProfile",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
