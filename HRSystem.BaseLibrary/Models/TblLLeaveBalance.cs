using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HRSystem.BaseLibrary.Models;

[Table("TblLLeaveBalance")]
public partial class TblLLeaveBalance
{
    public int EmployeeId { get; set; }

    public int TotalAccrued { get; set; }

    public int TotalUsed { get; set; }

    public int CurrentBalance { get; set; }

    [Key]
    public int LeaveBalanceID { get; set; }
}
