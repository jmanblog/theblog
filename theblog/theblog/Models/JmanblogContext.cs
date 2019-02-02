using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace theblog.Models
{
    public partial class JmanblogContext : DbContext
    {
        public JmanblogContext()
        {
        }

        public JmanblogContext(DbContextOptions<JmanblogContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Articles> Articles { get; set; }
        public virtual DbSet<History> History { get; set; }
        public virtual DbSet<Topics> Topics { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS; Database=Jmanblog; Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.1-servicing-10028");

            modelBuilder.Entity<Articles>(entity =>
            {
                entity.HasKey(e => e.ArticleId);

                entity.Property(e => e.ArticleId).ValueGeneratedNever();

                entity.Property(e => e.ArticleDate).HasColumnType("datetime");

                entity.Property(e => e.ArticleImage).HasMaxLength(50);

                entity.Property(e => e.ArticleKeywords).HasMaxLength(100);

                entity.Property(e => e.ArticleShort).HasMaxLength(20);

                entity.Property(e => e.ArticleTitle).HasMaxLength(50);

                entity.Property(e => e.ArticleUpdated).HasColumnType("datetime");
            });

            modelBuilder.Entity<History>(entity =>
            {
                entity.HasKey(e => e.LogId);

                entity.Property(e => e.LogId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Topics>(entity =>
            {
                entity.HasKey(e => e.TopicId);

                entity.Property(e => e.TopicId).ValueGeneratedNever();

                entity.Property(e => e.MainTopicName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.SubTopicName).HasMaxLength(50);

                entity.Property(e => e.TopicKeywords)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.Property(e => e.UserId).ValueGeneratedNever();

                entity.Property(e => e.AuthMethod).HasMaxLength(10);

                entity.Property(e => e.IdentityString).HasMaxLength(50);
            });
        }
    }
}
