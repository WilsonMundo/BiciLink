
using Domain.ModelContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public partial class SystemDBContext : DbContext
    {
        public SystemDBContext(DbContextOptions<SystemDBContext> options) : base(options)
        {

        }
        public virtual DbSet<Bicicletum> Bicicleta { get; set; }

        public virtual DbSet<Estacion> Estacions { get; set; }

        public virtual DbSet<EstadoBicicletum> EstadoBicicleta { get; set; }

        public virtual DbSet<EstadoMantenimiento> EstadoMantenimientos { get; set; }

        public virtual DbSet<EstadoReserva> EstadoReservas { get; set; }

        public virtual DbSet<Mantenimiento> Mantenimientos { get; set; }

        public virtual DbSet<Penalizacion> Penalizacions { get; set; }

        public virtual DbSet<Reserva> Reservas { get; set; }

        public virtual DbSet<Soporte> Soportes { get; set; }

        public virtual DbSet<TipoSoporte> TipoSoportes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bicicletum>(entity =>
            {
                entity.HasKey(e => e.BicicletaId).HasName("PK_Bicicleta_1");

                entity.Property(e => e.Caracteristica)                    
                    .IsUnicode(false);
                entity.Property(e => e.Descripcion)
                    .HasMaxLength(550)
                    .IsUnicode(false);
                entity.Property(e => e.Imagen)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.Imagen1)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.EstadoBicicleta).WithMany(p => p.Bicicleta)
                    .HasForeignKey(d => d.EstadoBicicletaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Bicicleta_EstadoBicicleta");
            });

            modelBuilder.Entity<Estacion>(entity =>
            {
                entity.ToTable("Estacion");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(250)
                    .IsUnicode(false);
                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Latitud)
                    .HasColumnType("decimal(18,14)");

                entity.Property(e => e.Longitud)
                      .HasColumnType("decimal(18,14)");
            });

            modelBuilder.Entity<EstadoBicicletum>(entity =>
            {
                entity.HasKey(e => e.EstadoBicicletaId);

                entity.Property(e => e.EstadoBicicletaId).HasColumnName("EstadoBicicletaID");
                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EstadoMantenimiento>(entity =>
            {
                entity.ToTable("EstadoMantenimiento");

                entity.Property(e => e.EstadoMantenimientoId).ValueGeneratedNever();
                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EstadoReserva>(entity =>
            {
                entity.HasKey(e => e.EstadoReservaId).HasName("PK_EstadoReserva_1");

                entity.ToTable("EstadoReserva");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Mantenimiento>(entity =>
            {
                entity.ToTable("Mantenimiento");

                entity.Property(e => e.BtFecha)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime")
                    .HasColumnName("btFecha");
                entity.Property(e => e.Descripcion)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.FechaFin).HasColumnType("datetime");
                entity.Property(e => e.Observacion)
                    .HasMaxLength(350)
                    .IsUnicode(false);

                entity.HasOne(d => d.Bicicleta).WithMany(p => p.Mantenimientos)
                    .HasForeignKey(d => d.BicicletaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Mantenimiento_Bicicleta");
            });

            modelBuilder.Entity<Penalizacion>(entity =>
            {
                entity.ToTable("Penalizacion");
            });

            modelBuilder.Entity<Reserva>(entity =>
            {
                entity.ToTable("Reserva");

                entity.Property(e => e.ReservaId).HasColumnName("ReservaID");
                entity.Property(e => e.BtFecha)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime")
                    .HasColumnName("btFecha");
                entity.Property(e => e.CodigoDesbloqueo)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.FechaReserva).HasColumnType("datetime");
                
                entity.HasOne(d => d.Bicicleta).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.BicicletaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reserva_Bicicleta");

                entity.HasOne(d => d.EstacionDestino).WithMany(p => p.ReservaEstacionDestinos)
                    .HasForeignKey(d => d.EstacionDestinoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Reserva_Estacion");

                entity.HasOne(d => d.EstacionOrigen).WithMany(p => p.ReservaEstacionOrigens)
                    .HasForeignKey(d => d.EstacionOrigenId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Reserva_Estacion1");

                entity.HasOne(d => d.EstadoReserva).WithMany(p => p.Reservas)
                    .HasForeignKey(d => d.EstadoReservaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Reserva_EstadoReserva");
            });

            modelBuilder.Entity<Soporte>(entity =>
            {
                entity.ToTable("Soporte");

                entity.Property(e => e.BtFecha)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime")
                    .HasColumnName("btFecha");
                entity.Property(e => e.Descripcion)
                    .HasMaxLength(150)
                    .IsUnicode(false);
                entity.Property(e => e.FechaCierre).HasColumnType("datetime");
                entity.Property(e => e.Observacion)
                    .HasMaxLength(350)
                    .IsUnicode(false);

                entity.HasOne(d => d.Reserva).WithMany(p => p.Soportes)
                    .HasForeignKey(d => d.ReservaId)
                    .HasConstraintName("FK_Soporte_Reserva");

                entity.HasOne(d => d.TipoSoporte).WithMany(p => p.Soportes)
                    .HasForeignKey(d => d.TipoSoporteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Soporte_TipoSoporte");
            });

            modelBuilder.Entity<TipoSoporte>(entity =>
            {
                entity.ToTable("TipoSoporte");

                entity.Property(e => e.TipoSoporteId).ValueGeneratedNever();
                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
            OnModelCreatingPartial(modelBuilder);

        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
