using Microsoft.EntityFrameworkCore.Migrations;

namespace eCommerceStarterCode.Migrations
{
    public partial class addProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "107fb5a5-e431-4576-a5d3-204be9ee34a5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d5804d1f-c8ea-41a3-8264-4449558174e7");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9a34ed3c-77d0-47c5-84a8-8cf1d9dc3d2f", "eb521450-af23-47e1-8ea3-3f55fc9e5303", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "427282a9-a332-4c11-a830-7b0bc2bc1e8d", "c3e77e7e-66ab-4f4e-a7a4-8d9eb208d656", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "427282a9-a332-4c11-a830-7b0bc2bc1e8d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9a34ed3c-77d0-47c5-84a8-8cf1d9dc3d2f");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "107fb5a5-e431-4576-a5d3-204be9ee34a5", "d7b015c4-a67e-4fa4-8021-b9ac8422db01", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d5804d1f-c8ea-41a3-8264-4449558174e7", "0bf68bf7-ebbf-455a-b021-70c35471afe0", "Admin", "ADMIN" });
        }
    }
}
