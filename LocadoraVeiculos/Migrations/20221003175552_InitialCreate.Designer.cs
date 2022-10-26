﻿// <auto-generated />
using System;
using LocadoraVeiculos.Repository.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LocadoraVeiculos.Migrations
{
    [DbContext(typeof(LocadoraVeiculosContext))]
    [Migration("20221003175552_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.29")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LocadoraVeiculos.Models.Alocação", b =>
                {
                    b.Property<string>("Cpf")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Chassi")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("DtEntrega")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DtSaida")
                        .HasColumnType("datetime2");

                    b.HasKey("Cpf", "Chassi");

                    b.HasIndex("Chassi");

                    b.ToTable("Alocação");
                });

            modelBuilder.Entity("LocadoraVeiculos.Models.Carro", b =>
                {
                    b.Property<string>("Chassi")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Ano")
                        .HasColumnType("int");

                    b.Property<int>("CodCategoria")
                        .HasColumnType("int");

                    b.Property<string>("Cor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Marca")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Modelo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Placa")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Chassi");

                    b.HasIndex("CodCategoria");

                    b.ToTable("Carro");
                });

            modelBuilder.Entity("LocadoraVeiculos.Models.Categoria", b =>
                {
                    b.Property<int>("CodCategoria")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descrição")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("ValorDiaria")
                        .HasColumnType("float");

                    b.HasKey("CodCategoria");

                    b.ToTable("Categoria");
                });

            modelBuilder.Entity("LocadoraVeiculos.Models.Cliente", b =>
                {
                    b.Property<string>("Cpf")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("CEP")
                        .HasColumnType("int");

                    b.Property<string>("Cidade")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Cnh")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DtNascimento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Logradouro")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RG")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UF")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Cpf");

                    b.ToTable("Cliente");
                });

            modelBuilder.Entity("LocadoraVeiculos.Models.Alocação", b =>
                {
                    b.HasOne("LocadoraVeiculos.Models.Carro", "Carro")
                        .WithMany("Alocação")
                        .HasForeignKey("Chassi")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LocadoraVeiculos.Models.Cliente", "Cliente")
                        .WithMany("Alocação")
                        .HasForeignKey("Cpf")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LocadoraVeiculos.Models.Carro", b =>
                {
                    b.HasOne("LocadoraVeiculos.Models.Categoria", "Categoria")
                        .WithMany("Carro")
                        .HasForeignKey("CodCategoria")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}