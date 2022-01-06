using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WDPR_A.Migrations
{
    public partial class orthopedagogen : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Specialty", "TwoFactorEnabled", "UserName" },
                values: new object[] { "ce145a14-8b20-493c-b213-465e5b306ec9", 0, "a68c568c-5a95-4410-9883-f6076b62f60e", "Orthopedagogue", null, false, "Karin", "Kemper", false, null, null, null, null, null, false, "32a9e103-d57c-4835-8402-972a5b656869", "ADHD", false, null });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Specialty", "TwoFactorEnabled", "UserName" },
                values: new object[] { "df2b36f9-c76a-4fd1-b45d-ad8edaaa8a9a", 0, "bbf8c28e-b698-49ae-9ad7-62d4c92bb702", "Orthopedagogue", null, false, "Johan", "Lo", false, null, null, null, null, null, false, "d34732d2-4224-4d99-b8a7-00c0c8aac564", "Faalangst", false, null });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Specialty", "TwoFactorEnabled", "UserName" },
                values: new object[] { "e12cec07-efd7-4fe3-b1a6-1e7e22e8ce2e", 0, "a7a7b6c0-9e66-4b27-b793-c1d5a6d6a53c", "Orthopedagogue", null, false, "Marianne", "van Dijk", false, null, null, null, null, null, false, "2c40f74e-6071-43b6-8f42-b1d0864c7297", "Dyslexie", false, null });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Specialty", "TwoFactorEnabled", "UserName" },
                values: new object[] { "f0f4e891-a058-4537-91c2-361b72fda299", 0, "70aa93d8-0dfd-439f-a7ba-8bc9af1e950f", "Orthopedagogue", null, false, "Steven", "Ito", false, null, null, null, null, null, false, "d1f30766-7287-422a-8591-488403ea4634", "Eetstoornis", false, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ce145a14-8b20-493c-b213-465e5b306ec9");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "df2b36f9-c76a-4fd1-b45d-ad8edaaa8a9a");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e12cec07-efd7-4fe3-b1a6-1e7e22e8ce2e");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f0f4e891-a058-4537-91c2-361b72fda299");
        }
    }
}
