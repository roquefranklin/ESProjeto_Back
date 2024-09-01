﻿// <auto-generated />
using System;
using ESProjeto_Back.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ESProjeto_Back.Migrations
{
    [DbContext(typeof(MotofretaContext))]
    partial class MotofretaContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("ESProjeto_Back.Models.GeolocationCoordinates", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<float?>("accuracy")
                        .HasColumnType("float");

                    b.Property<float?>("altitude")
                        .HasColumnType("float");

                    b.Property<float?>("altitudeAccuracy")
                        .HasColumnType("float");

                    b.Property<float?>("heading")
                        .HasColumnType("float");

                    b.Property<float?>("latitude")
                        .HasColumnType("float");

                    b.Property<float?>("longitude")
                        .HasColumnType("float");

                    b.Property<float?>("speed")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("GeolocationCoordinates");

                    b.HasAnnotation("Relational:JsonPropertyName", "coords");
                });

            modelBuilder.Entity("ESProjeto_Back.Models.GeolocationPosition", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("coordsId")
                        .HasColumnType("char(36)");

                    b.Property<float>("timestamp")
                        .HasColumnType("float")
                        .HasAnnotation("Relational:JsonPropertyName", "timestamp");

                    b.HasKey("Id");

                    b.HasIndex("coordsId");

                    b.ToTable("GeolocationPosition");
                });

            modelBuilder.Entity("ESProjeto_Back.Models.StopPoint", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("creationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("geolocalizacaoId")
                        .HasColumnType("char(36)");

                    b.Property<float>("latitude")
                        .HasMaxLength(90)
                        .HasColumnType("float");

                    b.Property<float>("longitude")
                        .HasMaxLength(180)
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("geolocalizacaoId");

                    b.ToTable("StopPoints");
                });

            modelBuilder.Entity("ESProjeto_Back.Models.Token", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<bool>("IsValid")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid?>("OldRefreshToken")
                        .HasColumnType("char(36)");

                    b.Property<int>("TipoToken")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("Validate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("token")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("OldRefreshToken")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("Token");
                });

            modelBuilder.Entity("ESProjeto_Back.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("NickName")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("varchar(1000)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Perfil")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<string>("RecoveryCode")
                        .HasMaxLength(6)
                        .HasColumnType("varchar(6)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ESProjeto_Back.Models.GeolocationPosition", b =>
                {
                    b.HasOne("ESProjeto_Back.Models.GeolocationCoordinates", "coords")
                        .WithMany()
                        .HasForeignKey("coordsId");

                    b.Navigation("coords");
                });

            modelBuilder.Entity("ESProjeto_Back.Models.StopPoint", b =>
                {
                    b.HasOne("ESProjeto_Back.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ESProjeto_Back.Models.GeolocationPosition", "geolocalizacao")
                        .WithMany()
                        .HasForeignKey("geolocalizacaoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("geolocalizacao");
                });

            modelBuilder.Entity("ESProjeto_Back.Models.Token", b =>
                {
                    b.HasOne("ESProjeto_Back.Models.Token", "RefreshToken")
                        .WithOne()
                        .HasForeignKey("ESProjeto_Back.Models.Token", "OldRefreshToken");

                    b.HasOne("ESProjeto_Back.Models.User", "User")
                        .WithMany("Tokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RefreshToken");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ESProjeto_Back.Models.User", b =>
                {
                    b.Navigation("Tokens");
                });
#pragma warning restore 612, 618
        }
    }
}
