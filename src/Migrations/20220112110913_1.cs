using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WDPR_A.Migrations
{
<<<<<<< HEAD:src/Migrations/20220111092726_emailunique.cs
    public partial class emailunique : Migration
=======
    public partial class _1 : Migration
>>>>>>> 83871fdde6a628a569398bf7d5776a60735f8794:src/Migrations/20220112110913_1.cs
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AppointmentDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IncomingClientId = table.Column<string>(type: "TEXT", nullable: false),
                    OrthopedagogueId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Discriminator = table.Column<string>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: true),
                    LastName = table.Column<string>(type: "TEXT", nullable: true),
                    Condition = table.Column<string>(type: "TEXT", nullable: true),
                    AgeCategory = table.Column<int>(type: "INTEGER", nullable: true),
<<<<<<< HEAD:src/Migrations/20220111092726_emailunique.cs
                    ChatCode = table.Column<string>(type: "TEXT", nullable: true),
=======
                    PrivateChatToken = table.Column<string>(type: "TEXT", nullable: true),
>>>>>>> 83871fdde6a628a569398bf7d5776a60735f8794:src/Migrations/20220112110913_1.cs
                    AppointmentId = table.Column<int>(type: "INTEGER", nullable: true),
                    Specialty = table.Column<string>(type: "TEXT", nullable: true),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: true),
                    SecurityStamp = table.Column<string>(type: "TEXT", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Appointments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "TEXT", nullable: true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    LoginProvider = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Chats",
                columns: table => new
                {
<<<<<<< HEAD:src/Migrations/20220111092726_emailunique.cs
                    Code = table.Column<string>(type: "TEXT", nullable: false),
=======
                    RoomId = table.Column<string>(type: "TEXT", nullable: false),
                    RoomName = table.Column<string>(type: "TEXT", nullable: true),
                    PrivateChatToken = table.Column<string>(type: "TEXT", nullable: true),
                    Subject = table.Column<string>(type: "TEXT", nullable: true),
                    IsPrivate = table.Column<bool>(type: "INTEGER", nullable: false),
                    AgeCategory = table.Column<int>(type: "INTEGER", nullable: false),
>>>>>>> 83871fdde6a628a569398bf7d5776a60735f8794:src/Migrations/20220112110913_1.cs
                    OrthopedagogueId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
<<<<<<< HEAD:src/Migrations/20220111092726_emailunique.cs
                    table.PrimaryKey("PK_Chats", x => x.Code);
=======
                    table.PrimaryKey("PK_Chats", x => x.RoomId);
>>>>>>> 83871fdde6a628a569398bf7d5776a60735f8794:src/Migrations/20220112110913_1.cs
                    table.ForeignKey(
                        name: "FK_Chats_AspNetUsers_OrthopedagogueId",
                        column: x => x.OrthopedagogueId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientGuardian",
                columns: table => new
                {
                    ClientsId = table.Column<string>(type: "TEXT", nullable: false),
                    GuardiansId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientGuardian", x => new { x.ClientsId, x.GuardiansId });
                    table.ForeignKey(
                        name: "FK_ClientGuardian_AspNetUsers_ClientsId",
                        column: x => x.ClientsId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientGuardian_AspNetUsers_GuardiansId",
                        column: x => x.GuardiansId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChatClient",
                columns: table => new
                {
<<<<<<< HEAD:src/Migrations/20220111092726_emailunique.cs
                    ChatsCode = table.Column<string>(type: "TEXT", nullable: false),
=======
                    ChatsRoomId = table.Column<string>(type: "TEXT", nullable: false),
>>>>>>> 83871fdde6a628a569398bf7d5776a60735f8794:src/Migrations/20220112110913_1.cs
                    ClientsId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
<<<<<<< HEAD:src/Migrations/20220111092726_emailunique.cs
                    table.PrimaryKey("PK_ChatClient", x => new { x.ChatsCode, x.ClientsId });
=======
                    table.PrimaryKey("PK_ChatClient", x => new { x.ChatsRoomId, x.ClientsId });
>>>>>>> 83871fdde6a628a569398bf7d5776a60735f8794:src/Migrations/20220112110913_1.cs
                    table.ForeignKey(
                        name: "FK_ChatClient_AspNetUsers_ClientsId",
                        column: x => x.ClientsId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
<<<<<<< HEAD:src/Migrations/20220111092726_emailunique.cs
                        name: "FK_ChatClient_Chats_ChatsCode",
                        column: x => x.ChatsCode,
                        principalTable: "Chats",
                        principalColumn: "Code",
=======
                        name: "FK_ChatClient_Chats_ChatsRoomId",
                        column: x => x.ChatsRoomId,
                        principalTable: "Chats",
                        principalColumn: "RoomId",
>>>>>>> 83871fdde6a628a569398bf7d5776a60735f8794:src/Migrations/20220112110913_1.cs
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
<<<<<<< HEAD:src/Migrations/20220111092726_emailunique.cs
                    UserName = table.Column<string>(type: "TEXT", nullable: false),
                    Text = table.Column<string>(type: "TEXT", nullable: false),
                    When = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ChatCode = table.Column<string>(type: "TEXT", nullable: false)
=======
                    SenderId = table.Column<string>(type: "TEXT", nullable: false),
                    Text = table.Column<string>(type: "TEXT", nullable: false),
                    When = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ChatRoomId = table.Column<string>(type: "TEXT", nullable: false)
>>>>>>> 83871fdde6a628a569398bf7d5776a60735f8794:src/Migrations/20220112110913_1.cs
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
<<<<<<< HEAD:src/Migrations/20220111092726_emailunique.cs
                        name: "FK_Messages_Chats_ChatCode",
                        column: x => x.ChatCode,
                        principalTable: "Chats",
                        principalColumn: "Code",
=======
                        name: "FK_Messages_AspNetUsers_SenderId",
                        column: x => x.SenderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Messages_Chats_ChatRoomId",
                        column: x => x.ChatRoomId,
                        principalTable: "Chats",
                        principalColumn: "RoomId",
>>>>>>> 83871fdde6a628a569398bf7d5776a60735f8794:src/Migrations/20220112110913_1.cs
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Specialty", "TwoFactorEnabled", "UserName" },
<<<<<<< HEAD:src/Migrations/20220111092726_emailunique.cs
                values: new object[] { "1846f376-ca44-43ca-8354-3bc4eb50645b", 0, "156087a1-311e-4fd4-9b0a-109fdf828b03", "Orthopedagogue", null, false, "Karin", "Kemper", false, null, null, null, null, null, false, "e4ebe808-cb81-4f29-9f9a-ee9fad4d16a5", "ADHD", false, null });
=======
                values: new object[] { "4953e73d-f695-4bf0-a6ab-95d81f8e4b4a", 0, "99c3f94e-1770-4699-9e1a-360fc901f7e0", "Orthopedagogue", null, false, "Karin", "Kemper", false, null, null, null, null, null, false, "727e981a-7fb5-4b0e-b256-d4589ccc3d4e", "ADHD", false, null });
>>>>>>> 83871fdde6a628a569398bf7d5776a60735f8794:src/Migrations/20220112110913_1.cs

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Specialty", "TwoFactorEnabled", "UserName" },
<<<<<<< HEAD:src/Migrations/20220111092726_emailunique.cs
                values: new object[] { "329049af-e16b-4b09-9e66-7ce2c5021a5b", 0, "db83ddff-9110-40c1-b60f-7c9f206db7e1", "Orthopedagogue", null, false, "Johan", "Lo", false, null, null, null, null, null, false, "24ce9520-01f6-4090-96ef-dad47aacf1bc", "Faalangst", false, null });
=======
                values: new object[] { "6c0c96e5-8958-4fc2-aa41-f2771d820d6d", 0, "3419e1a6-3fae-435e-8d16-d2e4a6a3bfaf", "Orthopedagogue", null, false, "Johan", "Lo", false, null, null, null, null, null, false, "7facc0e5-bd07-4914-9025-0936ff80dd78", "Faalangst", false, null });
>>>>>>> 83871fdde6a628a569398bf7d5776a60735f8794:src/Migrations/20220112110913_1.cs

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Specialty", "TwoFactorEnabled", "UserName" },
<<<<<<< HEAD:src/Migrations/20220111092726_emailunique.cs
                values: new object[] { "9391018f-6043-4a98-94af-5548c6c03148", 0, "db93429a-6568-45b4-8d73-bed682f010c3", "Orthopedagogue", null, false, "Steven", "Ito", false, null, null, null, null, null, false, "36d9295b-3f37-43e4-8bdb-bc863e48859d", "Eetstoornis", false, null });
=======
                values: new object[] { "cea3e040-9604-4970-8512-bb96fe8c8ff3", 0, "fbada17c-6977-4010-8ef2-c2623b0d417b", "Orthopedagogue", null, false, "Steven", "Ito", false, null, null, null, null, null, false, "6b9f99cc-c8f6-4b17-903f-c876b74bb41a", "Eetstoornis", false, null });
>>>>>>> 83871fdde6a628a569398bf7d5776a60735f8794:src/Migrations/20220112110913_1.cs

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Specialty", "TwoFactorEnabled", "UserName" },
<<<<<<< HEAD:src/Migrations/20220111092726_emailunique.cs
                values: new object[] { "986a8c41-850f-4baf-b3d1-f96de52d13ec", 0, "103f5917-d6d7-48b7-8465-6cda3200d905", "Orthopedagogue", null, false, "Marianne", "van Dijk", false, null, null, null, null, null, false, "138e9d82-38b4-437b-bc70-097edbcdf82b", "Dyslexie", false, null });
