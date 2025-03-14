using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ProgettoPoliziaMunicipale.Models;

namespace ProgettoPoliziaMunicipale.Data;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Anagrafica> Anagrafiche { get; set; }

    public virtual DbSet<TipoViolazione> TipoViolazioni { get; set; }

    public virtual DbSet<Verbale> Verbali { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAPTOP-IKTLQEHC\\SQLEXPRESS;Database=ProgettoSettimanale;User Id=sa;Password=sa;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Anagrafica>(entity =>
        {
            entity.HasKey(e => e.Idanagrafica).HasName("PK__Anagrafi__7AB1023CA7D25C68");
        });

        modelBuilder.Entity<TipoViolazione>(entity =>
        {
            entity.HasKey(e => e.Idviolazione).HasName("PK__TipoViol__AF77BD925674E0D5");
        });

        modelBuilder.Entity<Verbale>(entity =>
        {
            entity.HasKey(e => e.Idverbale).HasName("PK__Verbale__073D2A45CF0AF481");

            entity.HasOne(d => d.IdanagraficaNavigation).WithMany(p => p.Verbali).HasConstraintName("FK_Verbale_Anagrafica");

            entity.HasOne(d => d.IdviolazioneNavigation).WithMany(p => p.Verbali).HasConstraintName("FK_Verbale_TipoViolazione");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
