using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HRSystem.BaseLibrary.Models;

[Table("TPLEmployee")]
[Index("Email", Name = "UQ__Employee__A9D1053436BBDCA7", IsUnique = true)]
public partial class TPLEmployee
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int EmployeeID { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    [Required]
    [StringLength(100)]
    public string Email { get; set; }

    [Required]
    [StringLength(20)]
    public string Phone { get; set; }

    public DateTime HireDate { get; set; }

    public int JobID { get; set; }

    public int DepartmentID { get; set; }

    [Required]
    [StringLength(50)]
    public string EmploymentStatus { get; set; }

    [ForeignKey("DepartmentID")]
    [InverseProperty("TPLEmployees")]
    public virtual LkpHRDepartment Department { get; set; }

    [ForeignKey("JobID")]
    [InverseProperty("TPLEmployees")]
    public virtual TPLJob Job { get; set; }

   

    [InverseProperty("AssignedToNavigation")]
    public virtual ICollection<TPLAssetManagement> TPLAssetManagements { get; set; } = new List<TPLAssetManagement>();

    [InverseProperty("Employee")]
    public virtual ICollection<TPLAttendance> TPLAttendances { get; set; } = new List<TPLAttendance>();




    [InverseProperty("Employee")]
    public virtual ICollection<TPLLeaveBalance> TPLLeaveBalances { get; set; } = new List<TPLLeaveBalance>();

    [InverseProperty("Employee")]
    public virtual ICollection<TPLOffboarding> TPLOffboardings { get; set; } = new List<TPLOffboarding>();

    [InverseProperty("Employee")]
    public virtual ICollection<TPLOnboarding> TPLOnboardings { get; set; } = new List<TPLOnboarding>();

  

    [InverseProperty("employee")]
    public virtual ICollection<TPLPermission> TPLPermissions { get; set; } = new List<TPLPermission>();


    [InverseProperty("Manager")]
    public virtual ICollection<TPLProject> TPLProjects { get; set; } = new List<TPLProject>();

    [InverseProperty("employee")]
    public virtual ICollection<TPLRequest> TPLRequests { get; set; } = new List<TPLRequest>();


    [InverseProperty("Employee")]
    public virtual TPLUser TPLUser { get; set; }
}
