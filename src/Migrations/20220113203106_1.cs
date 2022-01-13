using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WDPR_A.Migrations
{
    public partial class _1 : Migration
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
                    ChatCode = table.Column<string>(type: "TEXT", nullable: true),
                    AppointmentId = table.Column<int>(type: "INTEGER", nullable: true),
                    Specialty = table.Column<string>(type: "TEXT", nullable: true),
                    PictureUrl = table.Column<string>(type: "TEXT", nullable: true),
                    OrthopedagogueWebText = table.Column<string>(type: "TEXT", nullable: true),
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
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "OrthopedagogueWebText", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PictureUrl", "SecurityStamp", "Specialty", "TwoFactorEnabled", "UserName" },
                values: new object[] { "044a8007-709e-4ea3-acec-c0b4d45c6083", 0, "0f709a2b-8468-43ba-bfe9-bcf903cef4dc", "Orthopedagogue", null, false, "Steven", "Ito", false, null, null, null, "Even voorstellen   \r\n Mijn naam is Steven Ito, ik ben geboren in 1987 in het hartje van Amsterdam. Mijn ouders hadden allebei een medische achtergrond en komen oorspronkelijk uit Japan. Ik heb al sinds mijn jeugd een passie gehad voor de medische wereld en het helpen van mensen, specifiek de jongeren. Omdat het mij interesseert hoe jongeren hun leven leiden. \r\n\r\n \r\nMijn studie   \r\n Na het VWO heb ik orthopedagogiek gestudeerd aan de Universiteit van Amsterdam. Bij mijn afstuderen heb ik mij gespecialiseerd in de behandeling van eetstoornissen. Daarna heb ik nog een tweejarig masterprogramma gevolgd in het buitenland op de Medische Universiteit van Tokyo. \r\n\r\n \r\nNu over jou: jij hebt misschien een eetstoornis  \r\n Bij jou bestaat het vermoeden dat je een eetstoornis hebt. Soms wordt gedacht dat er maar één eetstoornis is. Maar dit klopt niet, want er zijn verschillende soorten zoals: boulimia, anorexia en Binge Eating Disorder (BED). Sommige eetstoornissen zijn niet altijd zichtbaar en worden daarom ook ‘onzichtbare eetstoornissen’ genoemd. Als het goed is merk je dat je een verstoord en onregelmatig eetgedrag hebt. Dit wordt gekenmerkt dat er soms te weinig wordt gegeten en in andere perioden juist te veel wordt gegeten. Het heeft voornamelijk te maken met een verstoord lichaamsbeeld. Misschien heb je gemerkt dat je een angst hebt om dik te worden of juist om dun te worden, toch? Verder zijn er lichamelijke klachten zoals misselijkheid of maagpijn. Daarnaast herken je misschien dat je liever niet wilt eten als andere mensen in de buurt zijn. \r\n\r\n  \r\n\r\n \r\nWat gaan we doen?   \r\n\r\nIk ga met jou samen onderzoeken of jij een eetstoornis hebt met een paar testen, waarbij eventueel jouw ouder(s)/verzorger(s) betrokken worden. Mocht blijken uit de resultaten dat je toch een eetstoornis hebt. Dan zullen we samen bekijken waar jij moeite mee hebt of tegen aanloopt. We zullen dan samen stap-voor-stap ervoor zorgen dat we jouw eetstoornis en verstoord lichaamsbeeld verhelpen. \r\n\r\nDaarna gaan we met elkaar aan de slag en probeer ik je verder te helpen met tips en oefeningen, die laagdrempelig zijn, wat vooral prettig is voor jou.   \r\n Er zal in het proces veel humor en gezelligheid zijn. We gaan niet te ingewikkeld doen, maar onze mouwen opstropen en aan het werk met jouw grootste uitdagingen is de missie waaraan wij gaan werken.   \r\n\r\nJe bent van harte welkom. Ik help je heel graag. Wil je eerst nog meer informatie; bel of mail me gerust. We plannen in ieder geval een intakegesprek in om te zien of wij graag met elkaar verder willen. Daarna kun je je eventueel in overleg met je ouder(s)/verzorger(s)-inschrijven voor een behandeling. \r\n\r\n \r\n\r\nHoe meld ik mij aan? \r\n\r\nJe kan op onze website aanmelden via de registratie knop op de hoofdpagina. Bij de registratie moet je aan een aantal voorwaarden voldoen om een intakegesprek te plannen. Na de intakegesprek ben je succesvol aangemeld. \r\n\r\n \r\n\r\n \r\n\r\nHoe kan ik chatten met mijn hulpverlener? \r\n\r\nNa de login, verschijnt er een chat-icoon die jou de mogelijkheid biedt om met hulpverlener privé te chatten. Of je kan mij altijd mailen naar stevenito@ZMDH.nl \r\n\r\n \r\n\r\nHoe kan ik deelnemen aan de groepschat? \r\n\r\nNa de login verschijnt er een chat-icoon die jou de mogelijkheid biedt om deel te nemen aan een groepschat. ", null, null, false, "https://i.postimg.cc/bNbyP9RF/Steven-Ito-Orthopedagoog.png", "c3c16c3a-68f3-4e6f-af9b-c54dd75e3e0d", "Eetstoornis", false, null });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "OrthopedagogueWebText", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PictureUrl", "SecurityStamp", "Specialty", "TwoFactorEnabled", "UserName" },
                values: new object[] { "25f384cb-001f-4dfb-899a-7f3433895d34", 0, "7da8ac63-f37b-4526-8d83-fe54d19a0165", "Orthopedagogue", null, false, "Marianne", "van Dijk", false, null, null, null, "Even voorstellen \r\n\r\nHallo, ik ben Marianne van Dijk geboren en getogen in Rotterdam. Vanaf kleins af aan vind ik het altijd leuk om kinderen te helpen, ik haal hier namelijk mijn voldoening uit. Verder ben ik een moeder van drie kinderen, waarvan er een dyslexie heeft. Zelf heb ik ook dyslexie. Misschien valt het je op dat dyslexie een onderwerp is wat regelmatig in mijn leven ter sprake komt, hierdoor wil ik graag mijn ervaring delen met kinderen die hulp willen. \r\n\r\n \r\n\r\nMijn studie \r\n\r\nOmdat ik moeite had met lezen liep ik veel achterstand. Ik ben vanaf VMBO Basis begonnen en door de jaren heen ben ik verder gaan studeren. Uiteindelijk heb ik de opleiding orthopedagogiek afgerond aan de Universiteit van Leiden. \r\n\r\n \r\n\r\nNu over jou: Jij hebt misschien dyslexie \r\n\r\nDyslexie hebben is niet prettig. Het maakt lezen, spellen en schrijven veel ingewikkelder, terwijl iemand wel intelligent genoeg is om dat allemaal te begrijpen. Er is pas sprake van dyslexie als er geen andere oorzaken zijn die de leesproblemen kunnen verklaren. \r\n\r\n \r\n\r\nNiet elke kind dat dyslexie heeft, heeft moeite met spelling en lezen. Sommige kinderen hebben vooral problemen met het lezen en anderen met spelling. Zo heb je bijvoorbeeld kinderen die radend lezen, waardoor ze veel fouten maken doordat ze gokken wat er staat. Anderen lezen letter voor letter, waardoor het leestempo heel laag ligt. \r\n\r\n \r\n\r\nWat gaan we doen? \r\n\r\nSamen gaan we uitzoeken of jij dyslexie hebt. Dat doen we door verschillende soorten testen uit te voeren. Bij het uitvoeren van de testen merk ik vanzelf of jij symptomen hebt. Het zijn testen, zoals het hardop lezen van teksten of het uitspreken van klanken en letters. \r\n\r\nWat we vooral merken is dat je moeite hebt met het verschil te horen tussen klanken als: ‘m’, ‘n’, en ‘ng’, of ‘eu’, ‘uu’ en ‘ui’. Of je hebt moeite om op woorden te komen. En zo zijn er nog andere symptomen. \r\n\r\nJe bent van harte welkom en ik wil je graag bij helpen om mee om te gaan. Je kan mij altijd gerust bereiken voor meer informatie of uitleg door mij te bellen of te mailen. We maken eerst een intakegesprek zodat wij zien of we met elkaar verder willen. En uiteraard kun je je eventueel in overleg met je ouder(s)/verzorg(s) inschrijven voor een behandeling. \r\n\r\nHoe meld ik mij aan? \r\n\r\nJe kan op onze website aanmelden via de registratie knop op de hoofdpagina. Bij de registratie moet je aan een aantal voorwaarden voldoen om een intakegesprek te plannen. Na de intakegesprek ben je succesvol aangemeld. \r\n\r\n \r\n\r\n \r\n\r\nHoe kan ik chatten met mijn hulpverlener? \r\n\r\nNa de login, verschijnt er een chat-icoon die jou de mogelijkheid biedt om met hulpverlener privé te chatten. Of je kan mij altijd mailen naar mariannevandijk@ZMDH.nl \r\n\r\n \r\n\r\nHoe kan ik deelnemen aan de groepschat? \r\n\r\nNa de login verschijnt er een chat-icoon die jou de mogelijkheid biedt om deel te nemen aan een groepschat. ", null, null, false, "https://i.postimg.cc/wTSpbR8c/Marianne-Van-Dijk-Orthopedagoog.png", "8dc1273f-5b97-443a-b95b-0586f55ed560", "Dyslexie", false, null });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "OrthopedagogueWebText", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PictureUrl", "SecurityStamp", "Specialty", "TwoFactorEnabled", "UserName" },
                values: new object[] { "75be3d62-7435-4b81-875e-b2dbb221e05b", 0, "f3d5b690-8410-4339-a9c8-1b08fa252105", "Orthopedagogue", null, false, "Johan", "Lo", false, null, null, null, "Even voorstellen \r\n\r\nIk heet Johan Lo, geboren in 1990 in Rotterdam en de jongste van zes kinderen. Ik ben opgegroeid bij de haven door mijn ouders. Ik verdiep me in de laatste trends van jongeren en vind het interessant hoe de huidige generatie van jongeren zicht inzet voor belangrijke maatschappelijke problemen, zoals klimaatverandering. Jongs af aan probeerde ik jongeren te motiveren om dingen te doen waar ze bang voor zijn.  \r\n \r\n\r\nMijn studie   \r\nNa het VWO begon ik met stand up comedy en werkte ik twee jaar lang als een motiverende spreker bij verschillende bedrijven en evenementen. Hierna had ik orthopedagogiek gestudeerd aan de Universiteit van Cambridge. Tijdens mijn studie heb ik mij gespecialiseerd in de behandeling van faalangst.  \r\n \r\n\r\nNu over jou: jij hebt misschien faalangst   \r\nBij jou bestaat het vermoeden dat je faalangst hebt. Veelal gaat dit over het uitvoeren van iets concreets zoals een taak, een examen, een werkopdracht en dergelijke. De gevolgen hiervan is dat je vaker faalervaring ervaart.  \r\n \r\n\r\nWat gaan we doen?   \r\nIk ga met jou uitzoeken of jij faalangst hebt met een paar gesprekken waarbij evt. ook jouw ouder(s)/verzorger(s) betrokken worden. Mocht het blijken dat je faalangst hebt kijken we samen naar de problemen die je ervaart en hoe we dat stap voor stap kunnen oplossen. Met therapie kan ik je helpen om sterker te staan in je schoenen en een positiever beeld te creëren over jezelf. \r\nAllereerst luister ik graag aandachtig naar je. Ik ben namelijk een actieve luisteraar en beantwoord graag vragen die je hebt. Daarna ga ik een gesprek met je aan om te kijken waarom je faalangst ervaart. We gaan ook je faalangst confronteren met positieve denkpatronen. Wil je eerst nog meer informatie; bel, app of mail me gerust. We plannen in ieder geval een intakegesprek om elkaar beter te leren kennen. Daarna kun je je evt.-in overleg met je ouder(s)/verzorger(s)-inschrijven voor een behandeling. \r\n\r\n \r\n\r\nHoe meld ik mij aan? \r\n\r\nJe kan op onze website aanmelden via de registratie knop op de hoofdpagina. Bij de registratie moet je aan een aantal voorwaarden voldoen om een intakegesprek te plannen. Na de intakegesprek ben je succesvol aangemeld. \r\n\r\n \r\n\r\n \r\n\r\nHoe kan ik chatten met mijn hulpverlener? \r\n\r\nNa de login, verschijnt er een chat-icoon die jou de mogelijkheid biedt om met hulpverlener privé te chatten. Of je kan mij altijd mailen naar johanlo@ZMDH.nl \r\n\r\n \r\n\r\nHoe kan ik deelnemen aan de groepschat? \r\n\r\nNa de login verschijnt er een chat-icoon die jou de mogelijkheid biedt om deel te nemen aan een groepschat. ", null, null, false, "https://i.postimg.cc/9fwqH7rm/Johan-Lo-Orthopedagoog.png", "552b9a20-6561-4fde-9d02-dde4cc9d16fe", "Faalangst", false, null });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "OrthopedagogueWebText", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PictureUrl", "SecurityStamp", "Specialty", "TwoFactorEnabled", "UserName" },
                values: new object[] { "9d48c026-243c-45b9-bbe8-087f543d803e", 0, "ee306dfb-e73b-492f-8be1-c72189087e44", "Orthopedagogue", null, false, "Karin", "Kemper", false, null, null, null, "Even voorstellen									 \r\nIk heet Karin Kemper, geboren in 1972 in Almere en ben het enige kind.  Mijn vader is Argentijns en mijn moeder is Nederlands. Ik ben heel nieuwsgierig naar mensen met ADHD; naar wat hun motiveert, wat hun concentratievermogen is en hoe ik ze persoonlijk kan helpen. Toen ik jong was begon ik al met hulp aanbieden bij kinderen met ADHD. \r\n \r\n\r\nMijn studie   \r\nNa het behalen van mijn gymnasium ging ik werken in de Albert Heijn om geld te verdienen voor mijn opleiding. Hierna had ik orthopedagogiek gestudeerd aan de Universiteit van Leiden. Tijdens mijn studie heb ik mij gespecialiseerd in de behandeling van ADHD en had een bijbaantje als bezorger. \r\n \r\n\r\nNu over jou: jij hebt misschien ADHD \r\nBij jou bestaat het vermoeden dat je ADHD hebt. Als je ADHD hebt, heb je moeite om je aandacht bij iets te houden en dat je te druk bent. Bij ADHD word je afgeleid bij alle prikkels die bij jou binnenkomen en dat je je dan ook druk gedraagt. De gevolgen hiervan zijn dat je de informatie niet goed onthoudt en je misschien andere mensen lastigvalt zonder dat je, het door hebt. \r\n\r\n \r\n\r\nWat gaan we doen?   \r\nIk ga met jou uitzoeken of jij ADHD hebt met een paar gesprekken waarbij evt. ook jouw ouder(s)/verzorger(s) betrokken worden. Mocht het blijken dat je ADHD hebt kijken we samen naar de problemen die je ervaart en hoe we dat stap voor stap kunnen oplossen.  Ik ga mijn uiterst best doen om je te helpen concentreren en je te kalmeren.  \r\n\r\nAllereerst luister ik graag actief naar je. Dit betekent dat ik tijdens het luisteren ook vragen ga stellen. Dit is om meer inzicht te krijgen in hoe ik jou kan helpen.  \r\n\r\nWil je eerst nog meer informatie; bel, app of mail me gerust. We plannen in ieder geval een intakegesprek om elkaar beter te leren kennen. Daarna kun je je evt.-in overleg met je ouder(s)/verzorger(s)-inschrijven voor een behandeling. \r\n\r\n \r\n\r\nHoe meld ik mij aan? \r\n\r\nJe kan op onze website aanmelden via de registratie knop op de hoofdpagina. Bij de registratie moet je aan een aantal voorwaarden voldoen om een intakegesprek te plannen. Na het intakegesprek ben je succesvol aangemeld. \r\n\r\n \r\n\r\n \r\n\r\nHoe kan ik chatten met mijn hulpverlener? \r\n\r\nNa de login, verschijnt er een chat-icoon die jou de mogelijkheid biedt om met hulpverlener privé te chatten. Of je kan mij altijd mailen naar karinkemper@ZMDH.nl \r\n\r\n \r\n\r\nHoe kan ik deelnemen aan de groepschat? \r\n\r\nNa de login verschijnt er een chat-icoon die jou de mogelijkheid biedt om deel te nemen aan een groepschat. ", null, null, false, "https://i.postimg.cc/tRPnMpWP/Karin-Kemper-Orthopedagoog.png", "371b80f4-b30e-4467-97fd-eddfcdd63cf1", "ADHD", false, null });

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

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Email",
                table: "AspNetUsers",
                column: "Email",
                unique: true);

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
                name: "IX_Messages_ChatCode",
                table: "Messages",
                column: "ChatCode");

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
