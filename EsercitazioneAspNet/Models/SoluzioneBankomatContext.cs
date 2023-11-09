using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EsercitazioneAspNet.Models
{
    public partial class SoluzioneBankomatContext : DbContext
    {
        public SoluzioneBankomatContext()
        {
        }

        public SoluzioneBankomatContext(DbContextOptions<SoluzioneBankomatContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Banche> Banches { get; set; } = null!;
        public virtual DbSet<BancheFunzionalitum> BancheFunzionalita { get; set; } = null!;
        public virtual DbSet<ContiCorrente> ContiCorrentes { get; set; } = null!;
        public virtual DbSet<Funzionalitum> Funzionalita { get; set; } = null!;
        public virtual DbSet<Movimenti> Movimentis { get; set; } = null!;
        public virtual DbSet<Utenti> Utentis { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=SoluzioneBankomat;User Id=sa;Password=password123;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Banche>(entity =>
            {
                entity.ToTable("Banche");

                entity.HasIndex(e => e.Nome, "IX_Banche")
                    .IsUnique();

                entity.Property(e => e.Nome)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<BancheFunzionalitum>(entity =>
            {
                entity.ToTable("Banche_Funzionalita");

                entity.HasIndex(e => new { e.IdBanca, e.IdFunzionalita }, "IX_BancheFunzionalita")
                    .IsUnique();

                entity.HasOne(d => d.IdBancaNavigation)
                    .WithMany(p => p.BancheFunzionalita)
                    .HasForeignKey(d => d.IdBanca)
                    .HasConstraintName("FK_Banche_Funzionalita_Banche");

                entity.HasOne(d => d.IdFunzionalitaNavigation)
                    .WithMany(p => p.BancheFunzionalita)
                    .HasForeignKey(d => d.IdFunzionalita)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Banche_Funzionalita_Funzionalita");
            });

            modelBuilder.Entity<ContiCorrente>(entity =>
            {
                entity.ToTable("ContiCorrente");

                entity.Property(e => e.DataUltimaOperazione).HasColumnType("date");

                entity.Property(e => e.DataUltimoVersamento).HasColumnType("date");

                entity.HasOne(d => d.IdUtenteNavigation)
                    .WithMany(p => p.ContiCorrentes)
                    .HasForeignKey(d => d.IdUtente)
                    .HasConstraintName("FK_ContiCorrente_Utenti");
            });

            modelBuilder.Entity<Funzionalitum>(entity =>
            {
                entity.HasIndex(e => e.Nome, "IX_Funzionalita")
                    .IsUnique();

                entity.Property(e => e.Nome)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Movimenti>(entity =>
            {
                entity.ToTable("Movimenti");

                entity.Property(e => e.DataOperazione).HasColumnType("date");

                entity.Property(e => e.Funzionalita)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NomeBanca)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.NomeUtente)
                    .HasMaxLength(10)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Utenti>(entity =>
            {
                entity.ToTable("Utenti");

                entity.HasIndex(e => e.NomeUtente, "IX_Utenti")
                    .IsUnique();

                entity.Property(e => e.NomeUtente)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdBancaNavigation)
                    .WithMany(p => p.Utentis)
                    .HasForeignKey(d => d.IdBanca)
                    .HasConstraintName("FK_Utenti_Banche");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
