using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Navigation_Menu_App.Models;

namespace Navigation_Menu_App
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Lecturer> Lecturers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Module> Modules { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Database.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Lecturer>()
                .HasOne(lecturer => lecturer.Admin)
                .WithMany(admin => admin.Lecturers)
                .HasForeignKey(lecturer => lecturer.AdminId)
                .IsRequired();

            modelBuilder.Entity<Lecturer>()
                .HasMany(lecturer => lecturer.Students)
                .WithOne(student => student.Lecturer)
                .HasForeignKey(student => student.LecturerId);

            modelBuilder.Entity<Student>()
                .HasMany(student => student.Modules)
                .WithOne(module => module.Student)
                .HasForeignKey(module => module.StudentId);

            

        }
    }
}
