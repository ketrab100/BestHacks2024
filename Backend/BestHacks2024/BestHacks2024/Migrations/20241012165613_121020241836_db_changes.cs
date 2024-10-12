using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BestHacks2024.Migrations
{
    /// <inheritdoc />
    public partial class _121020241836_db_changes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MatchScore",
                table: "Matches");

            migrationBuilder.AddColumn<bool>(
                name: "AreMatched",
                table: "Matches",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "DidEmployeeAcceptJobOffer",
                table: "Matches",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "DidEmployerAcceptCandidate",
                table: "Matches",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AreMatched",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "DidEmployeeAcceptJobOffer",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "DidEmployerAcceptCandidate",
                table: "Matches");

            migrationBuilder.AddColumn<decimal>(
                name: "MatchScore",
                table: "Matches",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
