using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SaDick.Models;

namespace SaDick.Context;

public partial class SadickContext : DbContext
{
    public SadickContext()
    {
    }

    public SadickContext(DbContextOptions<SadickContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Administrator> Administrators { get; set; }

    public virtual DbSet<Child> Children { get; set; }

    public virtual DbSet<ChildGroup> ChildGroups { get; set; }

    public virtual DbSet<Educator> Educators { get; set; }

    public virtual DbSet<LoginAdmin> LoginAdmins { get; set; }

    public virtual DbSet<LoginEducator> LoginEducators { get; set; }

    public virtual DbSet<LoginParent> LoginParents { get; set; }

    public virtual DbSet<Parent> Parents { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Visiting> Visitings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=WIN-HJUHREA82LG\\MY_SERVER;Database=Sadick;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Administrator>(entity =>
        {
            entity.HasKey(e => e.AdminPhoneNum).HasName("PK__Administ__F47CB04533FEB258");

            entity.ToTable("Administrator");

            entity.Property(e => e.AdminPhoneNum)
                .ValueGeneratedNever()
                .HasColumnName("adminPhoneNum_");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .HasColumnName("name_");
            entity.Property(e => e.Surname)
                .HasMaxLength(20)
                .HasColumnName("surname_");
        });

        modelBuilder.Entity<Child>(entity =>
        {
            entity.HasKey(e => e.BirthSertificateSerNum).HasName("PK__Child__B3243F8B9037DCBD");

            entity.ToTable("Child");

            entity.Property(e => e.BirthSertificateSerNum)
                .HasMaxLength(12)
                .HasColumnName("birthSertificateSerNum_");
            entity.Property(e => e.GroupNum).HasColumnName("groupNum_");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .HasColumnName("name_");
            entity.Property(e => e.ParentPhoneNum).HasColumnName("parentPhoneNum_");
            entity.Property(e => e.Surname)
                .HasMaxLength(20)
                .HasColumnName("surname_");

            entity.HasOne(d => d.GroupNumNavigation).WithMany(p => p.Children)
                .HasForeignKey(d => d.GroupNum)
                .HasConstraintName("FK__Child__groupNum___4D94879B");

            entity.HasOne(d => d.ParentPhoneNumNavigation).WithMany(p => p.Children)
                .HasForeignKey(d => d.ParentPhoneNum)
                .HasConstraintName("FK__Child__parentPho__4E88ABD4");
        });

        modelBuilder.Entity<ChildGroup>(entity =>
        {
            entity.HasKey(e => e.GroupNum).HasName("PK__ChildGro__47168B5E0D244CE2");

            entity.ToTable("ChildGroup");

            entity.Property(e => e.GroupNum)
                .ValueGeneratedNever()
                .HasColumnName("groupNum_");
            entity.Property(e => e.NumOfChild).HasColumnName("numOfChild_");
        });

        modelBuilder.Entity<Educator>(entity =>
        {
            entity.HasKey(e => e.EducatorPhoneNum).HasName("PK__Educator__BFEE4964F5B4D46F");

            entity.ToTable("Educator");

            entity.Property(e => e.EducatorPhoneNum)
                .ValueGeneratedNever()
                .HasColumnName("educatorPhoneNum_");
            entity.Property(e => e.GroupNum).HasColumnName("groupNum_");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .HasColumnName("name_");
            entity.Property(e => e.Surname)
                .HasMaxLength(20)
                .HasColumnName("surname_");

            entity.HasOne(d => d.GroupNumNavigation).WithMany(p => p.Educators)
                .HasForeignKey(d => d.GroupNum)
                .HasConstraintName("FK__Educator__groupN__571DF1D5");
        });

        modelBuilder.Entity<LoginAdmin>(entity =>
        {
            entity.HasKey(e => e.AdminPhoneNum).HasName("PK__LoginAdm__F47CB0456154F028");

            entity.ToTable("LoginAdmin");

            entity.Property(e => e.AdminPhoneNum)
                .ValueGeneratedNever()
                .HasColumnName("adminPhoneNum_");
            entity.Property(e => e.Password)
                .HasMaxLength(12)
                .HasColumnName("password_");

            entity.HasOne(d => d.AdminPhoneNumNavigation).WithOne(p => p.LoginAdmin)
                .HasForeignKey<LoginAdmin>(d => d.AdminPhoneNum)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LoginAdmi__admin__619B8048");
        });

        modelBuilder.Entity<LoginEducator>(entity =>
        {
            entity.HasKey(e => e.EducatorPhoneNum).HasName("PK__LoginEdu__BFEE4964B2226430");

            entity.ToTable("LoginEducator");

            entity.Property(e => e.EducatorPhoneNum)
                .ValueGeneratedNever()
                .HasColumnName("educatorPhoneNum_");
            entity.Property(e => e.Password)
                .HasMaxLength(12)
                .HasColumnName("password_");

            entity.HasOne(d => d.EducatorPhoneNumNavigation).WithOne(p => p.LoginEducator)
                .HasForeignKey<LoginEducator>(d => d.EducatorPhoneNum)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LoginEduc__educa__5EBF139D");
        });

        modelBuilder.Entity<LoginParent>(entity =>
        {
            entity.HasKey(e => e.ParentPhoneNum).HasName("PK__LoginPar__AFAE94116AACEE54");

            entity.ToTable("LoginParent");

            entity.Property(e => e.ParentPhoneNum)
                .ValueGeneratedNever()
                .HasColumnName("parentPhoneNum_");
            entity.Property(e => e.Password)
                .HasMaxLength(12)
                .HasColumnName("password_");

            entity.HasOne(d => d.ParentPhoneNumNavigation).WithOne(p => p.LoginParent)
                .HasForeignKey<LoginParent>(d => d.ParentPhoneNum)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LoginPare__paren__5BE2A6F2");
        });

        modelBuilder.Entity<Parent>(entity =>
        {
            entity.HasKey(e => e.ParentPhoneNum).HasName("PK__Parent__AFAE94115AC1E616");

            entity.ToTable("Parent");

            entity.Property(e => e.ParentPhoneNum)
                .ValueGeneratedNever()
                .HasColumnName("parentPhoneNum_");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .HasColumnName("name_");
            entity.Property(e => e.Surname)
                .HasMaxLength(20)
                .HasColumnName("surname_");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.BirthSertificateSerNum).HasName("PK__Payment__B3243F8BAAA2F6DA");

            entity.ToTable("Payment");

            entity.Property(e => e.BirthSertificateSerNum)
                .HasMaxLength(12)
                .HasColumnName("birthSertificateSerNum_");
            entity.Property(e => e.MonthNotPayed).HasColumnName("monthNotPayed_");

            entity.HasOne(d => d.BirthSertificateSerNumNavigation).WithOne(p => p.Payment)
                .HasForeignKey<Payment>(d => d.BirthSertificateSerNum)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Payment__birthSe__5165187F");
        });

        modelBuilder.Entity<Visiting>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Visiting__DC501A11356CB882");

            entity.ToTable("Visiting");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id_");
            entity.Property(e => e.BirthSertificateSerNum)
                .HasMaxLength(12)
                .HasColumnName("birthSertificateSerNum_");
            entity.Property(e => e.Data)
                .HasColumnType("date")
                .HasColumnName("data_");

            entity.HasOne(d => d.BirthSertificateSerNumNavigation).WithMany(p => p.Visitings)
                .HasForeignKey(d => d.BirthSertificateSerNum)
                .HasConstraintName("FK__Visiting__birthS__5441852A");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
