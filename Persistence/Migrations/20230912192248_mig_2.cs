using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Models_Transmissions_TransmissionId",
                table: "Models");

            migrationBuilder.DropIndex(
                name: "IX_Models_TransmissionId",
                table: "Models");

            migrationBuilder.DropColumn(
                name: "TransmissionId",
                table: "Models");

            migrationBuilder.CreateIndex(
                name: "IX_Models_TranmissionId",
                table: "Models",
                column: "TranmissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Models_Transmissions_TranmissionId",
                table: "Models",
                column: "TranmissionId",
                principalTable: "Transmissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Models_Transmissions_TranmissionId",
                table: "Models");

            migrationBuilder.DropIndex(
                name: "IX_Models_TranmissionId",
                table: "Models");

            migrationBuilder.AddColumn<Guid>(
                name: "TransmissionId",
                table: "Models",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Models_TransmissionId",
                table: "Models",
                column: "TransmissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Models_Transmissions_TransmissionId",
                table: "Models",
                column: "TransmissionId",
                principalTable: "Transmissions",
                principalColumn: "Id");
        }
    }
}
