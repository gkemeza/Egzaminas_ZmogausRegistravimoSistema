using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Egzaminas_ZmogausRegistravimoSistema.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonInfos_Residences_ResidenceId",
                table: "PersonInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_PersonInfos_PersonInfoId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_PersonInfoId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_PersonInfos_ResidenceId",
                table: "PersonInfos");

            migrationBuilder.DropColumn(
                name: "PersonInfoId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ResidenceId",
                table: "PersonInfos");

            migrationBuilder.AddColumn<long>(
                name: "PersonInfoId",
                table: "Residences",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "PersonInfos",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Residences_PersonInfoId",
                table: "Residences",
                column: "PersonInfoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonInfos_UserId",
                table: "PersonInfos",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonInfos_Users_UserId",
                table: "PersonInfos",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Residences_PersonInfos_PersonInfoId",
                table: "Residences",
                column: "PersonInfoId",
                principalTable: "PersonInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonInfos_Users_UserId",
                table: "PersonInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_Residences_PersonInfos_PersonInfoId",
                table: "Residences");

            migrationBuilder.DropIndex(
                name: "IX_Residences_PersonInfoId",
                table: "Residences");

            migrationBuilder.DropIndex(
                name: "IX_PersonInfos_UserId",
                table: "PersonInfos");

            migrationBuilder.DropColumn(
                name: "PersonInfoId",
                table: "Residences");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "PersonInfos");

            migrationBuilder.AddColumn<long>(
                name: "PersonInfoId",
                table: "Users",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ResidenceId",
                table: "PersonInfos",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Users_PersonInfoId",
                table: "Users",
                column: "PersonInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonInfos_ResidenceId",
                table: "PersonInfos",
                column: "ResidenceId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonInfos_Residences_ResidenceId",
                table: "PersonInfos",
                column: "ResidenceId",
                principalTable: "Residences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_PersonInfos_PersonInfoId",
                table: "Users",
                column: "PersonInfoId",
                principalTable: "PersonInfos",
                principalColumn: "Id");
        }
    }
}
