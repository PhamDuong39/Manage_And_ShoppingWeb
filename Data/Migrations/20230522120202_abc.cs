using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class abc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Colors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ColorName = table.Column<string>(type: "nvarchar(30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Coupons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DiscountValue = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    VoucherName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coupons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Stage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    District = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ward = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethods",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DiscountValue = table.Column<int>(type: "int", nullable: false),
                    SaleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShipAdressMethods",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipAdressMethods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sizes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SizeNumber = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sizes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdRole = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phonenumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Fullname = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_users_Roles_IdRole",
                        column: x => x.IdRole,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShoeDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdSupplier = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdCategory = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdBrand = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CostPrice = table.Column<int>(type: "int", nullable: false),
                    SellPrice = table.Column<int>(type: "int", nullable: false),
                    AvailableQuantity = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    IdSale = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoeDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShoeDetails_Brands_IdBrand",
                        column: x => x.IdBrand,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShoeDetails_Categories_IdCategory",
                        column: x => x.IdCategory,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShoeDetails_Sales_IdSale",
                        column: x => x.IdSale,
                        principalTable: "Sales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShoeDetails_Suppliers_IdSupplier",
                        column: x => x.IdSupplier,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AchivePoints",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PointValue = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AchivePoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AchivePoints_users_IdUser",
                        column: x => x.IdUser,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bills",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    IdUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdCoupon = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdPaymentMethod = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdShipAdressMethod = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdLocation = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bills_Coupons_IdCoupon",
                        column: x => x.IdCoupon,
                        principalTable: "Coupons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bills_Location_IdLocation",
                        column: x => x.IdLocation,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bills_PaymentMethods_IdPaymentMethod",
                        column: x => x.IdPaymentMethod,
                        principalTable: "PaymentMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bills_ShipAdressMethods_IdShipAdressMethod",
                        column: x => x.IdShipAdressMethod,
                        principalTable: "ShipAdressMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bills_users_IdUser",
                        column: x => x.IdUser,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    IdUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.IdUser);
                    table.ForeignKey(
                        name: "FK_Carts_users_IdUser",
                        column: x => x.IdUser,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Color_ShoeDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdColor = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdShoeDetail = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Color_ShoeDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Color_ShoeDetails_Colors_IdColor",
                        column: x => x.IdColor,
                        principalTable: "Colors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Color_ShoeDetails_ShoeDetails_IdShoeDetail",
                        column: x => x.IdShoeDetail,
                        principalTable: "ShoeDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Descriptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdShoeDetail = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Note1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Note2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Note3 = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Descriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Descriptions_ShoeDetails_IdShoeDetail",
                        column: x => x.IdShoeDetail,
                        principalTable: "ShoeDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FavouriteShoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdShoeDetail = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    IdUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavouriteShoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FavouriteShoes_ShoeDetails_IdShoeDetail",
                        column: x => x.IdShoeDetail,
                        principalTable: "ShoeDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FavouriteShoes_users_IdUser",
                        column: x => x.IdUser,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Feedbacks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdShoeDetail = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RatingStar = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedbacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Feedbacks_ShoeDetails_IdShoeDetail",
                        column: x => x.IdShoeDetail,
                        principalTable: "ShoeDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Feedbacks_users_IdUser",
                        column: x => x.IdUser,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdShoeDetail = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageSource = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_ShoeDetails_IdShoeDetail",
                        column: x => x.IdShoeDetail,
                        principalTable: "ShoeDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sizes_ShoeDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdSize = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdShoeDetails = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sizes_ShoeDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sizes_ShoeDetails_ShoeDetails_IdShoeDetails",
                        column: x => x.IdShoeDetails,
                        principalTable: "ShoeDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sizes_ShoeDetails_Sizes_IdSize",
                        column: x => x.IdSize,
                        principalTable: "Sizes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BillDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdShoeDetail = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdBill = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BillDetails_Bills_IdBill",
                        column: x => x.IdBill,
                        principalTable: "Bills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BillDetails_ShoeDetails_IdShoeDetail",
                        column: x => x.IdShoeDetail,
                        principalTable: "ShoeDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdCart = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdShoeDetail = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartDetails_Carts_IdUser",
                        column: x => x.IdUser,
                        principalTable: "Carts",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartDetails_ShoeDetails_IdShoeDetail",
                        column: x => x.IdShoeDetail,
                        principalTable: "ShoeDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AchivePoints_IdUser",
                table: "AchivePoints",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_BillDetails_IdBill",
                table: "BillDetails",
                column: "IdBill");

            migrationBuilder.CreateIndex(
                name: "IX_BillDetails_IdShoeDetail",
                table: "BillDetails",
                column: "IdShoeDetail");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_IdCoupon",
                table: "Bills",
                column: "IdCoupon");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_IdLocation",
                table: "Bills",
                column: "IdLocation");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_IdPaymentMethod",
                table: "Bills",
                column: "IdPaymentMethod");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_IdShipAdressMethod",
                table: "Bills",
                column: "IdShipAdressMethod");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_IdUser",
                table: "Bills",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_CartDetails_IdShoeDetail",
                table: "CartDetails",
                column: "IdShoeDetail");

            migrationBuilder.CreateIndex(
                name: "IX_CartDetails_IdUser",
                table: "CartDetails",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_Color_ShoeDetails_IdColor",
                table: "Color_ShoeDetails",
                column: "IdColor");

            migrationBuilder.CreateIndex(
                name: "IX_Color_ShoeDetails_IdShoeDetail",
                table: "Color_ShoeDetails",
                column: "IdShoeDetail");

            migrationBuilder.CreateIndex(
                name: "IX_Descriptions_IdShoeDetail",
                table: "Descriptions",
                column: "IdShoeDetail");

            migrationBuilder.CreateIndex(
                name: "IX_FavouriteShoes_IdShoeDetail",
                table: "FavouriteShoes",
                column: "IdShoeDetail");

            migrationBuilder.CreateIndex(
                name: "IX_FavouriteShoes_IdUser",
                table: "FavouriteShoes",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_IdShoeDetail",
                table: "Feedbacks",
                column: "IdShoeDetail");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_IdUser",
                table: "Feedbacks",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_Images_IdShoeDetail",
                table: "Images",
                column: "IdShoeDetail");

            migrationBuilder.CreateIndex(
                name: "IX_ShoeDetails_IdBrand",
                table: "ShoeDetails",
                column: "IdBrand");

            migrationBuilder.CreateIndex(
                name: "IX_ShoeDetails_IdCategory",
                table: "ShoeDetails",
                column: "IdCategory");

            migrationBuilder.CreateIndex(
                name: "IX_ShoeDetails_IdSale",
                table: "ShoeDetails",
                column: "IdSale");

            migrationBuilder.CreateIndex(
                name: "IX_ShoeDetails_IdSupplier",
                table: "ShoeDetails",
                column: "IdSupplier");

            migrationBuilder.CreateIndex(
                name: "IX_Sizes_ShoeDetails_IdShoeDetails",
                table: "Sizes_ShoeDetails",
                column: "IdShoeDetails");

            migrationBuilder.CreateIndex(
                name: "IX_Sizes_ShoeDetails_IdSize",
                table: "Sizes_ShoeDetails",
                column: "IdSize");

            migrationBuilder.CreateIndex(
                name: "IX_users_IdRole",
                table: "users",
                column: "IdRole");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AchivePoints");

            migrationBuilder.DropTable(
                name: "BillDetails");

            migrationBuilder.DropTable(
                name: "CartDetails");

            migrationBuilder.DropTable(
                name: "Color_ShoeDetails");

            migrationBuilder.DropTable(
                name: "Descriptions");

            migrationBuilder.DropTable(
                name: "FavouriteShoes");

            migrationBuilder.DropTable(
                name: "Feedbacks");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Sizes_ShoeDetails");

            migrationBuilder.DropTable(
                name: "Bills");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "Colors");

            migrationBuilder.DropTable(
                name: "ShoeDetails");

            migrationBuilder.DropTable(
                name: "Sizes");

            migrationBuilder.DropTable(
                name: "Coupons");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropTable(
                name: "PaymentMethods");

            migrationBuilder.DropTable(
                name: "ShipAdressMethods");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Sales");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
