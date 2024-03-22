using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sanitario.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnimaliSmarriti",
                columns: table => new
                {
                    IdAnimaleSmarrito = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tipologia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataRegistrazione = table.Column<DateOnly>(type: "date", nullable: false),
                    DataNascita = table.Column<DateOnly>(type: "date", nullable: false),
                    ColoreMantello = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CodiceFiscaleProprietario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Microchip = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataInizioRicovero = table.Column<DateOnly>(type: "date", nullable: false),
                    Foto = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimaliSmarriti", x => x.IdAnimaleSmarrito);
                });

            migrationBuilder.CreateTable(
                name: "Armadietti",
                columns: table => new
                {
                    IdArmadietto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeroArmadietto = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Armadietti", x => x.IdArmadietto);
                });

            migrationBuilder.CreateTable(
                name: "Clienti",
                columns: table => new
                {
                    IdCliente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cognome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CodiceFiscale = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clienti", x => x.IdCliente);
                });

            migrationBuilder.CreateTable(
                name: "Dipendenti",
                columns: table => new
                {
                    IdDipendente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ruolo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dipendenti", x => x.IdDipendente);
                });

            migrationBuilder.CreateTable(
                name: "Cassetti",
                columns: table => new
                {
                    IdCassetto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdArmadietto = table.Column<int>(type: "int", nullable: false),
                    NumeroCassetto = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cassetti", x => x.IdCassetto);
                    table.ForeignKey(
                        name: "FK_Cassetti_Armadietti_IdArmadietto",
                        column: x => x.IdArmadietto,
                        principalTable: "Armadietti",
                        principalColumn: "IdArmadietto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Animali",
                columns: table => new
                {
                    IdAnimale = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tipologia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataRegistrazione = table.Column<DateOnly>(type: "date", nullable: false),
                    DataNascita = table.Column<DateOnly>(type: "date", nullable: false),
                    ColoreMantello = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdCliente = table.Column<int>(type: "int", nullable: false),
                    Microchip = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animali", x => x.IdAnimale);
                    table.ForeignKey(
                        name: "FK_Animali_Clienti_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "Clienti",
                        principalColumn: "IdCliente",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vendite",
                columns: table => new
                {
                    IdVendita = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCliente = table.Column<int>(type: "int", nullable: false),
                    DataVendita = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PrezzoTotale = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendite", x => x.IdVendita);
                    table.ForeignKey(
                        name: "FK_Vendite_Clienti_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "Clienti",
                        principalColumn: "IdCliente",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Prodotti",
                columns: table => new
                {
                    IdProdotto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descrizione = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prezzo = table.Column<double>(type: "float", nullable: false),
                    TipoProdotto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdCassetto = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prodotti", x => x.IdProdotto);
                    table.ForeignKey(
                        name: "FK_Prodotti_Cassetti_IdCassetto",
                        column: x => x.IdCassetto,
                        principalTable: "Cassetti",
                        principalColumn: "IdCassetto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Visite",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdAnimale = table.Column<int>(type: "int", nullable: false),
                    DataVisita = table.Column<DateOnly>(type: "date", nullable: false),
                    Esame = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visite", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Visite_Animali_IdAnimale",
                        column: x => x.IdAnimale,
                        principalTable: "Animali",
                        principalColumn: "IdAnimale",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DettagliVendite",
                columns: table => new
                {
                    IdDettagliVendita = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdVendita = table.Column<int>(type: "int", nullable: false),
                    IdProdotto = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DettagliVendite", x => x.IdDettagliVendita);
                    table.ForeignKey(
                        name: "FK_DettagliVendite_Prodotti_IdProdotto",
                        column: x => x.IdProdotto,
                        principalTable: "Prodotti",
                        principalColumn: "IdProdotto",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DettagliVendite_Vendite_IdVendita",
                        column: x => x.IdVendita,
                        principalTable: "Vendite",
                        principalColumn: "IdVendita",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CurePrescritte",
                columns: table => new
                {
                    IdCuraPrescritta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdVisita = table.Column<int>(type: "int", nullable: false),
                    IdProdotto = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurePrescritte", x => x.IdCuraPrescritta);
                    table.ForeignKey(
                        name: "FK_CurePrescritte_Visite_IdVisita",
                        column: x => x.IdVisita,
                        principalTable: "Visite",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CuraPrescrittaProdotto",
                columns: table => new
                {
                    CurePrescritteIdCuraPrescritta = table.Column<int>(type: "int", nullable: false),
                    ProdottiIdProdotto = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CuraPrescrittaProdotto", x => new { x.CurePrescritteIdCuraPrescritta, x.ProdottiIdProdotto });
                    table.ForeignKey(
                        name: "FK_CuraPrescrittaProdotto_CurePrescritte_CurePrescritteIdCuraPrescritta",
                        column: x => x.CurePrescritteIdCuraPrescritta,
                        principalTable: "CurePrescritte",
                        principalColumn: "IdCuraPrescritta",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CuraPrescrittaProdotto_Prodotti_ProdottiIdProdotto",
                        column: x => x.ProdottiIdProdotto,
                        principalTable: "Prodotti",
                        principalColumn: "IdProdotto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Animali_IdCliente",
                table: "Animali",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_Cassetti_IdArmadietto",
                table: "Cassetti",
                column: "IdArmadietto");

            migrationBuilder.CreateIndex(
                name: "IX_CuraPrescrittaProdotto_ProdottiIdProdotto",
                table: "CuraPrescrittaProdotto",
                column: "ProdottiIdProdotto");

            migrationBuilder.CreateIndex(
                name: "IX_CurePrescritte_IdVisita",
                table: "CurePrescritte",
                column: "IdVisita");

            migrationBuilder.CreateIndex(
                name: "IX_DettagliVendite_IdProdotto",
                table: "DettagliVendite",
                column: "IdProdotto");

            migrationBuilder.CreateIndex(
                name: "IX_DettagliVendite_IdVendita",
                table: "DettagliVendite",
                column: "IdVendita");

            migrationBuilder.CreateIndex(
                name: "IX_Prodotti_IdCassetto",
                table: "Prodotti",
                column: "IdCassetto");

            migrationBuilder.CreateIndex(
                name: "IX_Vendite_IdCliente",
                table: "Vendite",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_Visite_IdAnimale",
                table: "Visite",
                column: "IdAnimale");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnimaliSmarriti");

            migrationBuilder.DropTable(
                name: "CuraPrescrittaProdotto");

            migrationBuilder.DropTable(
                name: "DettagliVendite");

            migrationBuilder.DropTable(
                name: "Dipendenti");

            migrationBuilder.DropTable(
                name: "CurePrescritte");

            migrationBuilder.DropTable(
                name: "Prodotti");

            migrationBuilder.DropTable(
                name: "Vendite");

            migrationBuilder.DropTable(
                name: "Visite");

            migrationBuilder.DropTable(
                name: "Cassetti");

            migrationBuilder.DropTable(
                name: "Animali");

            migrationBuilder.DropTable(
                name: "Armadietti");

            migrationBuilder.DropTable(
                name: "Clienti");
        }
    }
}
