using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ekaterina_anisimova_kt_42_22_April_25.Migrations
{
    public partial class CreateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cd_academic_degree",
                columns: table => new
                {
                    academic_degree_id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор ученой степени")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    c_academic_degree_name = table.Column<string>(type: "varchar", maxLength: 100, nullable: false, comment: "Название ученой степени")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cd_academic_degree_academic_degree_id", x => x.academic_degree_id);
                });

            migrationBuilder.CreateTable(
                name: "cd_discipline",
                columns: table => new
                {
                    discipline_id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор дисциплины")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    c_discipline_name = table.Column<string>(type: "varchar", maxLength: 200, nullable: false, comment: "Название дисциплины")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cd_discipline_discipline_id", x => x.discipline_id);
                });

            migrationBuilder.CreateTable(
                name: "cd_position",
                columns: table => new
                {
                    position_id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор должности")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    c_position_name = table.Column<string>(type: "varchar", maxLength: 100, nullable: false, comment: "Название должности")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cd_position_position_id", x => x.position_id);
                });

            migrationBuilder.CreateTable(
                name: "cd_department",
                columns: table => new
                {
                    department_id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор кафедры")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    c_department_name = table.Column<string>(type: "varchar", maxLength: 200, nullable: false, comment: "Название кафедры"),
                    d_foundation_date = table.Column<DateTime>(type: "timestamp", nullable: false, comment: "Дата основания кафедры"),
                    f_head_of_department_id = table.Column<int>(type: "int4", nullable: false, comment: "Идентификатор заведующего кафедрой")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cd_department_department_id", x => x.department_id);
                });

            migrationBuilder.CreateTable(
                name: "cd_teacher",
                columns: table => new
                {
                    teacher_id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор преподавателя")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    c_teacher_firstname = table.Column<string>(type: "varchar", maxLength: 100, nullable: false, comment: "Имя преподавателя"),
                    c_teacher_lastname = table.Column<string>(type: "varchar", maxLength: 100, nullable: false, comment: "Фамилия преподавателя"),
                    c_teacher_middlename = table.Column<string>(type: "varchar", maxLength: 100, nullable: false, comment: "Отчество преподавателя"),
                    f_department_id = table.Column<int>(type: "int4", nullable: false, comment: "Идентификатор кафедры"),
                    f_academic_degree_id = table.Column<int>(type: "int4", nullable: false, comment: "Идентификатор ученой степени"),
                    f_position_id = table.Column<int>(type: "int4", nullable: false, comment: "Идентификатор должности")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cd_teacher_teacher_id", x => x.teacher_id);
                    table.ForeignKey(
                        name: "fk_f_academic_degree_id",
                        column: x => x.f_academic_degree_id,
                        principalTable: "cd_academic_degree",
                        principalColumn: "academic_degree_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_f_department_id",
                        column: x => x.f_department_id,
                        principalTable: "cd_department",
                        principalColumn: "department_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_f_position_id",
                        column: x => x.f_position_id,
                        principalTable: "cd_position",
                        principalColumn: "position_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "cd_load",
                columns: table => new
                {
                    load_id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор нагрузки")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    f_teacher_id = table.Column<int>(type: "int4", nullable: false, comment: "Идентификатор преподавателя"),
                    f_discipline_id = table.Column<int>(type: "int4", nullable: false, comment: "Идентификатор дисциплины"),
                    n_hours = table.Column<int>(type: "int4", nullable: false, comment: "Количество часов нагрузки")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cd_load_load_id", x => x.load_id);
                    table.ForeignKey(
                        name: "fk_f_discipline_id",
                        column: x => x.f_discipline_id,
                        principalTable: "cd_discipline",
                        principalColumn: "discipline_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_f_teacher_id",
                        column: x => x.f_teacher_id,
                        principalTable: "cd_teacher",
                        principalColumn: "teacher_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "idx_cd_department_fk_f_head_of_department_id",
                table: "cd_department",
                column: "f_head_of_department_id");

            migrationBuilder.CreateIndex(
                name: "IX_cd_department_f_head_of_department_id",
                table: "cd_department",
                column: "f_head_of_department_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_cd_load_fk_f_discipline_id",
                table: "cd_load",
                column: "f_discipline_id");

            migrationBuilder.CreateIndex(
                name: "idx_cd_load_fk_f_teacher_id",
                table: "cd_load",
                column: "f_teacher_id");

            migrationBuilder.CreateIndex(
                name: "idx_cd_teacher_fk_f_academic_degree_id",
                table: "cd_teacher",
                column: "f_academic_degree_id");

            migrationBuilder.CreateIndex(
                name: "idx_cd_teacher_fk_f_department_id",
                table: "cd_teacher",
                column: "f_department_id");

            migrationBuilder.CreateIndex(
                name: "idx_cd_teacher_fk_f_position_id",
                table: "cd_teacher",
                column: "f_position_id");

            migrationBuilder.AddForeignKey(
                name: "fk_f_head_of_department_id",
                table: "cd_department",
                column: "f_head_of_department_id",
                principalTable: "cd_teacher",
                principalColumn: "teacher_id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_f_head_of_department_id",
                table: "cd_department");

            migrationBuilder.DropTable(
                name: "cd_load");

            migrationBuilder.DropTable(
                name: "cd_discipline");

            migrationBuilder.DropTable(
                name: "cd_teacher");

            migrationBuilder.DropTable(
                name: "cd_academic_degree");

            migrationBuilder.DropTable(
                name: "cd_department");

            migrationBuilder.DropTable(
                name: "cd_position");
        }
    }
}
