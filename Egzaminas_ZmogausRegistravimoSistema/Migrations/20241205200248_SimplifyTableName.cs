using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Egzaminas_ZmogausRegistravimoSistema.Migrations
{
    /// <inheritdoc />
    public partial class SimplifyTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserInfos_PersonInfos_PersonInfoId",
                table: "UserInfos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserInfos",
                table: "UserInfos");

            migrationBuilder.RenameTable(
                name: "UserInfos",
                newName: "Users");

            migrationBuilder.RenameIndex(
                name: "IX_UserInfos_PersonInfoId",
                table: "Users",
                newName: "IX_Users_PersonInfoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_PersonInfos_PersonInfoId",
                table: "Users",
                column: "PersonInfoId",
                principalTable: "PersonInfos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_PersonInfos_PersonInfoId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "UserInfos");

            migrationBuilder.RenameIndex(
                name: "IX_Users_PersonInfoId",
                table: "UserInfos",
                newName: "IX_UserInfos_PersonInfoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserInfos",
                table: "UserInfos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserInfos_PersonInfos_PersonInfoId",
                table: "UserInfos",
                column: "PersonInfoId",
                principalTable: "PersonInfos",
                principalColumn: "Id");
        }
    }
}
