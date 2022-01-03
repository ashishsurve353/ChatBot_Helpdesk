using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserAPI.UserModel
{
    public partial class RegistrationDBContext : DbContext
    {
        public RegistrationDBContext()
        {
        }

        public RegistrationDBContext(DbContextOptions<RegistrationDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<RegistrationFeed> Registrations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\ProjectsV13;Initial Catalog=UserDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<RegistrationFeed>(entity =>
            {
                entity.HasKey(e => e.UserName1);

                entity.ToTable("Username");

                entity.Property(e => e.UserName1)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("UserName");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                //entity.Property(e => e.Key)
                //    .HasMaxLength(500)
                //    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                //entity.Property(e => e.UserId).ValueGeneratedOnAdd();

                entity.Property(e => e.UserPassword)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                //entity.Property(e => e.UserRole)
                //    .HasMaxLength(50)
                //    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
