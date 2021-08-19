using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MoveAround.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmailMessage",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailMessage", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "GoogleAdresAPI",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoogleAdresAPI", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdentityUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Tipas = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressCity = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AddressGatveNr = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BuisnessName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BuisnessCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PVM = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    BuisnesAdressCity = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    BuisnesAdressStreet = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EstablishedDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BuisnessEmail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UserEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppUsers_AspNetUsers_IdentityUserId",
                        column: x => x.IdentityUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipas = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PakrovimoTipas = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Svoris = table.Column<double>(type: "float", nullable: false),
                    Turis = table.Column<double>(type: "float", nullable: false),
                    Ilgis = table.Column<double>(type: "float", nullable: false),
                    PaleciuSk = table.Column<int>(type: "int", nullable: false),
                    Temperatura = table.Column<double>(type: "float", nullable: false),
                    PapildomaInfo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Kaina = table.Column<double>(type: "float", nullable: false),
                    FromAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ToAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UzsakovasId = table.Column<int>(type: "int", nullable: false),
                    FromStreet = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FromHouseNo = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    FromCity = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FromDistrict = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: true),
                    FromZipCode = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    FromCountry = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FromLatitude = table.Column<double>(type: "float", nullable: false),
                    FromLongtitude = table.Column<double>(type: "float", nullable: false),
                    ToStreet = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ToHouseNo = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    ToCity = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ToDistrict = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: true),
                    ToZipCode = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    ToCountry = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ToLatitude = table.Column<double>(type: "float", nullable: false),
                    ToLongtitude = table.Column<double>(type: "float", nullable: false),
                    FDate = table.Column<DateTime>(type: "date", nullable: true),
                    TDate = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Order_AppUsers_UzsakovasId",
                        column: x => x.UzsakovasId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transport",
                columns: table => new
                {
                    TransportId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UzsakovasId = table.Column<int>(type: "int", nullable: false),
                    FullFromLocation = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    LocationFromCountry = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LocationFromDistrict = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LocationFromCity = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LocationFromStreet = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LocationFronNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LocationFromZipCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LocationFromLatitude = table.Column<double>(type: "float", nullable: false),
                    LocationFromLongitude = table.Column<double>(type: "float", nullable: false),
                    FullToLocation = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    LocationToCountry = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LocationToDistrict = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LocationToCity = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LocationToStreet = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LocationFromNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LocationToZipCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LocationToLatitude = table.Column<double>(type: "float", nullable: false),
                    LocationToLongitude = table.Column<double>(type: "float", nullable: false),
                    GalimaNuoData = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TransportoSkaicius = table.Column<int>(type: "int", nullable: false),
                    PakrovimoTipas = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SunkvezimioTipas = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ArGalimas = table.Column<bool>(type: "bit", nullable: false),
                    Svoris = table.Column<double>(type: "float", nullable: false),
                    Turis = table.Column<double>(type: "float", nullable: false),
                    Ilgis = table.Column<double>(type: "float", nullable: false),
                    PaleciuSk = table.Column<int>(type: "int", nullable: false),
                    Temperatura = table.Column<bool>(type: "bit", nullable: false),
                    PapildomaInfo = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: true),
                    Kaina = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transport", x => x.TransportId);
                    table.ForeignKey(
                        name: "FK_Transport_AppUsers_UzsakovasId",
                        column: x => x.UzsakovasId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_IdentityUserId",
                table: "AppUsers",
                column: "IdentityUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Order_UzsakovasId",
                table: "Order",
                column: "UzsakovasId");

            migrationBuilder.CreateIndex(
                name: "IX_Transport_UzsakovasId",
                table: "Transport",
                column: "UzsakovasId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "EmailMessage");

            migrationBuilder.DropTable(
                name: "GoogleAdresAPI");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Transport");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AppUsers");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
