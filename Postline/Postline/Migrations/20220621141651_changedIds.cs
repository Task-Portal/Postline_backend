using Microsoft.EntityFrameworkCore.Migrations;

namespace Postline.Migrations
{
    public partial class changedIds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "831ac314-d9cd-42f1-a487-51c7ec4f32d6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c73c09a0-68e7-4804-8348-6a082fd349bf");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f2986548-848b-45c4-8515-092acdb42104");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Posts",
                newName: "PostId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Categories",
                newName: "CategoryId");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3f69ef89-0dff-4dbe-b032-e2c2896f5577", "a767728d-97be-46e7-b96b-196cf23ecf01", "None", "NONE" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9954280f-6c39-4915-8982-6439e28085a0", "5d659b86-e659-49c3-a340-c2632faeff59", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "87b5ec27-14ca-49f4-8229-dcc5112915ae", "a5445270-8dcb-40b2-9c46-edc9dcbf7b85", "Manager", "MANAGER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3f69ef89-0dff-4dbe-b032-e2c2896f5577");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "87b5ec27-14ca-49f4-8229-dcc5112915ae");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9954280f-6c39-4915-8982-6439e28085a0");

            migrationBuilder.RenameColumn(
                name: "PostId",
                table: "Posts",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Categories",
                newName: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c73c09a0-68e7-4804-8348-6a082fd349bf", "7e889067-61a1-414e-a5ec-058128f125eb", "None", "NONE" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "831ac314-d9cd-42f1-a487-51c7ec4f32d6", "2cf8204d-2435-47d7-8513-6b8eec2e13e9", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f2986548-848b-45c4-8515-092acdb42104", "1ed4ef83-5bbc-40b1-a77a-7e14c417a7c5", "Manager", "MANAGER" });
        }
    }
}
