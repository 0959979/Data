using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace zorgapp.Migrations
{
    public partial class database : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    AdminId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Password = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.AdminId);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    AppointmentId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Date = table.Column<DateTime>(nullable: false),
                    CaseId = table.Column<string>(nullable: true),
                    Info = table.Column<string>(nullable: true),
                    DoctorId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.AppointmentId);
                });

            migrationBuilder.CreateTable(
                name: "Cases",
                columns: table => new
                {
                    CaseId = table.Column<string>(nullable: false),
                    CaseInfo = table.Column<string>(nullable: true),
                    CaseName = table.Column<string>(nullable: true),
                    PatientId = table.Column<int>(nullable: false),
                    DoctorId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cases", x => new { x.DoctorId, x.CaseId });
                });

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    DoctorId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    LocalId = table.Column<List<string>>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Specialism = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.DoctorId);
                });

            migrationBuilder.CreateTable(
                name: "Medicines",
                columns: table => new
                {
                    MedicineId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: true),
                    DateStart = table.Column<DateTime>(nullable: false),
                    DateEnd = table.Column<DateTime>(nullable: false),
                    Amount = table.Column<int>(nullable: false),
                    Mg = table.Column<float>(nullable: false),
                    PatientId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicines", x => x.MedicineId);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    MessageId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Sender = table.Column<string>(nullable: true),
                    Receiver = table.Column<string>(nullable: true),
                    Subject = table.Column<string>(nullable: true),
                    Text = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    DoctorToPatient = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.MessageId);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    PatientId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    LocalId = table.Column<List<string>>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    CanSeeMeId = table.Column<List<int>>(nullable: true),
                    ICanSeeId = table.Column<List<int>>(nullable: true),
                    LinkCode = table.Column<string>(nullable: true),
                    LinkUses = table.Column<int>(nullable: false),
                    DoctorId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.PatientId);
                    table.ForeignKey(
                        name: "FK_Patients_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "DoctorId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PatientsDoctorss",
                columns: table => new
                {
                    DoctorId = table.Column<int>(nullable: false),
                    PatientId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientsDoctorss", x => new { x.DoctorId, x.PatientId });
                    table.ForeignKey(
                        name: "FK_PatientsDoctorss_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "DoctorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatientsDoctorss_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "PatientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "AdminId", "Password", "UserName" },
                values: new object[] { -1, "749f09bade8aca755660eeb17792da880218d4fbdc4e25fbec279d7fe9f65d70", "admin" });

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "DoctorId", "Email", "FirstName", "LastName", "LocalId", "Password", "PhoneNumber", "Specialism", "UserName" },
                values: new object[,]
                {
                    { -1, "admin@mail.mail", "admin", "admin", null, "749f09bade8aca755660eeb17792da880218d4fbdc4e25fbec279d7fe9f65d70", "12345678", "-", "admin" },
                    { -2, "admin2@mail.mail", "admin2", "admin2", null, "af3d131396a3c479f9d31c2b9ef5ff9b4c4d1f222087eb24049311402c856702", "12345678", "-", "admin2" },
                    { -3, "admin3@mail.mail", "admin3", "admin3", null, "72c535b1171f05c58533f9a031ff6445ed4ae3460063c06816eca3040655b6af", "12345678", "-", "admin3" }
                });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "PatientId", "CanSeeMeId", "DoctorId", "Email", "FirstName", "ICanSeeId", "LastName", "LinkCode", "LinkUses", "LocalId", "Password", "PhoneNumber", "UserName" },
                values: new object[,]
                {
                    { -1, null, null, "admin@mail.mail", "admin", null, "admin", null, 0, null, "749f09bade8aca755660eeb17792da880218d4fbdc4e25fbec279d7fe9f65d70", "12345678", "admin" },
                    { -2, null, null, "adminu@mail.mail", "Adminu", null, "Adminu", null, 0, null, "63d9f3a3580e4f30308f489aab55087c455db7020658363deb062727b7afceb9", "12345678", "Adminu" },
                    { -3, null, null, "admin3@mail.mail", "Admin3", null, "Admin3", null, 0, null, "72c535b1171f05c58533f9a031ff6445ed4ae3460063c06816eca3040655b6af", "12345678", "Admin3" }
                });

            migrationBuilder.InsertData(
                table: "PatientsDoctorss",
                columns: new[] { "DoctorId", "PatientId" },
                values: new object[] { -1, -1 });

            migrationBuilder.CreateIndex(
                name: "IX_Patients_DoctorId",
                table: "Patients",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientsDoctorss_PatientId",
                table: "PatientsDoctorss",
                column: "PatientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "Cases");

            migrationBuilder.DropTable(
                name: "Medicines");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "PatientsDoctorss");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Doctors");
        }
    }
}
