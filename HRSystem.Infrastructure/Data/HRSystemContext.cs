using HRSystem.BaseLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


namespace HRSystem.Infrastructure.Data;

public partial class HRSystemContext : DbContext
{
    public HRSystemContext()
    {
    }

    public HRSystemContext(DbContextOptions<HRSystemContext> options)
        : base(options)
    {
    }
    
    public virtual DbSet<LKPLeaveType> LKPLeaveTypes { get; set; }

    public virtual DbSet<LKPPermissionType> LKPPermissionTypes { get; set; }

    public virtual DbSet<LkpGeneralDataBranch> LkpGeneralDataBranches { get; set; }

    public virtual DbSet<LkpGeneralDataCompanyProfile> LkpGeneralDataCompanyProfiles { get; set; }

    public virtual DbSet<LkpHRDepartment> LkpHRDepartments { get; set; }


    public virtual DbSet<TPLAssetManagement> TPLAssetManagements { get; set; }

    public virtual DbSet<TPLAttendance> TPLAttendances { get; set; }






    public virtual DbSet<TPLEmployee> TPLEmployees { get; set; }




    public virtual DbSet<TPLJob> TPLJobs { get; set; }

    public virtual DbSet<TPLLeave> TPLLeaves { get; set; }

    public virtual DbSet<TPLLeaveBalance> TPLLeaveBalances { get; set; }

    public virtual DbSet<TPLOffboarding> TPLOffboardings { get; set; }

    public virtual DbSet<TPLOnboarding> TPLOnboardings { get; set; }


    public virtual DbSet<TPLPermission> TPLPermissions { get; set; }

    public virtual DbSet<TPLProject> TPLProjects { get; set; }

    public virtual DbSet<TPLProjectAssignment> TPLProject_Assignments { get; set; }


    public virtual DbSet<TPLRequest> TPLRequests { get; set; }




    public virtual DbSet<TPLTraining> TPLTrainings { get; set; }

    public virtual DbSet<TPLUser> TPLUsers { get; set; }

    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LKPLeaveType>(entity =>
        {
            entity.HasKey(e => e.LeaveTypeId).HasName("PK_LeaveTypes");
        });

        modelBuilder.Entity<LKPPermissionType>(entity =>
        {
            entity.Property(e => e.permission_type_id).ValueGeneratedOnAdd();
        });

       

        

        modelBuilder.Entity<LkpGeneralDataBranch>(entity =>
        {
            entity.Property(e => e.IsDeleted).HasAnnotation("Relational:DefaultConstraintName", "DF_LkpBranches_IsDeleted");

            entity.HasOne(d => d.Company).WithMany(p => p.LkpGeneralDataBranches)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LkpGeneralDataBranches_LkpGeneralDataCompanyProfiles");
        });

        modelBuilder.Entity<LkpGeneralDataCompanyProfile>(entity =>
        {
            entity.Property(e => e.IsDeleted).HasAnnotation("Relational:DefaultConstraintName", "DF_LkpGeneralDataCompanyProfiles_IsDeleted");
        });

        modelBuilder.Entity<LkpHRDepartment>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("PK_LkpDepartments");

            entity.Property(e => e.IsDeleted).HasAnnotation("Relational:DefaultConstraintName", "DF_LkpDepartments_IsDeleted");

