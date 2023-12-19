using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SalesManagement2.Model;

public partial class SalesContext : DbContext
{
    public SalesContext()
    {
        Database.EnsureCreated();
    }

    public SalesContext(DbContextOptions<SalesContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    public virtual DbSet<MDivision> MDivisions { get; set; }

    public virtual DbSet<MMessage> MMessages { get; set; }

    public virtual DbSet<MPosition> MPositions { get; set; }

    public virtual DbSet<MStaff> MStaffs { get; set; }

    public virtual DbSet<MStore> MStores { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(System.Configuration.ConfigurationManager.ConnectionStrings["SalesContext"].ConnectionString);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MDivision>(entity =>
        {
            entity.HasKey(e => e.MDivisionId).HasName("PK__tmp_ms_x__4596EAAF5EF4CAE2");

            entity.ToTable("M_Division");

            entity.Property(e => e.MDivisionId).HasColumnName("M_DivisionID");
            entity.Property(e => e.Comments)
                .HasMaxLength(80)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.DivisionName)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.DspFlg).HasColumnName("DspFLG");
        });

        modelBuilder.Entity<MMessage>(entity =>
        {
            entity.HasKey(e => e.MsgId).HasName("PK__M_Messag__662358921AD33F80");

            entity.ToTable("M_Message");

            entity.Property(e => e.MsgId)
                .HasMaxLength(6)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("MsgID");
            entity.Property(e => e.MsgComments).UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.MsgTitle).UseCollation("SQL_Latin1_General_CP1_CI_AS");
        });

        modelBuilder.Entity<MPosition>(entity =>
        {
            entity.HasKey(e => e.MPositionId).HasName("PK__tmp_ms_x__0DC4147426861A97");

            entity.ToTable("M_Position");

            entity.Property(e => e.MPositionId).HasColumnName("M_PositionID");
            entity.Property(e => e.Comments)
                .HasMaxLength(80)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.DspFlg).HasColumnName("DspFLG");
            entity.Property(e => e.PositionName)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
        });

        modelBuilder.Entity<MStaff>(entity =>
        {
            entity.HasKey(e => e.StaffCd).HasName("PK__M_Staff__96D4DAB093011EF0");

            entity.ToTable("M_Staff");

            entity.Property(e => e.StaffCd)
                .HasMaxLength(5)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("StaffCD");
            entity.Property(e => e.DivisionId).HasColumnName("DivisionID");
            entity.Property(e => e.PositionId).HasColumnName("PositionID");
            entity.Property(e => e.StaffAddress)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.StaffAddressKana)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.StaffBirthday).HasColumnType("date");
            entity.Property(e => e.StaffComments)
                .HasMaxLength(80)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.StaffJoinDate).HasColumnType("date");
            entity.Property(e => e.StaffMail)
                .HasMaxLength(30)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.StaffMobileTel)
                .HasMaxLength(12)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.StaffName)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.StaffNameKana)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.StaffPassword)
                .HasMaxLength(20)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.StaffPostal)
                .HasMaxLength(7)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.StaffTel)
                .HasMaxLength(12)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.StaffUserId)
                .HasMaxLength(20)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("StaffUserID");
            entity.Property(e => e.StoreCd)
                .HasMaxLength(5)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("StoreCD");

            entity.HasOne(d => d.Division).WithMany(p => p.MStaffs)
                .HasForeignKey(d => d.DivisionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_M_Staff_To_M_Division");

            entity.HasOne(d => d.Position).WithMany(p => p.MStaffs)
                .HasForeignKey(d => d.PositionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_M_Staff_To_M_Position");

            entity.HasOne(d => d.StoreCdNavigation).WithMany(p => p.MStaffs)
                .HasForeignKey(d => d.StoreCd)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_M_Staff_To_M_Store");
        });

        modelBuilder.Entity<MStore>(entity =>
        {
            entity.HasKey(e => e.StoreCd).HasName("PK__M_Store__3B84A7883F057647");

            entity.ToTable("M_Store");

            entity.Property(e => e.StoreCd)
                .HasMaxLength(5)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("StoreCD");
            entity.Property(e => e.StoreAddress)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.StoreAddressKana)
                .HasMaxLength(60)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.StoreComments)
                .HasMaxLength(80)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.StoreFax)
                .HasMaxLength(12)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.StoreMail)
                .HasMaxLength(30)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.StoreName)
                .HasMaxLength(30)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.StoreNameKana)
                .HasMaxLength(50)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.StorePostal)
                .HasMaxLength(7)
                .IsFixedLength()
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
            entity.Property(e => e.StoreTel)
                .HasMaxLength(12)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
