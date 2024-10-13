using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BestHacks2024.Migrations
{
    /// <inheritdoc />
    public partial class customSql : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            INSERT INTO ""Tags"" (""Id"", ""Name"") VALUES
            ('fb5c9f99-442a-48db-8c63-93db890fa1a9', 'Ruby'),
            ('96c5f48c-d9f5-4b62-9cd9-8d2db15b3c82', 'Java'),
            ('cd5f46b8-c7a7-450b-80b8-40d9c1b459ef', 'SQL');
        ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
