using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WDPR_A.Migrations
{
    public partial class _2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "006c921b-611f-4cf9-a18c-3d722840ee42",
                column: "ConcurrencyStamp",
                value: "c31c586f-1cd5-44f6-89ec-210b2cb3c74c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "26d89e3f-e47f-468b-bd78-6f58aef3285e",
                column: "ConcurrencyStamp",
                value: "6fa74624-e9e8-477c-b4ef-efc4da937569");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6e7321be-b891-44a3-abd8-71283880ddb9",
                column: "ConcurrencyStamp",
                value: "17903d7b-dd37-45ed-9e43-43ce52ea06b6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ede8d0e5-2581-476c-ba45-a99d7089844a",
                column: "ConcurrencyStamp",
                value: "02c2ed0e-473a-47f0-90ba-76fef32e7b41");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "13198681-ef68-4acf-bd7e-544e12fed291",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "5e6d6571-b830-4eda-b68f-59a65620854c", "74f2cd7d-02fa-4da3-a780-bfcacfeee24f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1988e216-9179-42a1-8243-2b6bf362b1b4",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "09f502c8-7154-43d3-bdc6-a72e3ead2e2e", "8baff5ea-1af4-4960-b734-50a3ee8a9d27" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4e3371ca-b20a-4c91-b6c2-7c872c310a54",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "7eb8e1c4-146d-4723-9e24-914b49865ee0", "41c0f138-c195-4c87-a38c-0b949bc28074" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7d028f6c-929e-45b0-8493-573078b85f79",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "cd66a1fb-7e09-4cd8-9426-9cc2a867c3e4", "758bfc3a-95e6-4f68-ad16-3d5a78de6858" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "006c921b-611f-4cf9-a18c-3d722840ee42",
                column: "ConcurrencyStamp",
                value: "5b7900fb-9151-4c77-a5a8-d74b9c366287");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "26d89e3f-e47f-468b-bd78-6f58aef3285e",
                column: "ConcurrencyStamp",
                value: "1cc60221-e6f8-4bf2-acb5-ce762ddcd2e6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6e7321be-b891-44a3-abd8-71283880ddb9",
                column: "ConcurrencyStamp",
                value: "1c850c91-d8a5-4968-b4f5-a01e10f8cb31");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ede8d0e5-2581-476c-ba45-a99d7089844a",
                column: "ConcurrencyStamp",
                value: "2855e5f4-235c-4a4c-ab63-3af2d8c2b6db");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "13198681-ef68-4acf-bd7e-544e12fed291",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "2b9678b2-d653-4a41-ad6d-9baea79ccdf5", "ce8e0f34-0651-4e7d-9bca-8a8fd3b9f2a4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1988e216-9179-42a1-8243-2b6bf362b1b4",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "5a7e536e-95ba-4731-9a3e-72901de75f20", "9c0d3284-0768-4b98-9652-e15a21d613d3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4e3371ca-b20a-4c91-b6c2-7c872c310a54",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "d198e904-b533-48c6-bc38-bb735d9a9c46", "ba62bffc-7065-4fc4-b3e4-2b466710bfea" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7d028f6c-929e-45b0-8493-573078b85f79",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "ced03aed-2fc8-4f17-9b13-04d08d4d7bf9", "142eb5ba-268d-4b1d-af12-46d65da80eab" });
        }
    }
}
