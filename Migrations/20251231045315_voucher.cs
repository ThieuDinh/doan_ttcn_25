using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace doan_ttcn.Migrations
{
    /// <inheritdoc />
    public partial class voucher : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Voucher_VoucherId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Voucher",
                table: "Voucher");

            migrationBuilder.RenameTable(
                name: "Voucher",
                newName: "Vouchers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vouchers",
                table: "Vouchers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Vouchers_VoucherId",
                table: "Orders",
                column: "VoucherId",
                principalTable: "Vouchers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Vouchers_VoucherId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vouchers",
                table: "Vouchers");

            migrationBuilder.RenameTable(
                name: "Vouchers",
                newName: "Voucher");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Voucher",
                table: "Voucher",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Voucher_VoucherId",
                table: "Orders",
                column: "VoucherId",
                principalTable: "Voucher",
                principalColumn: "Id");
        }
    }
}
