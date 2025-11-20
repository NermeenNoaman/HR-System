using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HRSystem.BaseLibrary.Models;

public partial class TPLEvaluationCriterion
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CriteriaID { get; set; }

    [Required]
    [StringLength(100)]
    public string CriteriaName { get; set; }

    [StringLength(100)]
    public string Description { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    public decimal Weight { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedDate { get; set; }

    public bool IsDeleted { get; set; }

    [InverseProperty("Criteria")]
    public virtual ICollection<TPLPerformanceEvaluation> TPLPerformanceEvaluations { get; set; } = new List<TPLPerformanceEvaluation>();
}
