// DTOs for TPLEvaluationCriterion Entity

using System.ComponentModel.DataAnnotations;

namespace HRSystem.BaseLibrary.DTOs
{
    // Read DTO (OUTPUT)
    public class EvaluationCriteriaReadDto
    {
        public int CriteriaID { get; set; }
        public string CriteriaName { get; set; }
        public string Description { get; set; }
        public decimal Weight { get; set; }
    }

    // Create DTO (INPUT)
    public class EvaluationCriteriaCreateDto
    {
        [Required(ErrorMessage = "Criteria Name is required.")]
        [StringLength(100)]
        public string CriteriaName { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Weight is required.")]
        public decimal Weight { get; set; }
    }

    // Update DTO (INPUT)
    public class EvaluationCriteriaUpdateDto
    {
        [Required(ErrorMessage = "Criteria ID is required for update.")]
        public int CriteriaID { get; set; }

        [Required(ErrorMessage = "Criteria Name is required.")]
        [StringLength(100)]
        public string CriteriaName { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Weight is required.")]
        public decimal Weight { get; set; }
    }
}