            entity.HasOne(d => d.Branch).WithMany(p => p.LkpHRDepartments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LkpHRDepartments_LkpGeneralDataBranches");
        });


        modelBuilder.Entity<TPLAssetManagement>(entity =>
        {
            entity.HasKey(e => e.AssetID).HasName("PK__TPLAsset__434923721A60BBB8");

            entity.Property(e => e.AssetID).ValueGeneratedOnAdd();

            entity.HasOne(d => d.AssignedToNavigation).WithMany(p => p.TPLAssetManagements)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TPLAssetManagement_TPLEmployee");
        });

        modelBuilder.Entity<TPLAttendance>(entity =>
        {
            entity.HasKey(e => e.AttendanceID).HasName("PK__TPLAtten__8B69263C191476DC");

            entity.Property(e => e.AttendanceID).ValueGeneratedOnAdd();

            entity.Property(e => e.CheckInLatitude).HasColumnType("decimal(10, 8)");

            entity.Property(e => e.CheckInLongitude).HasColumnType("decimal(10, 8)");

            entity.HasOne(d => d.Employee).WithMany(p => p.TPLAttendances)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TPLAttendance_TPLEmployee");
        });

       

        modelBuilder.Entity<TPLEmployee>(entity =>
        {
            entity.HasKey(e => e.EmployeeID).HasName("PK__Employee__7AD04FF148254588");

            entity.Property(e => e.EmployeeID).ValueGeneratedOnAdd();

            entity.HasOne(d => d.Department).WithMany(p => p.TPLEmployees)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TPLEmployee_LkpHRDepartments");

            entity.HasOne(d => d.Job).WithMany(p => p.TPLEmployees)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TPLEmployee_Job");
        });

       

        modelBuilder.Entity<TPLJob>(entity =>
        {
            entity.HasKey(e => e.JobID).HasName("PK__TPLJob__056690E20F0CC5B9");

            entity.Property(e => e.JobID).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<TPLLeave>(entity =>
        {
            entity.HasKey(e => e.LeaveID).HasName("PK__Leave__796DB97974021ABD");

            entity.HasOne(d => d.request).WithOne(p => p.TPLLeave)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TPLLeave_TPLRequests");
        });

        modelBuilder.Entity<TPLLeaveBalance>(entity =>
        {
            entity.Property(e => e.BalanceId).ValueGeneratedOnAdd();

            entity.HasOne(d => d.Employee).WithMany(p => p.TPLLeaveBalances)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TPLLeaveBalance_TPLEmployee");
        });

        modelBuilder.Entity<TPLOffboarding>(entity =>
        {
            entity.HasKey(e => e.ExitID).HasName("PK__TPLOffbo__26D64E9808277847");

            entity.Property(e => e.ExitID).ValueGeneratedOnAdd();

            entity.HasOne(d => d.Employee).WithMany(p => p.TPLOffboardings)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TPLOffboarding_TPLEmployee");
        });

        modelBuilder.Entity<TPLOnboarding>(entity =>
        {
            entity.HasKey(e => e.OnboardingID).HasName("PK__Onboardi__43F2371E2B631C75");

            entity.Property(e => e.OnboardingID).ValueGeneratedOnAdd();

            entity.HasOne(d => d.Employee).WithMany(p => p.TPLOnboardings)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TPLOnboarding_TPLEmployee");
        });

       

        modelBuilder.Entity<TPLPermission>(entity =>
        {
            entity.Property(e => e.permission_id).ValueGeneratedOnAdd();

            entity.HasOne(d => d.employee).WithMany(p => p.TPLPermissions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TPLPermissions_TPLEmployee");

            entity.HasOne(d => d.permission_type).WithMany(p => p.TPLPermissions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TPLPermissions_LKPPermissionTypes");
        });

        modelBuilder.Entity<TPLProject>(entity =>
        {
            entity.HasKey(e => e.ProjectID).HasName("PK__TPLProje__761ABED0A98D3175");

            entity.Property(e => e.ProjectID).ValueGeneratedOnAdd();

            entity.HasOne(d => d.Manager).WithMany(p => p.TPLProjects).HasConstraintName("FK_TPLProject_Manager");
        });



        modelBuilder.Entity<TPLRequest>(entity =>
        {
            entity.Property(e => e.request_id).ValueGeneratedOnAdd();

            entity.HasOne(d => d.employee).WithMany(p => p.TPLRequests)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TPLRequests_TPLEmployee");

            entity.HasOne(d => d.leave_type).WithMany(p => p.TPLRequests)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TPLRequests_LKPLeaveTypes");
        });



        modelBuilder.Entity<TPLTraining>(entity =>
        {
            entity.HasKey(e => e.TrainingID).HasName("PK__TPLTrain__E8D71DE221E8C971");

            entity.Property(e => e.TrainingID).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<TPLUser>(entity =>
        {
            entity.HasKey(e => e.UserID).HasName("PK__User__1788CCAC2EB55247");

            entity.Property(e => e.UserID).ValueGeneratedOnAdd();

            entity.HasOne(d => d.Employee).WithOne(p => p.TPLUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TPLUser_TPLEmployee");
        });
        

        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RefreshT__3214EC07E7828848");
            entity.HasOne(r => r.User)
                .WithMany(u => u.RefreshTokens)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });


    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

