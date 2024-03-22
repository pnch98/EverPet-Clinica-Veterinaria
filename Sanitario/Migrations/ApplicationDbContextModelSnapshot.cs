﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Sanitario.Data;

#nullable disable

namespace Sanitario.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CuraPrescrittaProdotto", b =>
                {
                    b.Property<int>("CurePrescritteIdCuraPrescritta")
                        .HasColumnType("int");

                    b.Property<int>("ProdottiIdProdotto")
                        .HasColumnType("int");

                    b.HasKey("CurePrescritteIdCuraPrescritta", "ProdottiIdProdotto");

                    b.HasIndex("ProdottiIdProdotto");

                    b.ToTable("CuraPrescrittaProdotto", (string)null);
                });

            modelBuilder.Entity("Sanitario.Models.Animale", b =>
                {
                    b.Property<int>("IdAnimale")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdAnimale"));

                    b.Property<string>("ColoreMantello")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateOnly>("DataNascita")
                        .HasColumnType("date");

                    b.Property<DateOnly>("DataRegistrazione")
                        .HasColumnType("date");

                    b.Property<int>("IdCliente")
                        .HasColumnType("int");

                    b.Property<string>("Microchip")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tipologia")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdAnimale");

                    b.HasIndex("IdCliente");

                    b.ToTable("Animali", (string)null);
                });

            modelBuilder.Entity("Sanitario.Models.AnimaleSmarrito", b =>
                {
                    b.Property<int>("IdAnimaleSmarrito")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdAnimaleSmarrito"));

                    b.Property<string>("CodiceFiscaleProprietario")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ColoreMantello")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateOnly>("DataInizioRicovero")
                        .HasColumnType("date");

                    b.Property<DateOnly>("DataNascita")
                        .HasColumnType("date");

                    b.Property<DateOnly>("DataRegistrazione")
                        .HasColumnType("date");

                    b.Property<string>("Foto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Microchip")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tipologia")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdAnimaleSmarrito");

                    b.ToTable("AnimaliSmarriti", (string)null);
                });

            modelBuilder.Entity("Sanitario.Models.Armadietto", b =>
                {
                    b.Property<int>("IdArmadietto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdArmadietto"));

                    b.Property<int>("NumeroArmadietto")
                        .HasColumnType("int");

                    b.HasKey("IdArmadietto");

                    b.ToTable("Armadietti", (string)null);
                });

            modelBuilder.Entity("Sanitario.Models.Cassetto", b =>
                {
                    b.Property<int>("IdCassetto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCassetto"));

                    b.Property<int>("IdArmadietto")
                        .HasColumnType("int");

                    b.Property<int>("NumeroCassetto")
                        .HasColumnType("int");

                    b.HasKey("IdCassetto");

                    b.HasIndex("IdArmadietto");

                    b.ToTable("Cassetti", (string)null);
                });

            modelBuilder.Entity("Sanitario.Models.Cliente", b =>
                {
                    b.Property<int>("IdCliente")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCliente"));

                    b.Property<string>("CodiceFiscale")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Cognome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdCliente");

                    b.ToTable("Clienti", (string)null);
                });

            modelBuilder.Entity("Sanitario.Models.CuraPrescritta", b =>
                {
                    b.Property<int>("IdCuraPrescritta")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCuraPrescritta"));

                    b.Property<int>("IdProdotto")
                        .HasColumnType("int");

                    b.Property<int>("IdVisita")
                        .HasColumnType("int");

                    b.HasKey("IdCuraPrescritta");

                    b.HasIndex("IdVisita");

                    b.ToTable("CurePrescritte", (string)null);
                });

            modelBuilder.Entity("Sanitario.Models.DettagliVendita", b =>
                {
                    b.Property<int>("IdDettagliVendita")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdDettagliVendita"));

                    b.Property<int>("IdProdotto")
                        .HasColumnType("int");

                    b.Property<int>("IdVendita")
                        .HasColumnType("int");

                    b.HasKey("IdDettagliVendita");

                    b.HasIndex("IdProdotto");

                    b.HasIndex("IdVendita");

                    b.ToTable("DettagliVendite", (string)null);
                });

            modelBuilder.Entity("Sanitario.Models.Dipendente", b =>
                {
                    b.Property<int>("IdDipendente")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdDipendente"));

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ruolo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdDipendente");

                    b.ToTable("Dipendenti", (string)null);
                });

            modelBuilder.Entity("Sanitario.Models.Prodotto", b =>
                {
                    b.Property<int>("IdProdotto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdProdotto"));

                    b.Property<string>("Descrizione")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdCassetto")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Prezzo")
                        .HasColumnType("float");

                    b.Property<string>("TipoProdotto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdProdotto");

                    b.HasIndex("IdCassetto");

                    b.ToTable("Prodotti", (string)null);
                });

            modelBuilder.Entity("Sanitario.Models.Vendita", b =>
                {
                    b.Property<int>("IdVendita")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdVendita"));

                    b.Property<DateTime>("DataVendita")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdCliente")
                        .HasColumnType("int");

                    b.Property<double>("PrezzoTotale")
                        .HasColumnType("float");

                    b.HasKey("IdVendita");

                    b.HasIndex("IdCliente");

                    b.ToTable("Vendite", (string)null);
                });

            modelBuilder.Entity("Sanitario.Models.Visita", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateOnly>("DataVisita")
                        .HasColumnType("date");

                    b.Property<string>("Esame")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdAnimale")
                        .HasColumnType("int");

                    b.Property<bool>("IsArchiviato")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("IdAnimale");

                    b.ToTable("Visite", (string)null);
                });

            modelBuilder.Entity("CuraPrescrittaProdotto", b =>
                {
                    b.HasOne("Sanitario.Models.CuraPrescritta", null)
                        .WithMany()
                        .HasForeignKey("CurePrescritteIdCuraPrescritta")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sanitario.Models.Prodotto", null)
                        .WithMany()
                        .HasForeignKey("ProdottiIdProdotto")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Sanitario.Models.Animale", b =>
                {
                    b.HasOne("Sanitario.Models.Cliente", "Cliente")
                        .WithMany("Animali")
                        .HasForeignKey("IdCliente")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cliente");
                });

            modelBuilder.Entity("Sanitario.Models.Cassetto", b =>
                {
                    b.HasOne("Sanitario.Models.Armadietto", "Armadietto")
                        .WithMany("Cassetti")
                        .HasForeignKey("IdArmadietto")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Armadietto");
                });

            modelBuilder.Entity("Sanitario.Models.CuraPrescritta", b =>
                {
                    b.HasOne("Sanitario.Models.Visita", "Visita")
                        .WithMany("CurePrescritte")
                        .HasForeignKey("IdVisita")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Visita");
                });

            modelBuilder.Entity("Sanitario.Models.DettagliVendita", b =>
                {
                    b.HasOne("Sanitario.Models.Prodotto", "Prodotto")
                        .WithMany("DettagliVendite")
                        .HasForeignKey("IdProdotto")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sanitario.Models.Vendita", "Vendita")
                        .WithMany("DettagliVendite")
                        .HasForeignKey("IdVendita")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Prodotto");

                    b.Navigation("Vendita");
                });

            modelBuilder.Entity("Sanitario.Models.Prodotto", b =>
                {
                    b.HasOne("Sanitario.Models.Cassetto", "Cassetto")
                        .WithMany("Prodotti")
                        .HasForeignKey("IdCassetto")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cassetto");
                });

            modelBuilder.Entity("Sanitario.Models.Vendita", b =>
                {
                    b.HasOne("Sanitario.Models.Cliente", "Cliente")
                        .WithMany("Vendite")
                        .HasForeignKey("IdCliente")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cliente");
                });

            modelBuilder.Entity("Sanitario.Models.Visita", b =>
                {
                    b.HasOne("Sanitario.Models.Animale", "Animale")
                        .WithMany("Visite")
                        .HasForeignKey("IdAnimale")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Animale");
                });

            modelBuilder.Entity("Sanitario.Models.Animale", b =>
                {
                    b.Navigation("Visite");
                });

            modelBuilder.Entity("Sanitario.Models.Armadietto", b =>
                {
                    b.Navigation("Cassetti");
                });

            modelBuilder.Entity("Sanitario.Models.Cassetto", b =>
                {
                    b.Navigation("Prodotti");
                });

            modelBuilder.Entity("Sanitario.Models.Cliente", b =>
                {
                    b.Navigation("Animali");

                    b.Navigation("Vendite");
                });

            modelBuilder.Entity("Sanitario.Models.Prodotto", b =>
                {
                    b.Navigation("DettagliVendite");
                });

            modelBuilder.Entity("Sanitario.Models.Vendita", b =>
                {
                    b.Navigation("DettagliVendite");
                });

            modelBuilder.Entity("Sanitario.Models.Visita", b =>
                {
                    b.Navigation("CurePrescritte");
                });
#pragma warning restore 612, 618
        }
    }
}
