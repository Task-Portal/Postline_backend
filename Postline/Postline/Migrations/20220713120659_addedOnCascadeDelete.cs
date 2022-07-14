using Microsoft.EntityFrameworkCore.Migrations;

namespace Postline.Migrations
{
    public partial class addedOnCascadeDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_AspNetUsers_UserId1",
                table: "Posts");

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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f9673b36-7c3f-45c9-88f7-c559d31e4a5a", "1e233912-1e61-4436-8518-752f0a8136a1", "None", "NONE" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9591e37b-aa1c-427e-8cdd-ad75931ec5b0", "1f124cf2-7e64-4594-bdb7-daa877da18eb", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a39e6e51-6251-4b58-ba44-d355912c7190", "4468f0fd-ef06-40c2-ad54-411bff5d728f", "Manager", "MANAGER" });

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_AspNetUsers_UserId1",
                table: "Posts",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_AspNetUsers_UserId1",
                table: "Posts");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9591e37b-aa1c-427e-8cdd-ad75931ec5b0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a39e6e51-6251-4b58-ba44-d355912c7190");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f9673b36-7c3f-45c9-88f7-c559d31e4a5a");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_AspNetUsers_UserId1",
                table: "Posts",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
