﻿// <auto-generated />
using System;
using JuegosApi;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace JuegosApi.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("JuegosApi.Entidades.Dato", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime?>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("Genero")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Datos");
                });

            modelBuilder.Entity("JuegosApi.Entidades.Juego", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Juegos");
                });

            modelBuilder.Entity("JuegosApi.Entidades.JuegoDato", b =>
                {
                    b.Property<int>("JuegoId")
                        .HasColumnType("int");

                    b.Property<int>("DatoId")
                        .HasColumnType("int");

                    b.Property<int>("Orden")
                        .HasColumnType("int");

                    b.HasKey("JuegoId", "DatoId");

                    b.HasIndex("DatoId");

                    b.ToTable("JuegoDato");
                });

            modelBuilder.Entity("JuegosApi.Entidades.JuegoDato", b =>
                {
                    b.HasOne("JuegosApi.Entidades.Dato", "Dato")
                        .WithMany("JuegoDato")
                        .HasForeignKey("DatoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("JuegosApi.Entidades.Juego", "Juego")
                        .WithMany("JuegoDato")
                        .HasForeignKey("JuegoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Dato");

                    b.Navigation("Juego");
                });

            modelBuilder.Entity("JuegosApi.Entidades.Dato", b =>
                {
                    b.Navigation("JuegoDato");
                });

            modelBuilder.Entity("JuegosApi.Entidades.Juego", b =>
                {
                    b.Navigation("JuegoDato");
                });
#pragma warning restore 612, 618
        }
    }
}
