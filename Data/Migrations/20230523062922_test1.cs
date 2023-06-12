using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class test1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavouriteShoes_ShoeDetails_IdShoeDetail",
                table: "FavouriteShoes");

            migrationBuilder.DropForeignKey(
                name: "FK_FavouriteShoes_users_IdUser",
                table: "FavouriteShoes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FavouriteShoes",
                table: "FavouriteShoes");

            migrationBuilder.RenameTable(
                name: "FavouriteShoes",
                newName: "favouriteShoes");

            migrationBuilder.RenameIndex(
                name: "IX_FavouriteShoes_IdUser",
                table: "favouriteShoes",
                newName: "IX_favouriteShoes_IdUser");

            migrationBuilder.RenameIndex(
                name: "IX_FavouriteShoes_IdShoeDetail",
                table: "favouriteShoes",
                newName: "IX_favouriteShoes_IdShoeDetail");

            migrationBuilder.AlterColumn<string>(
                name: "SaleName",
                table: "Sales",
                type: "nvarchar(30)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "VoucherName",
                table: "Coupons",
                type: "nvarchar(30)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_favouriteShoes",
                table: "favouriteShoes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_favouriteShoes_ShoeDetails_IdShoeDetail",
                table: "favouriteShoes",
                column: "IdShoeDetail",
                principalTable: "ShoeDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_favouriteShoes_users_IdUser",
                table: "favouriteShoes",
                column: "IdUser",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_favouriteShoes_ShoeDetails_IdShoeDetail",
                table: "favouriteShoes");

            migrationBuilder.DropForeignKey(
                name: "FK_favouriteShoes_users_IdUser",
                table: "favouriteShoes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_favouriteShoes",
                table: "favouriteShoes");

            migrationBuilder.RenameTable(
                name: "favouriteShoes",
                newName: "FavouriteShoes");

            migrationBuilder.RenameIndex(
                name: "IX_favouriteShoes_IdUser",
                table: "FavouriteShoes",
                newName: "IX_FavouriteShoes_IdUser");

            migrationBuilder.RenameIndex(
                name: "IX_favouriteShoes_IdShoeDetail",
                table: "FavouriteShoes",
                newName: "IX_FavouriteShoes_IdShoeDetail");

            migrationBuilder.AlterColumn<string>(
                name: "SaleName",
                table: "Sales",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)");

            migrationBuilder.AlterColumn<string>(
                name: "VoucherName",
                table: "Coupons",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FavouriteShoes",
                table: "FavouriteShoes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FavouriteShoes_ShoeDetails_IdShoeDetail",
                table: "FavouriteShoes",
                column: "IdShoeDetail",
                principalTable: "ShoeDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FavouriteShoes_users_IdUser",
                table: "FavouriteShoes",
                column: "IdUser",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
