﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using zorgapp.Models;

namespace zorgapp.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20200111130845_database")]
    partial class database
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("zorgapp.Models.Admin", b =>
                {
                    b.Property<int>("AdminId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Password");

                    b.Property<string>("UserName");

                    b.HasKey("AdminId");

                    b.ToTable("Admins");

                    b.HasData(
                        new { AdminId = -1, Password = "749f09bade8aca755660eeb17792da880218d4fbdc4e25fbec279d7fe9f65d70", UserName = "admin" }
                    );
                });

            modelBuilder.Entity("zorgapp.Models.Appointment", b =>
                {
                    b.Property<int>("AppointmentId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CaseId");

                    b.Property<DateTime>("Date");

                    b.Property<int>("DoctorId");

                    b.Property<string>("Info");

                    b.HasKey("AppointmentId");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("zorgapp.Models.Case", b =>
                {
                    b.Property<int>("DoctorId");

                    b.Property<string>("CaseId");

                    b.Property<string>("CaseInfo");

                    b.Property<string>("CaseName");

                    b.Property<int>("PatientId");

                    b.HasKey("DoctorId", "CaseId");

                    b.ToTable("Cases");
                });

            modelBuilder.Entity("zorgapp.Models.Doctor", b =>
                {
                    b.Property<int>("DoctorId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<List<string>>("LocalId");

                    b.Property<string>("Password");

                    b.Property<string>("PhoneNumber");

                    b.Property<string>("Specialism");

                    b.Property<string>("UserName");

                    b.HasKey("DoctorId");

                    b.ToTable("Doctors");

                    b.HasData(
                        new { DoctorId = -1, Email = "admin@mail.mail", FirstName = "admin", LastName = "admin", Password = "749f09bade8aca755660eeb17792da880218d4fbdc4e25fbec279d7fe9f65d70", PhoneNumber = "12345678", Specialism = "-", UserName = "admin" },
                        new { DoctorId = -2, Email = "admin2@mail.mail", FirstName = "admin2", LastName = "admin2", Password = "af3d131396a3c479f9d31c2b9ef5ff9b4c4d1f222087eb24049311402c856702", PhoneNumber = "12345678", Specialism = "-", UserName = "admin2" },
                        new { DoctorId = -3, Email = "admin3@mail.mail", FirstName = "admin3", LastName = "admin3", Password = "72c535b1171f05c58533f9a031ff6445ed4ae3460063c06816eca3040655b6af", PhoneNumber = "12345678", Specialism = "-", UserName = "admin3" }
                    );
                });

            modelBuilder.Entity("zorgapp.Models.Medicine", b =>
                {
                    b.Property<int>("MedicineId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Amount");

                    b.Property<DateTime>("DateEnd");

                    b.Property<DateTime>("DateStart");

                    b.Property<float>("Mg");

                    b.Property<string>("Name");

                    b.Property<int>("PatientId");

                    b.HasKey("MedicineId");

                    b.ToTable("Medicines");
                });

            modelBuilder.Entity("zorgapp.Models.Message", b =>
                {
                    b.Property<int>("MessageId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<bool>("DoctorToPatient");

                    b.Property<string>("Receiver");

                    b.Property<string>("Sender");

                    b.Property<string>("Subject");

                    b.Property<string>("Text");

                    b.HasKey("MessageId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("zorgapp.Models.Patient", b =>
                {
                    b.Property<int>("PatientId")
                        .ValueGeneratedOnAdd();

                    b.Property<List<int>>("CanSeeMeId");

                    b.Property<int?>("DoctorId");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<List<int>>("ICanSeeId");

                    b.Property<string>("LastName");

                    b.Property<string>("LinkCode");

                    b.Property<int>("LinkUses");

                    b.Property<List<string>>("LocalId");

                    b.Property<string>("Password");

                    b.Property<string>("PhoneNumber");

                    b.Property<string>("UserName");

                    b.HasKey("PatientId");

                    b.HasIndex("DoctorId");

                    b.ToTable("Patients");

                    b.HasData(
                        new { PatientId = -1, Email = "admin@mail.mail", FirstName = "admin", LastName = "admin", LinkUses = 0, Password = "749f09bade8aca755660eeb17792da880218d4fbdc4e25fbec279d7fe9f65d70", PhoneNumber = "12345678", UserName = "admin" },
                        new { PatientId = -2, Email = "adminu@mail.mail", FirstName = "Adminu", LastName = "Adminu", LinkUses = 0, Password = "63d9f3a3580e4f30308f489aab55087c455db7020658363deb062727b7afceb9", PhoneNumber = "12345678", UserName = "Adminu" },
                        new { PatientId = -3, Email = "admin3@mail.mail", FirstName = "Admin3", LastName = "Admin3", LinkUses = 0, Password = "72c535b1171f05c58533f9a031ff6445ed4ae3460063c06816eca3040655b6af", PhoneNumber = "12345678", UserName = "Admin3" }
                    );
                });

            modelBuilder.Entity("zorgapp.Models.PatientsDoctors", b =>
                {
                    b.Property<int>("DoctorId");

                    b.Property<int>("PatientId");

                    b.HasKey("DoctorId", "PatientId");

                    b.HasIndex("PatientId");

                    b.ToTable("PatientsDoctorss");

                    b.HasData(
                        new { DoctorId = -1, PatientId = -1 }
                    );
                });

            modelBuilder.Entity("zorgapp.Models.Patient", b =>
                {
                    b.HasOne("zorgapp.Models.Doctor", "Doctor")
                        .WithMany()
                        .HasForeignKey("DoctorId");
                });

            modelBuilder.Entity("zorgapp.Models.PatientsDoctors", b =>
                {
                    b.HasOne("zorgapp.Models.Doctor", "Doctor")
                        .WithMany("PatientsDoctorss")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("zorgapp.Models.Patient", "Patient")
                        .WithMany("PatientsDoctorss")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
