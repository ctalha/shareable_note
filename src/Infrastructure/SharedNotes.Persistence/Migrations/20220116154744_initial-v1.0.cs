using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SharedNotes.Persistence.Migrations
{
    public partial class initialv10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "colleges",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    create_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    delete_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    update_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_colleges", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "departments",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    degree = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    create_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    delete_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    update_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FK_PK_college_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_departments", x => x.id);
                    table.ForeignKey(
                        name: "FK_departments_colleges_FK_PK_college_id",
                        column: x => x.FK_PK_college_id,
                        principalTable: "colleges",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "file_documents",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    course_title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    document_title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    full_path = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    path = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    length = table.Column<long>(type: "bigint", nullable: false),
                    extension = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    directory_name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    create_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    delete_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    update_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FK_PK_department_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_file_documents", x => x.id);
                    table.ForeignKey(
                        name: "FK_file_documents_departments_FK_PK_department_id",
                        column: x => x.FK_PK_department_id,
                        principalTable: "departments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_departments_FK_PK_college_id",
                table: "departments",
                column: "FK_PK_college_id");

            migrationBuilder.CreateIndex(
                name: "IX_file_documents_FK_PK_department_id",
                table: "file_documents",
                column: "FK_PK_department_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "file_documents");

            migrationBuilder.DropTable(
                name: "departments");

            migrationBuilder.DropTable(
                name: "colleges");
        }
    }
}
