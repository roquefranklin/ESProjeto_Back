using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESProjeto_Back.Migrations
{
    /// <inheritdoc />
    public partial class stoppointrepo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GeolocationCoordinates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    accuracy = table.Column<float>(type: "float", nullable: true),
                    altitude = table.Column<float>(type: "float", nullable: true),
                    altitudeAccuracy = table.Column<float>(type: "float", nullable: true),
                    heading = table.Column<float>(type: "float", nullable: true),
                    latitude = table.Column<float>(type: "float", nullable: true),
                    longitude = table.Column<float>(type: "float", nullable: true),
                    speed = table.Column<float>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeolocationCoordinates", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "GeolocationPosition",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    coordsId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    timestamp = table.Column<float>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeolocationPosition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GeolocationPosition_GeolocationCoordinates_coordsId",
                        column: x => x.coordsId,
                        principalTable: "GeolocationCoordinates",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "StopPoints",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    geolocalizacaoId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    latitude = table.Column<float>(type: "float", maxLength: 90, nullable: false),
                    longitude = table.Column<float>(type: "float", maxLength: 180, nullable: false),
                    creationDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StopPoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StopPoints_GeolocationPosition_geolocalizacaoId",
                        column: x => x.geolocalizacaoId,
                        principalTable: "GeolocationPosition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StopPoints_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_GeolocationPosition_coordsId",
                table: "GeolocationPosition",
                column: "coordsId");

            migrationBuilder.CreateIndex(
                name: "IX_StopPoints_geolocalizacaoId",
                table: "StopPoints",
                column: "geolocalizacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_StopPoints_UserId",
                table: "StopPoints",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StopPoints");

            migrationBuilder.DropTable(
                name: "GeolocationPosition");

            migrationBuilder.DropTable(
                name: "GeolocationCoordinates");
        }
    }
}
