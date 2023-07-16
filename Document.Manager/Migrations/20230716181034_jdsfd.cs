using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Document.Manager.Migrations
{
    public partial class jdsfd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttachedDocuments_Users_UserEmail",
                table: "AttachedDocuments");

            migrationBuilder.DropColumn(
                name: "TransactionNumber",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "UserEmail",
                table: "AttachedDocuments",
                newName: "TransactionId");

            migrationBuilder.RenameIndex(
                name: "IX_AttachedDocuments_UserEmail",
                table: "AttachedDocuments",
                newName: "IX_AttachedDocuments_TransactionId");

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TransactionNumber = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transaction_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_UserId",
                table: "Transaction",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AttachedDocuments_Transaction_TransactionId",
                table: "AttachedDocuments",
                column: "TransactionId",
                principalTable: "Transaction",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttachedDocuments_Transaction_TransactionId",
                table: "AttachedDocuments");

            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.RenameColumn(
                name: "TransactionId",
                table: "AttachedDocuments",
                newName: "UserEmail");

            migrationBuilder.RenameIndex(
                name: "IX_AttachedDocuments_TransactionId",
                table: "AttachedDocuments",
                newName: "IX_AttachedDocuments_UserEmail");

            migrationBuilder.AddColumn<long>(
                name: "TransactionNumber",
                table: "Users",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddForeignKey(
                name: "FK_AttachedDocuments_Users_UserEmail",
                table: "AttachedDocuments",
                column: "UserEmail",
                principalTable: "Users",
                principalColumn: "Email",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
