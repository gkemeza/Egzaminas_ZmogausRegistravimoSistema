using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Egzaminas_ZmogausRegistravimoSistema.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePersonInfosTablePersonalIdAndPhoneNumberToTypeLong : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "PhoneNumber",
                table: "PersonInfos",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "PersonalId",
                table: "PersonInfos",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PhoneNumber",
                table: "PersonInfos",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "PersonalId",
                table: "PersonInfos",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }
    }
}
