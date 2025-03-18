using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RailWayAPI.Models;

public partial class RailWaySystemContext : DbContext
{
    public RailWaySystemContext()
    {
    }

    public RailWaySystemContext(DbContextOptions<RailWaySystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CrossStationRoute> CrossStationRoutes { get; set; }

    public virtual DbSet<Route> Routes { get; set; }

    public virtual DbSet<Station> Stations { get; set; }

    public virtual DbSet<Table> Tables { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<Train> Trains { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=master;Trusted_Connection=True;Database=RailWaySystem;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CrossStationRoute>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("CrossStationRoute");

            entity.HasOne(d => d.IdRouteNavigation).WithMany()
                .HasForeignKey(d => d.IdRoute)
                .HasConstraintName("FK_CrossStationRoute_Route");

            entity.HasOne(d => d.IdStationNavigation).WithMany()
                .HasForeignKey(d => d.IdStation)
                .HasConstraintName("FK_CrossStationRoute_Station");
        });

        modelBuilder.Entity<Route>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Rout");

            entity.ToTable("Route");

            entity.Property(e => e.Title).HasMaxLength(50);
        });

        modelBuilder.Entity<Station>(entity =>
        {
            entity.ToTable("Station");

            entity.Property(e => e.Title).HasMaxLength(50);
        });

        modelBuilder.Entity<Table>(entity =>
        {
            entity.ToTable("Table");

            entity.HasOne(d => d.IdRouteNavigation).WithMany(p => p.Tables)
                .HasForeignKey(d => d.IdRoute)
                .HasConstraintName("FK_Table_Route");

            entity.HasOne(d => d.IdStationNavigation).WithMany(p => p.Tables)
                .HasForeignKey(d => d.IdStation)
                .HasConstraintName("FK_Table_Station");

            entity.HasOne(d => d.IdTrainNavigation).WithMany(p => p.Tables)
                .HasForeignKey(d => d.IdTrain)
                .HasConstraintName("FK_Table_Train");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.ToTable("Ticket");

            entity.Property(e => e.Arrival)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Cost).HasColumnType("decimal(18, 1)");
            entity.Property(e => e.Type).HasMaxLength(50);

            entity.HasOne(d => d.IdRouteNavigation).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.IdRoute)
                .HasConstraintName("FK_Ticket_Route");

            entity.HasOne(d => d.IdTrainNavigation).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.IdTrain)
                .HasConstraintName("FK_Ticket_Train");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("FK_Ticket_User");
        });

        modelBuilder.Entity<Train>(entity =>
        {
            entity.ToTable("Train");

            entity.Property(e => e.TypeTrain).HasMaxLength(50);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Gender).HasMaxLength(50);
            entity.Property(e => e.Lastname).HasMaxLength(50);
            entity.Property(e => e.Login).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.NumberPassport).HasMaxLength(50);
            entity.Property(e => e.NumberPhone).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.Patronymic).HasMaxLength(50);
            entity.Property(e => e.SeriesPassport).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
