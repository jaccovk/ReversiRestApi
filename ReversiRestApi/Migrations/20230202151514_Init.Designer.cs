﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ReversiRestApi.DAL;

namespace ReversiRestApi.Migrations
{
    [DbContext(typeof(SpelDbContext))]
    [Migration("20230202151514_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.15");

            modelBuilder.Entity("ReversieISpelImplementatie.Model.Spel", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AandeBeurt")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Afgelopen")
                        .HasColumnType("INTEGER");

                    b.Property<string>("BordString")
                        .HasColumnType("TEXT");

                    b.Property<string>("Omschrijving")
                        .HasColumnType("TEXT");

                    b.Property<string>("Speler1Token")
                        .HasColumnType("TEXT");

                    b.Property<string>("Speler2Token")
                        .HasColumnType("TEXT");

                    b.Property<string>("Token")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("Spel");
                });
#pragma warning restore 612, 618
        }
    }
}
