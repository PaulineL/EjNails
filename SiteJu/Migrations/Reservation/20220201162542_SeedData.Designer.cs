﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SiteJu.Data;

#nullable disable

namespace SiteJu.Migrations.Reservation
{
    [DbContext(typeof(ReservationContext))]
    [Migration("20220201162542_SeedData")]
    partial class SeedData
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.1");

            modelBuilder.Entity("SiteJu.Models.Client", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("Firstname")
                        .HasColumnType("TEXT");

                    b.Property<string>("Lastname")
                        .HasColumnType("TEXT");

                    b.Property<string>("Telephone")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("Client", (string)null);
                });

            modelBuilder.Entity("SiteJu.Models.Prestation", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Prestations")
                        .HasColumnType("TEXT");

                    b.Property<int>("Price")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.ToTable("Prestation", (string)null);
                });

            modelBuilder.Entity("SiteJu.Models.RDV", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ClientID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Date")
                        .HasColumnType("TEXT");

                    b.Property<string>("Firstname")
                        .HasColumnType("TEXT");

                    b.Property<string>("Heure")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name_prestaitons")
                        .HasColumnType("TEXT");

                    b.Property<int?>("PrestationID")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("ClientID");

                    b.HasIndex("PrestationID");

                    b.ToTable("RDV", (string)null);
                });

            modelBuilder.Entity("SiteJu.Models.RDV", b =>
                {
                    b.HasOne("SiteJu.Models.Client", "Client")
                        .WithMany("RDV")
                        .HasForeignKey("ClientID");

                    b.HasOne("SiteJu.Models.Prestation", "Prestation")
                        .WithMany()
                        .HasForeignKey("PrestationID");

                    b.Navigation("Client");

                    b.Navigation("Prestation");
                });

            modelBuilder.Entity("SiteJu.Models.Client", b =>
                {
                    b.Navigation("RDV");
                });
#pragma warning restore 612, 618
        }
    }
}