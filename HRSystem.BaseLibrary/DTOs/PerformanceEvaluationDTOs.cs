// DTOs for TPLPerformanceEvaluation Entity

using System;
using System.ComponentModel.DataAnnotations;

namespace HRSystem.BaseLibrary.DTOs
{
    // Read DTO (OUTPUT)
    public class PerformanceEvaluationReadDto
    {
        public int EvaluationID { get; set; }
        public int EmployeeID { get; set; }
        public DateTime Date { get; set; }
        public int Score { get; set; }
        public string Comments { get; set; }
        public int CriteriaID { get; set; }
    }

    // Create DTO (INPUT)
    public class PerformanceEvaluationCreateDto
    {
        [Required(ErrorMessage = "Employee ID is required.")]
        public int EmployeeID { get; set; }

        [Required(ErrorMessage = "Date is required.")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Score is required.")]
        public int Score { get; set; }

        [Required(ErrorMessage = "Comments is required.")]
        public string Comments { get; set; }

        [Required(ErrorMessage = "Criteria ID is required.")]
        public int CriteriaID { get; set; }
    }

    // Update DTO (INPUT)
    public class PerformanceEvaluationUpdateDto
    {
        [Required(ErrorMessage = "Evaluation ID is required for update.")]
        public int EvaluationID { get; set; }

        [Required(ErrorMessage = "Employee ID is required.")]
        public int EmployeeID { get; set; }

        [Required(ErrorMessage = "Date is required.")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Score is required.")]
        public int Score { get; set; }

        [Required(ErrorMessage = "Comments is required.")]
        public string Comments { get; set; }

        [Required(ErrorMessage = "Criteria ID is required.")]
        public int CriteriaID { get; set; }
    }
}




