using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VialambreAppTest1.Migrations
{
    /// <inheritdoc />
    public partial class VialambreAppNewMigration : Migration
    {
        /// <inheritdoc />
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
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Document = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RolName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
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
                name: "Cliente",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Document = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Empaque",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empaque", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LugarEntrega",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LugarEntrega", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pieza",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BriefId = table.Column<int>(type: "int", nullable: true),
                    Consecutivo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PiezaName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumeroPieza = table.Column<int>(type: "int", nullable: true),
                    CantidadPiezas = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PiezaAlto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PiezaAncho = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PiezaFondo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaterialInput = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tubo = table.Column<bool>(type: "bit", nullable: false),
                    Alambre = table.Column<bool>(type: "bit", nullable: false),
                    Lamina = table.Column<bool>(type: "bit", nullable: false),
                    Madera = table.Column<bool>(type: "bit", nullable: false),
                    Formica = table.Column<bool>(type: "bit", nullable: false),
                    Polietileno = table.Column<bool>(type: "bit", nullable: false),
                    Acrilico = table.Column<bool>(type: "bit", nullable: false),
                    Ps = table.Column<bool>(type: "bit", nullable: false),
                    Pp = table.Column<bool>(type: "bit", nullable: false),
                    Mdf = table.Column<bool>(type: "bit", nullable: false),
                    NingunMaterial = table.Column<bool>(type: "bit", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Publicidad = table.Column<bool>(type: "bit", nullable: true),
                    Tintas = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AcabadoInput = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Laminado = table.Column<bool>(type: "bit", nullable: false),
                    Refilado = table.Column<bool>(type: "bit", nullable: false),
                    Troquelado = table.Column<bool>(type: "bit", nullable: false),
                    Termodoblado = table.Column<bool>(type: "bit", nullable: false),
                    Termoformado = table.Column<bool>(type: "bit", nullable: false),
                    NingunAcabado = table.Column<bool>(type: "bit", nullable: false),
                    Cargue = table.Column<float>(type: "real", nullable: true),
                    AcabadosyAccesoriosInput = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ruedas = table.Column<bool>(type: "bit", nullable: false),
                    Niveladores = table.Column<bool>(type: "bit", nullable: false),
                    Cromado = table.Column<bool>(type: "bit", nullable: false),
                    Zincado = table.Column<bool>(type: "bit", nullable: false),
                    NingunAccesorio = table.Column<bool>(type: "bit", nullable: false),
                    ComentariosAdicionales = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Item1name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Item1Alto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Item1Ancho = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Item2name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Item2Alto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Item2Ancho = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Item3name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Item3Alto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Item3Ancho = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Item4name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Item4Alto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Item4Ancho = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArchivoPieza = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pieza", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
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
                name: "Brief",
                columns: table => new
                {
                    BriefId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Consecutivo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientFullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientDocument = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AgregarNuevoCliente = table.Column<bool>(type: "bit", nullable: false),
                    NewClientFullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewClientDocument = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AsesorDocument = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AsesorFullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AsesorEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AsesorPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipoCotizacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HV = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Marca = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Producto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewProducto = table.Column<bool>(type: "bit", nullable: false),
                    ProductoInput = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaRequerida = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EntregarEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewLugarEntrega = table.Column<bool>(type: "bit", nullable: false),
                    EntregarEnInput = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ComentariosAdicionales = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Costos = table.Column<bool>(type: "bit", nullable: true),
                    Design = table.Column<bool>(type: "bit", nullable: true),
                    Linea = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EsCantidadUnica = table.Column<bool>(type: "bit", nullable: false),
                    UnidadesInput = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EsEscala = table.Column<bool>(type: "bit", nullable: false),
                    EscalaInput = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Instalacion = table.Column<bool>(type: "bit", nullable: true),
                    DireccionInstalacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CanalVenta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EsCanalVenta = table.Column<bool>(type: "bit", nullable: false),
                    CanalVentaInput = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipoEmpaque = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewEmpaque = table.Column<bool>(type: "bit", nullable: false),
                    TipoEmpaqueInput = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PresupuestoUnidad = table.Column<int>(type: "int", nullable: true),
                    ValorTotalCotizado = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    ArchivosCostosExist = table.Column<bool>(type: "bit", nullable: true),
                    ArchivosCostos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArchivosDesignExist = table.Column<bool>(type: "bit", nullable: true),
                    ArchivosDesign = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrlExist = table.Column<bool>(type: "bit", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KeyVisual = table.Column<bool>(type: "bit", nullable: false),
                    Artes = table.Column<bool>(type: "bit", nullable: false),
                    Planos = table.Column<bool>(type: "bit", nullable: false),
                    Logos = table.Column<bool>(type: "bit", nullable: false),
                    ManualMarca = table.Column<bool>(type: "bit", nullable: false),
                    Boceto = table.Column<bool>(type: "bit", nullable: false),
                    MuestraFisica = table.Column<bool>(type: "bit", nullable: false),
                    ImagenReferencia = table.Column<bool>(type: "bit", nullable: false),
                    Ninguna = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EsRecotizacion = table.Column<bool>(type: "bit", nullable: false),
                    FueRecotizado = table.Column<bool>(type: "bit", nullable: false),
                    FechaRecotizacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StatusDesign = table.Column<int>(type: "int", nullable: false),
                    AsignadoDesign = table.Column<bool>(type: "bit", nullable: false),
                    DesignerAsignado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RespuestaDesign = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaRespuestaDesign = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RespuestaAsesorDesign = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaRespuestaAsesorDesign = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaDesignAceptado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaAsignacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StatusAsesor = table.Column<int>(type: "int", nullable: false),
                    StatusCostos = table.Column<int>(type: "int", nullable: false),
                    AsignadoCostos = table.Column<bool>(type: "bit", nullable: false),
                    CosteadorAsignado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RespuestaCostos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RespuestaCostosInput = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaRespuestaCostos = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RespuestaAsesorCostos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaRespuestaAsesorCostos = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaCostosAceptado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    EmpaqueId = table.Column<int>(type: "int", nullable: true),
                    LugarEntregaId = table.Column<int>(type: "int", nullable: true),
                    PiezaId = table.Column<int>(type: "int", nullable: true),
                    ClienteId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brief", x => x.BriefId);
                    table.ForeignKey(
                        name: "FK_Brief_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Brief_Cliente_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Cliente",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Brief_Empaque_EmpaqueId",
                        column: x => x.EmpaqueId,
                        principalTable: "Empaque",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Brief_LugarEntrega_LugarEntregaId",
                        column: x => x.LugarEntregaId,
                        principalTable: "LugarEntrega",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Brief_Pieza_PiezaId",
                        column: x => x.PiezaId,
                        principalTable: "Pieza",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Brief_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id");
                });

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
                name: "IX_Brief_AppUserId",
                table: "Brief",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Brief_ClienteId",
                table: "Brief",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Brief_EmpaqueId",
                table: "Brief",
                column: "EmpaqueId");

            migrationBuilder.CreateIndex(
                name: "IX_Brief_LugarEntregaId",
                table: "Brief",
                column: "LugarEntregaId");

            migrationBuilder.CreateIndex(
                name: "IX_Brief_PiezaId",
                table: "Brief",
                column: "PiezaId");

            migrationBuilder.CreateIndex(
                name: "IX_Brief_ProductId",
                table: "Brief",
                column: "ProductId");
        }

        /// <inheritdoc />
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
                name: "Brief");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.DropTable(
                name: "Empaque");

            migrationBuilder.DropTable(
                name: "LugarEntrega");

            migrationBuilder.DropTable(
                name: "Pieza");

            migrationBuilder.DropTable(
                name: "Product");
        }
    }
}
