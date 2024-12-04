using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Egzaminas_ZmogausRegistravimoSistema.Migrations
{
    /// <inheritdoc />
    public partial class MakePersonInfoOptional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserInfos_PersonInfos_PersonInfoId",
                table: "UserInfos");

            migrationBuilder.AlterColumn<long>(
                name: "PersonInfoId",
                table: "UserInfos",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_UserInfos_PersonInfos_PersonInfoId",
                table: "UserInfos",
                column: "PersonInfoId",
                principalTable: "PersonInfos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserInfos_PersonInfos_PersonInfoId",
                table: "UserInfos");

            migrationBuilder.AlterColumn<long>(
                name: "PersonInfoId",
                table: "UserInfos",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserInfos_PersonInfos_PersonInfoId",
                table: "UserInfos",
                column: "PersonInfoId",
                principalTable: "PersonInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
