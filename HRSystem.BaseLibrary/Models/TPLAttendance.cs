// File: TPLAttendance.cs

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HRSystem.BaseLibrary.Models;

/// Represents an attendance record for an employee, logging check-in/out times.
[Table("TPLAttendance")]
public partial class TPLAttendance
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int AttendanceID { get; set; }

    public int EmployeeID { get; set; }

    /// The date of the attendance record (Time component is ignored).
    public DateTime Date { get; set; }

    /// The time of employee check-in (Time-only format).
    [Column(TypeName = "time(7)")]
    public TimeSpan? CheckIn { get; set; }

    /// The time of employee check-out (Time-only format). Null if active.
    [Column(TypeName = "time(7)")]
    public TimeSpan? CheckOut { get; set; }

    /// Status of the attendance record (e.g., Present).
    [Required]
    [StringLength(50)]
    public string Status { get; set; }

    [ForeignKey("EmployeeID")]
    [InverseProperty("TPLAttendances")]
    public virtual TPLEmployee Employee { get; set; }
}
