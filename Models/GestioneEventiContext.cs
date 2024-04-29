using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace gestioneEventi.Models;

public partial class GestioneEventiContext : DbContext
{
    public GestioneEventiContext()
    {
    }

    public GestioneEventiContext(DbContextOptions<GestioneEventiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Evento> Eventos { get; set; }

    public virtual DbSet<Partecipante> Partecipantes { get; set; }

    public virtual DbSet<Risorsa> Risorsas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=ACADEMY2024-16\\SQLEXPRESS;Database=GestioneEventi;User Id=academy;Password=academy!;MultipleActiveResultSets=true;Encrypt=false;TrustServerCertificate=false");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Evento>(entity =>
        {
            entity.HasKey(e => e.IdEvento).HasName("PK__Evento__C8DC7BDACF464A10");

            entity.ToTable("Evento");

            entity.HasIndex(e => new { e.Nome, e.DataEvento }, "UQ__Evento__7305C9CE63CF81AC").IsUnique();

            entity.Property(e => e.IdEvento).HasColumnName("idEvento");
            entity.Property(e => e.CapMax).HasColumnName("capMax");
            entity.Property(e => e.DataEvento).HasColumnName("dataEvento");
            entity.Property(e => e.Desrizione)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("desrizione");
            entity.Property(e => e.Luogo)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("luogo");
            entity.Property(e => e.Nome)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("nome");
        });

        modelBuilder.Entity<Partecipante>(entity =>
        {
            entity.HasKey(e => e.IdPartecipante).HasName("PK__partecip__CC455B5198863E86");

            entity.ToTable("partecipante");

            entity.HasIndex(e => e.Telefono, "UQ__partecip__2A16D945B022504C").IsUnique();

            entity.Property(e => e.IdPartecipante).HasColumnName("idPartecipante");
            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nome");
            entity.Property(e => e.Telefono)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("telefono");

            entity.HasMany(d => d.EventoRifs).WithMany(p => p.PartecipanteRifs)
                .UsingEntity<Dictionary<string, object>>(
                    "Composto",
                    r => r.HasOne<Evento>().WithMany()
                        .HasForeignKey("EventoRif")
                        .HasConstraintName("FK__composto__evento__4222D4EF"),
                    l => l.HasOne<Partecipante>().WithMany()
                        .HasForeignKey("PartecipanteRif")
                        .HasConstraintName("FK__composto__partec__412EB0B6"),
                    j =>
                    {
                        j.HasKey("PartecipanteRif", "EventoRif").HasName("PK__composto__19ABB89272477BE1");
                        j.ToTable("composto");
                        j.IndexerProperty<int>("PartecipanteRif").HasColumnName("partecipanteRif");
                        j.IndexerProperty<int>("EventoRif").HasColumnName("eventoRif");
                    });
        });

        modelBuilder.Entity<Risorsa>(entity =>
        {
            entity.HasKey(e => e.IdRisorsa).HasName("PK__Risorsa__452CFB5C54A79D31");

            entity.ToTable("Risorsa");

            entity.HasIndex(e => e.Nome, "UQ__Risorsa__6F71C0DC48F6E6E6").IsUnique();

            entity.Property(e => e.IdRisorsa).HasColumnName("idRisorsa");
            entity.Property(e => e.Costo).HasColumnName("costo");
            entity.Property(e => e.EventoRif).HasColumnName("eventoRif");
            entity.Property(e => e.Nome)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("nome");
            entity.Property(e => e.Numero).HasColumnName("numero");
            entity.Property(e => e.Tipo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tipo");

            entity.HasOne(d => d.EventoRifNavigation).WithMany(p => p.Risorsas)
                .HasForeignKey(d => d.EventoRif)
                .HasConstraintName("FK__Risorsa__eventoR__3B75D760");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
