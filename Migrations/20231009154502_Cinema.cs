using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Cinema.Migrations
{
    /// <inheritdoc />
    public partial class Cinema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "age_restriction",
                columns: table => new
                {
                    restriction_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    restriction = table.Column<string>(type: "varchar(5)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_age_restriction", x => x.restriction_id);
                });

            migrationBuilder.CreateTable(
                name: "customer_ticket",
                columns: table => new
                {
                    ct_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    fk_user = table.Column<int>(type: "integer", nullable: false),
                    fk_ticket = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customer_ticket", x => x.ct_id);
                });

            migrationBuilder.CreateTable(
                name: "director",
                columns: table => new
                {
                    director_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    director_firstname = table.Column<string>(type: "varchar(50)", maxLength: 20, nullable: false),
                    director_lastname = table.Column<string>(type: "varchar(50)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_director", x => x.director_id);
                });

            migrationBuilder.CreateTable(
                name: "film",
                columns: table => new
                {
                    film_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    film_name = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    description = table.Column<string>(type: "varchar(1000)", maxLength: 150, nullable: false),
                    release_date = table.Column<DateTime>(type: "date", nullable: false),
                    duration = table.Column<TimeSpan>(type: "interval", nullable: false),
                    rating = table.Column<float>(type: "real", nullable: false),
                    budget = table.Column<decimal>(type: "money", nullable: false),
                    poster = table.Column<string>(type: "text", nullable: false),
                    fk_director = table.Column<int>(type: "integer", nullable: false),
                    fk_restriction = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_film", x => x.film_id);
                });

            migrationBuilder.CreateTable(
                name: "film_country",
                columns: table => new
                {
                    fc_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    fk_film = table.Column<int>(type: "integer", nullable: false),
                    fk_country = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_film_country", x => x.fc_id);
                });

            migrationBuilder.CreateTable(
                name: "film_genre",
                columns: table => new
                {
                    fg_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    fk_film = table.Column<int>(type: "integer", nullable: false),
                    fk_genre = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_film_genre", x => x.fg_id);
                });

            migrationBuilder.CreateTable(
                name: "genre",
                columns: table => new
                {
                    genre_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    genre_name = table.Column<string>(type: "varchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_genre", x => x.genre_id);
                });

            migrationBuilder.CreateTable(
                name: "hall",
                columns: table => new
                {
                    hall_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    hall_number = table.Column<int>(type: "integer", nullable: false),
                    screen_type = table.Column<string>(type: "varchar(10)", nullable: false),
                    count_of_rows = table.Column<int>(type: "integer", nullable: false),
                    places_per_row = table.Column<int>(type: "integer", nullable: false),
                    description = table.Column<string>(type: "varchar(1000)", nullable: false),
                    hall_image = table.Column<string>(type: "varchar(500)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hall", x => x.hall_id);
                });

            migrationBuilder.CreateTable(
                name: "origin_country",
                columns: table => new
                {
                    country_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    country_name = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_origin_country", x => x.country_id);
                });

            migrationBuilder.CreateTable(
                name: "pricing",
                columns: table => new
                {
                    pricing_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    price = table.Column<decimal>(type: "money", nullable: false),
                    fk_hall = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pricing", x => x.pricing_id);
                });

            migrationBuilder.CreateTable(
                name: "session_",
                columns: table => new
                {
                    session_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    fk_film = table.Column<int>(type: "integer", nullable: false),
                    fk_hall = table.Column<int>(type: "integer", nullable: false),
                    session_date = table.Column<DateTime>(type: "date", nullable: false),
                    start_time = table.Column<TimeSpan>(type: "interval", nullable: false),
                    end_time = table.Column<TimeSpan>(type: "interval", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_session_", x => x.session_id);
                });

            migrationBuilder.CreateTable(
                name: "session_row_place",
                columns: table => new
                {
                    srp_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    fk_session = table.Column<int>(type: "integer", nullable: false),
                    row_ = table.Column<int>(type: "integer", nullable: false),
                    place_ = table.Column<int>(type: "integer", nullable: false),
                    status = table.Column<string>(type: "varchar(15)", nullable: false),
                    fk_user = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_session_row_place", x => x.srp_id);
                });

            migrationBuilder.CreateTable(
                name: "ticket",
                columns: table => new
                {
                    ticket_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    fk_srp = table.Column<int>(type: "integer", nullable: false),
                    sale_date = table.Column<DateTime>(type: "date", nullable: false),
                    fk_price = table.Column<int>(type: "integer", nullable: false),
                    fk_user = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ticket", x => x.ticket_id);
                });

            migrationBuilder.CreateTable(
                name: "user_",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_firstname = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    user_lastname = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    user_login = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    user_password = table.Column<string>(type: "varchar(64)", nullable: false),
                    user_phone_number = table.Column<string>(type: "varchar(12)", maxLength: 12, nullable: false),
                    user_date_of_birth = table.Column<DateTime>(type: "date", nullable: false),
                    role_ = table.Column<string>(type: "varchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_", x => x.user_id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "age_restriction");

            migrationBuilder.DropTable(
                name: "customer_ticket");

            migrationBuilder.DropTable(
                name: "director");

            migrationBuilder.DropTable(
                name: "film");

            migrationBuilder.DropTable(
                name: "film_country");

            migrationBuilder.DropTable(
                name: "film_genre");

            migrationBuilder.DropTable(
                name: "genre");

            migrationBuilder.DropTable(
                name: "hall");

            migrationBuilder.DropTable(
                name: "origin_country");

            migrationBuilder.DropTable(
                name: "pricing");

            migrationBuilder.DropTable(
                name: "session_");

            migrationBuilder.DropTable(
                name: "session_row_place");

            migrationBuilder.DropTable(
                name: "ticket");

            migrationBuilder.DropTable(
                name: "user_");
        }
    }
}
