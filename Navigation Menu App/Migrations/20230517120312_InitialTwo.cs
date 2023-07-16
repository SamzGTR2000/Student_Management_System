using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Navigation_Menu_App.Migrations
{
    /// <inheritdoc />
    public partial class InitialTwo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lecturers_Admins_AdminId",
                table: "Lecturers");

            migrationBuilder.DropForeignKey(
                name: "FK_Modules_Students_StudentId",
                table: "Modules");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Lecturers_LecturerId",
                table: "Students");

            migrationBuilder.AlterColumn<int>(
                name: "LecturerId",
                table: "Students",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "Modules",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AdminId",
                table: "Lecturers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Lecturers_Admins_AdminId",
                table: "Lecturers",
                column: "AdminId",
                principalTable: "Admins",
                principalColumn: "AdminId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Modules_Students_StudentId",
                table: "Modules",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Lecturers_LecturerId",
                table: "Students",
                column: "LecturerId",
                principalTable: "Lecturers",
                principalColumn: "LecturerId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lecturers_Admins_AdminId",
                table: "Lecturers");

            migrationBuilder.DropForeignKey(
                name: "FK_Modules_Students_StudentId",
                table: "Modules");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Lecturers_LecturerId",
                table: "Students");

            migrationBuilder.AlterColumn<int>(
                name: "LecturerId",
                table: "Students",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "Modules",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "AdminId",
                table: "Lecturers",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Lecturers_Admins_AdminId",
                table: "Lecturers",
                column: "AdminId",
                principalTable: "Admins",
                principalColumn: "AdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_Modules_Students_StudentId",
                table: "Modules",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Lecturers_LecturerId",
                table: "Students",
                column: "LecturerId",
                principalTable: "Lecturers",
                principalColumn: "LecturerId");
        }
    }
}
