using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EmailService.Models;

public partial class PostgresContext : DbContext
{
    public PostgresContext()
    {
    }

    public PostgresContext(DbContextOptions<PostgresContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Aviso> Avisos { get; set; }

    public virtual DbSet<Configuracaoemail> Configuracaoemails { get; set; }

    public virtual DbSet<Produto> Produtos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=postgres;Username=postgres;Password=123123");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Aviso>(entity =>
        {
            entity.HasKey(e => new { e.Avdata, e.Avcodigoproduto }).HasName("aviso_pkey");

            entity.ToTable("aviso");

            entity.HasIndex(e => e.Avcodigoproduto, "aviso_fkindex1");

            entity.Property(e => e.Avdata)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("avdata");
            entity.Property(e => e.Avcodigoproduto)
                .HasMaxLength(25)
                .HasColumnName("avcodigoproduto");
        });

        modelBuilder.Entity<Configuracaoemail>(entity =>
        {
            entity.HasKey(e => e.Ceemail).HasName("configuracaoemail_pkey");

            entity.ToTable("configuracaoemail");

            entity.Property(e => e.Ceemail)
                .HasMaxLength(50)
                .HasColumnName("ceemail");
            entity.Property(e => e.Cenome)
                .HasMaxLength(100)
                .HasColumnName("cenome");
        });

        modelBuilder.Entity<Produto>(entity =>
        {
            entity.HasKey(e => e.Procodigo).HasName("produto_pkey");

            entity.ToTable("produto");

            entity.Property(e => e.Procodigo)
                .HasMaxLength(25)
                .HasColumnName("procodigo");
            entity.Property(e => e.Proavisaressup).HasColumnName("proavisaressup");
            entity.Property(e => e.Pronome)
                .HasMaxLength(100)
                .HasColumnName("pronome");
            entity.Property(e => e.Proqtdavisa).HasColumnName("proqtdavisa");
            entity.Property(e => e.Proqtdestoque).HasColumnName("proqtdestoque");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
