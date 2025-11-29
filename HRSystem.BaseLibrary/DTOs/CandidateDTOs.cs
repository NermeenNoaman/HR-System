// DTOs for TPLCandidate Entity

using System.ComponentModel.DataAnnotations;

namespace HRSystem.BaseLibrary.DTOs
{
    // Read DTO (OUTPUT)
    public class CandidateReadDto
    {
        public int CandidateID { get; set; }
        public string Status { get; set; }
        public int JobApplicationId { get; set; }
    }

    // Create DTO (INPUT)
    public class CandidateCreateDto
    {
        [Required(ErrorMessage = "Status is required.")]
        [StringLength(50)]
        public string Status { get; set; }

        [Required(ErrorMessage = "Job Application ID is required.")]
        public int JobApplicationId { get; set; }
    }

    // Update DTO (INPUT)
    public class CandidateUpdateDto
    {
        [Required(ErrorMessage = "Candidate ID is required for update.")]
        public int CandidateID { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        [StringLength(50)]
        public string Status { get; set; }

        [Required(ErrorMessage = "Job Application ID is required.")]
        public int JobApplicationId { get; set; }
    }
}




