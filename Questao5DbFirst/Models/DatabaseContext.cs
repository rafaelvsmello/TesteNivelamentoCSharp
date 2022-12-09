using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Questao5DbFirst.Models;

public partial class DatabaseContext : DbContext
{
    public DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Contacorrente> Contacorrentes { get; set; }

    public virtual DbSet<Idempotencium> Idempotencia { get; set; }

    public virtual DbSet<Movimento> Movimentos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite("Filename=database.sqlite");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contacorrente>(entity =>
        {
            entity.HasKey(e => e.Idcontacorrente);

            entity.ToTable("contacorrente");

            entity.HasIndex(e => e.Numero, "IX_contacorrente_numero").IsUnique();

            entity.Property(e => e.Idcontacorrente)
                .HasColumnType("TEXT(37)")
                .HasColumnName("idcontacorrente");
            entity.Property(e => e.Ativo)
                .HasColumnType("INTEGER(1)")
                .HasColumnName("ativo");
            entity.Property(e => e.Nome)
                .HasColumnType("TEXT(100)")
                .HasColumnName("nome");
            entity.Property(e => e.Numero)
                .HasColumnType("INTEGER(10)")
                .HasColumnName("numero");
        });

        modelBuilder.Entity<Idempotencium>(entity =>
        {
            entity.HasKey(e => e.ChaveIdempotencia);

            entity.ToTable("idempotencia");

            entity.Property(e => e.ChaveIdempotencia)
                .HasColumnType("TEXT(37)")
                .HasColumnName("chave_idempotencia");
            entity.Property(e => e.Requisicao)
                .HasColumnType("TEXT(1000)")
                .HasColumnName("requisicao");
            entity.Property(e => e.Resultado)
                .HasColumnType("TEXT(1000)")
                .HasColumnName("resultado");
        });

        modelBuilder.Entity<Movimento>(entity =>
        {
            entity.HasKey(e => e.Idmovimento);

            entity.ToTable("movimento");

            entity.Property(e => e.Idmovimento)
                .HasColumnType("TEXT (37)")
                .HasColumnName("idmovimento");
            entity.Property(e => e.Datamovimento)
                .HasColumnType("DATETIME (25)")
                .HasColumnName("datamovimento");
            entity.Property(e => e.Idcontacorrente)
                .HasColumnType("TEXT (37)")
                .HasColumnName("idcontacorrente");
            entity.Property(e => e.Tipomovimento)
                .HasColumnType("TEXT (1)")
                .HasColumnName("tipomovimento");
            entity.Property(e => e.Valor).HasColumnName("valor");

            entity.HasOne(d => d.IdcontacorrenteNavigation).WithMany(p => p.Movimentos)
                .HasForeignKey(d => d.Idcontacorrente)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
