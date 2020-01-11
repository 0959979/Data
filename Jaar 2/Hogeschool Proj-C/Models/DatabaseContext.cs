using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace zorgapp.Models
{
    public class DatabaseContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // creates a many to many relation between doctors and patients by doing two one to many relations
            modelBuilder.Entity<PatientsDoctors>()
            .HasKey(x => new { x.DoctorId, x.PatientId });
            modelBuilder.Entity<PatientsDoctors>()
            .HasOne(pd => pd.Doctor)
            .WithMany(d => d.PatientsDoctorss)
            .HasForeignKey(pd => pd.DoctorId);
            modelBuilder.Entity<PatientsDoctors>()
            .HasOne(pd => pd.Patient)
            .WithMany (p => p.PatientsDoctorss)
            .HasForeignKey (pd => pd.PatientId);

            //cases
            modelBuilder.Entity<Case>()
            .HasKey(x => new { x.DoctorId, x.CaseId });

            //make sure the necessary accounts are present
            modelBuilder.Entity<Doctor>()
            .HasData(new Doctor()
            {
                DoctorId = -1,
                FirstName = "admin",
                LastName = "admin",
                Email = "admin@mail.mail",
                PhoneNumber = "12345678",
                Specialism = "-",
                UserName = "admin",
                Password = Program.Hash256bits("admin" + "password")
            },
            new Doctor()
            {
                DoctorId = -2,
                FirstName = "admin2",
                LastName = "admin2",
                Email = "admin2@mail.mail",
                PhoneNumber = "12345678",
                Specialism = "-",
                UserName = "admin2",
                Password = Program.Hash256bits("admin2" + "password")
            },
            new Doctor()
            {
                DoctorId = -3,
                FirstName = "admin3",
                LastName = "admin3",
                Email = "admin3@mail.mail",
                PhoneNumber = "12345678",
                Specialism = "-",
                UserName = "admin3",
                Password = Program.Hash256bits("admin3" + "password")
            }
            );

            modelBuilder.Entity<Patient>()
            .HasData(new Patient()
            {
                PatientId = -1,
                FirstName = "admin",
                LastName = "admin",
                Email = "admin@mail.mail",
                PhoneNumber = "12345678",
                UserName = "admin",
                Password = Program.Hash256bits("admin" + "password"),
                LinkCode = null,
                LinkUses = 0

            },
            new Patient()
            {
                PatientId = -2,
                FirstName = "Adminu",
                LastName = "Adminu",
                Email = "adminu@mail.mail",
                PhoneNumber = "12345678",
                UserName = "Adminu",
                Password = Program.Hash256bits("adminu" + "password"),
                LinkCode = null,
                LinkUses = 0
            },
            new Patient()
            {
                PatientId = -3,
                FirstName = "Admin3",
                LastName = "Admin3",
                Email = "admin3@mail.mail",
                PhoneNumber = "12345678",
                UserName = "Admin3",
                Password = Program.Hash256bits("admin3" + "password"),
                LinkCode = null,
                LinkUses = 0
            }
            );

            modelBuilder.Entity<Admin>()
            .HasData(new Admin()
            {
                AdminId = -1,
                UserName = "admin",
                Password = Program.Hash256bits("adminpassword")
            }
            );

            //link between patient 1 and doctor 1
            modelBuilder.Entity<PatientsDoctors>()
            .HasData(new PatientsDoctors()
            {
                PatientId = -1,
                DoctorId = -1
            }
            );

            //https://www.learnentityframeworkcore.com/configuration/many-to-many-relationship-configuration

        }
        // creates the tables in the database
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<Case> Cases { get; set; }
        public DbSet<PatientsDoctors> PatientsDoctorss { get; set; }

    }

}

