using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Document.Manager.Migrations
{
    public partial class jdsfdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttachedDocuments_Transaction_TransactionId",
                table: "AttachedDocuments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AttachedDocuments",
                table: "AttachedDocuments");

            migrationBuilder.RenameTable(
                name: "AttachedDocuments",
                newName: "Documents");

            migrationBuilder.RenameIndex(
                name: "IX_AttachedDocuments_TransactionId",
                table: "Documents",
                newName: "IX_Documents_TransactionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Documents",
                table: "Documents",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Transaction_TransactionId",
                table: "Documents",
                column: "TransactionId",
                principalTable: "Transaction",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Transaction_TransactionId",
                table: "Documents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Documents",
                table: "Documents");

            migrationBuilder.RenameTable(
                name: "Documents",
                newName: "AttachedDocuments");

            migrationBuilder.RenameIndex(
                name: "IX_Documents_TransactionId",
                table: "AttachedDocuments",
                newName: "IX_AttachedDocuments_TransactionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AttachedDocuments",
                table: "AttachedDocuments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AttachedDocuments_Transaction_TransactionId",
                table: "AttachedDocuments",
                column: "TransactionId",
                principalTable: "Transaction",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
