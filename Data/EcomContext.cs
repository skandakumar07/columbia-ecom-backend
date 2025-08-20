using System;
using System.Collections.Generic;
using EcommerceTrail.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceTrail.Data;

public partial class EcomContext : DbContext
{
    public EcomContext()
    {
    }

    public EcomContext(DbContextOptions<EcomContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AddressBook> AddressBooks { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=tcp:practiceproject1.database.windows.net,1433;Database=ProjectEcom;User ID=Skanda;Password=Sk@Columbia;Encrypt=True;TrustServerCertificate=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AddressBook>(entity =>
        {
            entity.HasKey(e => e.AddressId).HasName("PK__AddressB__091C2A1B380A457C");

            entity.ToTable("AddressBook");

            entity.Property(e => e.AddressId).HasColumnName("AddressID");
            entity.Property(e => e.AddressLine).HasMaxLength(255);
            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.Country).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.State).HasMaxLength(100);
            entity.Property(e => e.UsersId).HasColumnName("UsersID");
            entity.Property(e => e.ZipCode).HasMaxLength(20);

            entity.HasOne(d => d.Users).WithMany(p => p.AddressBooks)
                .HasForeignKey(d => d.UsersId)
                .HasConstraintName("FK__AddressBo__Users__6383C8BA");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC279187CECE");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D10534A168E7BA").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.Typeofuser)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasDefaultValue("user")
                .HasColumnName("typeofuser");
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
