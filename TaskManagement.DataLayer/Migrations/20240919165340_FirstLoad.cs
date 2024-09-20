using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagement.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class FirstLoad : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppTask",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 6000, nullable: false),
                    TaskStatus = table.Column<int>(type: "int", nullable: false),
                    CreatedByUserId = table.Column<int>(type: "int", nullable: false),
                    AssignedToUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppTask", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppTask_AppUser_AssignedToUserId",
                        column: x => x.AssignedToUserId,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);

                    //table.ForeignKey(
                    //    name: "FK_AppTask_AppUser_CreatedByUserId",
                    //    column: x => x.CreatedByUserId,
                    //    principalTable: "AppUser",
                    //    principalColumn: "Id",
                    //    onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppTask_AssignedToUserId",
                table: "AppTask",
                column: "AssignedToUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppTask_CreatedByUserId",
                table: "AppTask",
                column: "CreatedByUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppTask");

            migrationBuilder.DropTable(
                name: "AppUser");
        }
    }
}
