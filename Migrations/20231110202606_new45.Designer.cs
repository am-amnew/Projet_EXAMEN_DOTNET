﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApplication2.dal;

#nullable disable

namespace WebApplication2.Migrations
{
    [DbContext(typeof(LivreDbContext))]
    [Migration("20231110202606_new45")]
    partial class new45
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WebApplication2.Models.Abonne", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Prenom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Abonnes");
                });

            modelBuilder.Entity("WebApplication2.Models.Emprunt", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AbonneId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateEmprunt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateRetour")
                        .HasColumnType("datetime2");

                    b.Property<int>("LivreId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AbonneId");

                    b.HasIndex("LivreId");

                    b.ToTable("Emprunts");
                });

            modelBuilder.Entity("WebApplication2.Models.Livre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Auteur")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EstEmprunte")
                        .HasColumnType("bit");

                    b.Property<string>("Resume")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Titre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Livres");
                });

            modelBuilder.Entity("WebApplication2.Models.Emprunt", b =>
                {
                    b.HasOne("WebApplication2.Models.Abonne", "Abonne")
                        .WithMany("Emprunts")
                        .HasForeignKey("AbonneId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApplication2.Models.Livre", "Livre")
                        .WithMany("Emprunts")
                        .HasForeignKey("LivreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Abonne");

                    b.Navigation("Livre");
                });

            modelBuilder.Entity("WebApplication2.Models.Abonne", b =>
                {
                    b.Navigation("Emprunts");
                });

            modelBuilder.Entity("WebApplication2.Models.Livre", b =>
                {
                    b.Navigation("Emprunts");
                });
#pragma warning restore 612, 618
        }
    }
}
