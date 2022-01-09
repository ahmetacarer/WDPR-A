using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WDPR_A.Migrations
{
    public partial class _25 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "AgeCategory",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AppointmentId",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IncomingClientId",
                table: "Appointments",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Specialty", "TwoFactorEnabled", "UserName" },
                values: new object[] { "2dfade34-5ba3-4bc2-b50d-c5d4e5d29027", 0, "5eb0bf2e-2da8-49a7-9a02-21eee0f9c5e2", "Orthopedagogue", null, false, "Karin", "Kemper", false, null, null, null, null, null, false, "aecd87d1-927d-42f1-a4e1-90808961daf4", "ADHD", false, null });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Specialty", "TwoFactorEnabled", "UserName" },
                values: new object[] { "536ffd33-7cd8-4443-93bd-1273895e55dd", 0, "8d01ea89-dea6-40b9-a004-6feb7f802f10", "Orthopedagogue", null, false, "Steven", "Ito", false, null, null, null, null, null, false, "8e8d5f71-3839-42b3-822e-60a772eedc18", "Eetstoornis", false, null });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Specialty", "TwoFactorEnabled", "UserName" },
                values: new object[] { "67f88bfb-409f-417d-8f7b-fec56e2fcca3", 0, "349593c8-3728-4372-b320-fe3c8f312f16", "Orthopedagogue", null, false, "Johan", "Lo", false, null, null, null, null, null, false, "1ed6963c-5866-4901-a5dd-41626d3d69b4", "Faalangst", false, null });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Specialty", "TwoFactorEnabled", "UserName" },
                values: new object[] { "fdb08b90-f8bd-4f19-8c1e-f67a580491a2", 0, "5ac09d5d-08c8-47a6-bb13-5f45e12c875a", "Orthopedagogue", null, false, "Marianne", "van Dijk", false, null, null, null, null, null, false, "64242c79-45a1-4a08-ab1a-e30d6bac797a", "Dyslexie", false, null });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AppointmentId",
                table: "AspNetUsers",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_IncomingClientId",
                table: "Appointments",
                column: "IncomingClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_AspNetUsers_IncomingClientId",
                table: "Appointments",
                column: "IncomingClientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Appointments_AppointmentId",
                table: "AspNetUsers",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_AspNetUsers_IncomingClientId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Appointments_AppointmentId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_AppointmentId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_IncomingClientId",
                table: "Appointments");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2dfade34-5ba3-4bc2-b50d-c5d4e5d29027");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "536ffd33-7cd8-4443-93bd-1273895e55dd");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "67f88bfb-409f-417d-8f7b-fec56e2fcca3");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fdb08b90-f8bd-4f19-8c1e-f67a580491a2");

            migrationBuilder.DropColumn(
                name: "AgeCategory",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AppointmentId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IncomingClientId",
                table: "Appointments");

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
    }
}
