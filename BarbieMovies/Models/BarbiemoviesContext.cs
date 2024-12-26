using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace BarbieMovies.Models;

public partial class BarbiemoviesContext : DbContext
{
    public BarbiemoviesContext()
    {
    }

    public BarbiemoviesContext(DbContextOptions<BarbiemoviesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categories> Categories { get; set; }

    public virtual DbSet<Movies> Movies { get; set; }

    public virtual DbSet<Usuarios> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;user=root;database=barbiemovies;password=root", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.40-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Categories>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("categories");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Movies>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("movies");

            entity.HasIndex(e => e.CategoryId, "category_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.Director)
                .HasMaxLength(100)
                .HasColumnName("director");
            entity.Property(e => e.DurationMinutes).HasColumnName("duration_minutes");
            entity.Property(e => e.ReleaseYear).HasColumnName("release_year");
            entity.Property(e => e.Title)
                .HasMaxLength(150)
                .HasColumnName("title");

            entity.HasOne(d => d.Category).WithMany(p => p.Movies)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("movies_ibfk_1");
        });

        modelBuilder.Entity<Usuarios>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("usuarios");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(45)
                .HasColumnName("nombre");
            entity.Property(e => e.Password)
                .HasMaxLength(45)
                .HasColumnName("password");
            entity.Property(e => e.Rol)
                .HasMaxLength(45)
                .HasColumnName("rol");
            entity.Property(e => e.Usuario)
                .HasMaxLength(45)
                .HasColumnName("usuario");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
