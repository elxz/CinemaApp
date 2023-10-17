﻿// <auto-generated />
using System;
using Cinema.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Cinema.Migrations
{
    [DbContext(typeof(CinemaDbContext))]
    partial class CinemaDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Cinema.Models.AgeRestriction", b =>
                {
                    b.Property<int>("restriction_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("restriction_id"));

                    b.Property<string>("restriction")
                        .IsRequired()
                        .HasColumnType("varchar(5)");

                    b.HasKey("restriction_id");

                    b.ToTable("age_restriction");
                });

            modelBuilder.Entity("Cinema.Models.CustomerTicket", b =>
                {
                    b.Property<int>("ct_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ct_id"));

                    b.Property<int>("fk_ticket")
                        .HasColumnType("integer");

                    b.Property<int>("fk_user")
                        .HasColumnType("integer");

                    b.HasKey("ct_id");

                    b.ToTable("customer_ticket");
                });

            modelBuilder.Entity("Cinema.Models.Director", b =>
                {
                    b.Property<int>("director_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("director_id"));

                    b.Property<string>("director_firstname")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("director_lastname")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(50)");

                    b.HasKey("director_id");

                    b.ToTable("director");
                });

            modelBuilder.Entity("Cinema.Models.Film", b =>
                {
                    b.Property<int>("film_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("film_id"));

                    b.Property<decimal>("budget")
                        .HasColumnType("money");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar(1000)");

                    b.Property<TimeSpan>("duration")
                        .HasColumnType("interval");

                    b.Property<string>("film_name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.Property<int>("fk_director")
                        .HasColumnType("integer");

                    b.Property<int>("fk_restriction")
                        .HasColumnType("integer");

                    b.Property<string>("poster")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<float>("rating")
                        .HasColumnType("real");

                    b.Property<DateTime>("release_date")
                        .HasColumnType("date");

                    b.HasKey("film_id");

                    b.ToTable("film");
                });

            modelBuilder.Entity("Cinema.Models.FilmCountry", b =>
                {
                    b.Property<int>("fc_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("fc_id"));

                    b.Property<int>("fk_country")
                        .HasColumnType("integer");

                    b.Property<int>("fk_film")
                        .HasColumnType("integer");

                    b.HasKey("fc_id");

                    b.ToTable("film_country");
                });

            modelBuilder.Entity("Cinema.Models.FilmGenre", b =>
                {
                    b.Property<int>("fg_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("fg_id"));

                    b.Property<int>("fk_film")
                        .HasColumnType("integer");

                    b.Property<int>("fk_genre")
                        .HasColumnType("integer");

                    b.HasKey("fg_id");

                    b.ToTable("film_genre");
                });

            modelBuilder.Entity("Cinema.Models.Genre", b =>
                {
                    b.Property<int>("genre_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("genre_id"));

                    b.Property<string>("genre_name")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.HasKey("genre_id");

                    b.ToTable("genre");
                });

            modelBuilder.Entity("Cinema.Models.Hall", b =>
                {
                    b.Property<int>("hall_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("hall_id"));

                    b.Property<int>("count_of_rows")
                        .HasColumnType("integer");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("varchar(1000)");

                    b.Property<string>("hall_image")
                        .IsRequired()
                        .HasColumnType("varchar(500)");

                    b.Property<int>("hall_number")
                        .HasColumnType("integer");

                    b.Property<int>("places_per_row")
                        .HasColumnType("integer");

                    b.Property<string>("screen_type")
                        .IsRequired()
                        .HasColumnType("varchar(10)");

                    b.HasKey("hall_id");

                    b.ToTable("hall");
                });

            modelBuilder.Entity("Cinema.Models.OriginCountry", b =>
                {
                    b.Property<int>("country_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("country_id"));

                    b.Property<string>("country_name")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("country_id");

                    b.ToTable("origin_country");
                });

            modelBuilder.Entity("Cinema.Models.Pricing", b =>
                {
                    b.Property<int>("pricing_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("pricing_id"));

                    b.Property<int>("fk_hall")
                        .HasColumnType("integer");

                    b.Property<decimal>("price")
                        .HasColumnType("money");

                    b.HasKey("pricing_id");

                    b.ToTable("pricing");
                });

            modelBuilder.Entity("Cinema.Models.Session", b =>
                {
                    b.Property<int>("session_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("session_id"));

                    b.Property<TimeSpan>("end_time")
                        .HasColumnType("interval");

                    b.Property<int>("fk_film")
                        .HasColumnType("integer");

                    b.Property<int>("fk_hall")
                        .HasColumnType("integer");

                    b.Property<DateTime>("session_date")
                        .HasColumnType("date");

                    b.Property<TimeSpan>("start_time")
                        .HasColumnType("interval");

                    b.HasKey("session_id");

                    b.ToTable("session_");
                });

            modelBuilder.Entity("Cinema.Models.SessionRowPlace", b =>
                {
                    b.Property<int>("srp_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("srp_id"));

                    b.Property<int>("fk_session")
                        .HasColumnType("integer");

                    b.Property<int>("fk_user")
                        .HasColumnType("integer");

                    b.Property<int>("place_")
                        .HasColumnType("integer");

                    b.Property<int>("row_")
                        .HasColumnType("integer");

                    b.Property<string>("status")
                        .IsRequired()
                        .HasColumnType("varchar(15)");

                    b.HasKey("srp_id");

                    b.ToTable("session_row_place");
                });

            modelBuilder.Entity("Cinema.Models.Ticket", b =>
                {
                    b.Property<int>("ticket_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ticket_id"));

                    b.Property<int>("fk_price")
                        .HasColumnType("integer");

                    b.Property<int>("fk_srp")
                        .HasColumnType("integer");

                    b.Property<int>("fk_user")
                        .HasColumnType("integer");

                    b.Property<DateTime>("sale_date")
                        .HasColumnType("date");

                    b.HasKey("ticket_id");

                    b.ToTable("ticket");
                });

            modelBuilder.Entity("Cinema.Models.User", b =>
                {
                    b.Property<int>("user_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("user_id"));

                    b.Property<string>("role_")
                        .IsRequired()
                        .HasColumnType("varchar(10)");

                    b.Property<DateTime>("user_date_of_birth")
                        .HasColumnType("date");

                    b.Property<string>("user_firstname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("user_lastname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("user_login")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("user_password")
                        .IsRequired()
                        .HasColumnType("varchar(64)");

                    b.Property<string>("user_phone_number")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("varchar(12)");

                    b.HasKey("user_id");

                    b.ToTable("user_");
                });
#pragma warning restore 612, 618
        }
    }
}