=======
                values: new object[] { "d0e8944d-b908-4f9f-97f5-6b89b6d68667", 0, "043e0318-366e-4026-bafd-91d4ba15ede7", "Orthopedagogue", null, false, "Marianne", "van Dijk", false, null, null, null, null, null, false, "c1f37188-4232-4430-92c8-5d47e83b2cb6", "Dyslexie", false, null });
>>>>>>> 83871fdde6a628a569398bf7d5776a60735f8794:src/Migrations/20220112110913_1.cs

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_IncomingClientId",
                table: "Appointments",
                column: "IncomingClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_OrthopedagogueId",
                table: "Appointments",
                column: "OrthopedagogueId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AppointmentId",
                table: "AspNetUsers",
                column: "AppointmentId");
<<<<<<< HEAD:src/Migrations/20220111092726_emailunique.cs

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Email",
                table: "AspNetUsers",
                column: "Email",
                unique: true);
=======
>>>>>>> 83871fdde6a628a569398bf7d5776a60735f8794:src/Migrations/20220112110913_1.cs

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChatClient_ClientsId",
                table: "ChatClient",
                column: "ClientsId");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_OrthopedagogueId",
                table: "Chats",
                column: "OrthopedagogueId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientGuardian_GuardiansId",
                table: "ClientGuardian",
                column: "GuardiansId");

            migrationBuilder.CreateIndex(
<<<<<<< HEAD:src/Migrations/20220111092726_emailunique.cs
                name: "IX_Messages_ChatCode",
                table: "Messages",
                column: "ChatCode");
=======
                name: "IX_Messages_ChatRoomId",
                table: "Messages",
                column: "ChatRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_SenderId",
                table: "Messages",
                column: "SenderId");
>>>>>>> 83871fdde6a628a569398bf7d5776a60735f8794:src/Migrations/20220112110913_1.cs

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_AspNetUsers_IncomingClientId",
                table: "Appointments",
                column: "IncomingClientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_AspNetUsers_OrthopedagogueId",
                table: "Appointments",
                column: "OrthopedagogueId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_AspNetUsers_IncomingClientId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_AspNetUsers_OrthopedagogueId",
                table: "Appointments");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "ChatClient");

            migrationBuilder.DropTable(
                name: "ClientGuardian");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Chats");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Appointments");
        }
    }
}
