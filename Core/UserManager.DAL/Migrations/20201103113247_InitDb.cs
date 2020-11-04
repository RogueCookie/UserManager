using Microsoft.EntityFrameworkCore.Migrations;

namespace UserManager.DAL.Migrations
{
    public partial class InitDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    user_name = table.Column<string>(nullable: true),
                    user_status = table.Column<int>(nullable: false),
                    password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "id", "password", "user_name", "user_status" },
                values: new object[] { 1, "123", "tom@gmail.com", 2 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "id", "password", "user_name", "user_status" },
                values: new object[] { 2, null, "alice@yahoo.com", 3 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "id", "password", "user_name", "user_status" },
                values: new object[] { 3, null, "sam@online.com", 4 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "id", "password", "user_name", "user_status" },
                values: new object[] { 4, null, "val@online.com", 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
