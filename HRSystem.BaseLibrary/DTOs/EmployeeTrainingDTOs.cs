using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSystem.BaseLibrary.DTOs
{
    public class TPLEmployeeTrainingReadDTO
    {
        public int EmployeeID { get; set; }
        public int TrainingID { get; set; }
        public string CompletionStatus { get; set; }
        public int Score { get; set; }

    }
    public class TPLEmployeeTrainingCreateDTO
    {
        [Required]
        public int EmployeeID { get; set; }

        [Required]
        public int TrainingID { get; set; }

        [Required]
        [StringLength(50)]
        public string CompletionStatus { get; set; }

        public int Score { get; set; } = 0;
    }

    public class TPLEmployeeTrainingUpdateDTO
    {

        [StringLength(50)]
        public string CompletionStatus { get; set; } 

        [Range(0, 100)] 
        public int? Score { get; set; }
    }
}
