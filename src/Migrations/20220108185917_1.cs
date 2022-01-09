using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WDPR_A.Migrations
{
    public partial class _1 : Migration
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

            migrationBuilder.CreateTable(
                name: "Chats",
                columns: table => new
                {
                    Code = table.Column<string>(type: "TEXT", nullable: false),
                    OrthopedagogueId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chats", x => x.Code);
                    table.ForeignKey(
                        name: "FK_Chats_AspNetUsers_OrthopedagogueId",
                        column: x => x.OrthopedagogueId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChatClient",
                columns: table => new
                {
                    ChatsCode = table.Column<string>(type: "TEXT", nullable: false),
                    ClientsId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatClient", x => new { x.ChatsCode, x.ClientsId });
                    table.ForeignKey(
                        name: "FK_ChatClient_AspNetUsers_ClientsId",
                        column: x => x.ClientsId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChatClient_Chats_ChatsCode",
                        column: x => x.ChatsCode,
                        principalTable: "Chats",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserName = table.Column<string>(type: "TEXT", nullable: false),
                    Text = table.Column<string>(type: "TEXT", nullable: false),
                    When = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ChatCode = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_Chats_ChatCode",
                        column: x => x.ChatCode,
                        principalTable: "Chats",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Specialty", "TwoFactorEnabled", "UserName" },
                values: new object[] { "17c5f728-1c3d-4256-a9de-0bfe465235b6", 0, "171f7824-bf72-4108-84bb-b6fc32c9733c", "Orthopedagogue", null, false, "Steven", "Ito", false, null, null, null, null, null, false, "2984f629-5333-4f14-9aef-cce84b594909", "Eetstoornis", false, null });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Specialty", "TwoFactorEnabled", "UserName" },
                values: new object[] { "a953b137-09d8-4ea1-8a8e-808598968614", 0, "cd363cc2-cf57-4f35-ac93-6292a3cd49b2", "Orthopedagogue", null, false, "Johan", "Lo", false, null, null, null, null, null, false, "b46e0986-bf95-4f99-b8ad-49d8eae2a571", "Faalangst", false, null });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Specialty", "TwoFactorEnabled", "UserName" },
                values: new object[] { "b234a553-a0e4-4af7-9af6-2130d7de31b7", 0, "aa5b1518-a4d9-4103-8f60-5c0fe02e973a", "Orthopedagogue", null, false, "Karin", "Kemper", false, null, null, null, null, null, false, "62bd6e13-0a49-4868-9e98-8f2dea0e2535", "ADHD", false, null });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Specialty", "TwoFactorEnabled", "UserName" },
                values: new object[] { "d89f02cf-e741-42e3-b53a-d77738967d8e", 0, "d88a2bd0-7ca2-44b9-b44a-0e2c566d6ac1", "Orthopedagogue", null, false, "Marianne", "van Dijk", false, null, null, null, null, null, false, "895be6b3-b514-477b-a769-fdc486fb997c", "Dyslexie", false, null });

            migrationBuilder.CreateIndex(
                name: "IX_ChatClient_ClientsId",
                table: "ChatClient",
                column: "ClientsId");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_OrthopedagogueId",
                table: "Chats",
                column: "OrthopedagogueId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ChatCode",
                table: "Messages",
                column: "ChatCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatClient");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Chats");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "17c5f728-1c3d-4256-a9de-0bfe465235b6");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a953b137-09d8-4ea1-8a8e-808598968614");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b234a553-a0e4-4af7-9af6-2130d7de31b7");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d89f02cf-e741-42e3-b53a-d77738967d8e");

            migrationBuilder.DropColumn(
                name: "AgeCategory",
                table: "AspNetUsers");

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
