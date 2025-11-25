using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSystem.BaseLibrary.DTOs
{
    public class TPLDisciplinaryActionReadDTO
    {
        public int ActionID { get; set; }
        public int EmployeeID { get; set; }
        public DateTime Date { get; set; }
        public string ActionType { get; set; }
        public string Notes { get; set; }
        public int TakenBy { get; set; }

    }

    public class TPLDisciplinaryActionCreateDTO
    {
        [Required]
        public int EmployeeID { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [StringLength(100)]
        public string ActionType { get; set; } 

        [Required]
        public string Notes { get; set; } 

        [Required]
        public int TakenBy { get; set; }
    }

    public class TPLDisciplinaryActionUpdateDTO
    {

        public DateTime? Date { get; set; }

        [StringLength(100)]
        public string ActionType { get; set; }

        public string Notes { get; set; }

        public int? TakenBy { get; set; }
    }
}
