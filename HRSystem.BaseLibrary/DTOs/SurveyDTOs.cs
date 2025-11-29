// DTOs for TPLSurvey Entity

using System;
using System.ComponentModel.DataAnnotations;

namespace HRSystem.BaseLibrary.DTOs
{
    // Read DTO (OUTPUT)
    public class SurveyReadDto
    {
        public int SurveyID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    // Create DTO (INPUT)
    public class SurveyCreateDto
    {
        [Required(ErrorMessage = "Title is required.")]
        [StringLength(100)]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Created Date is required.")]
        public DateTime CreatedDate { get; set; }
    }

    // Update DTO (INPUT)
    public class SurveyUpdateDto
    {
        [Required(ErrorMessage = "Survey ID is required for update.")]
        public int SurveyID { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(100)]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Created Date is required.")]
        public DateTime CreatedDate { get; set; }
    }
}




