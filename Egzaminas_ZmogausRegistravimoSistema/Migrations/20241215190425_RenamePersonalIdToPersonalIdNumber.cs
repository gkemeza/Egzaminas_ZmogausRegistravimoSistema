using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Egzaminas_ZmogausRegistravimoSistema.Migrations
{
    /// <inheritdoc />
    public partial class RenamePersonalIdToPersonalIdNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PersonalId",
                table: "PersonInfos",
                newName: "PersonalIdNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PersonalIdNumber",
                table: "PersonInfos",
                newName: "PersonalId");
        }
    }
}
