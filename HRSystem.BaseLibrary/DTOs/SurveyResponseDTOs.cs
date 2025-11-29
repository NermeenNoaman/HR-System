// DTOs for TPLSurvey_Response Entity

using System.ComponentModel.DataAnnotations;

namespace HRSystem.BaseLibrary.DTOs
{
    // Read DTO (OUTPUT)
    public class SurveyResponseReadDto
    {
        public int ResponseID { get; set; }
        public int SurveyID { get; set; }
        public int EmployeeID { get; set; }
        public string ResponseText { get; set; }
        public int Rating { get; set; }
    }

    // Create DTO (INPUT)
    public class SurveyResponseCreateDto
    {
        [Required(ErrorMessage = "Survey ID is required.")]
        public int SurveyID { get; set; }

        [Required(ErrorMessage = "Employee ID is required.")]
        public int EmployeeID { get; set; }

        public string ResponseText { get; set; }

        [Required(ErrorMessage = "Rating is required.")]
        public int Rating { get; set; }
    }

    // Update DTO (INPUT)
    public class SurveyResponseUpdateDto
    {
        [Required(ErrorMessage = "Response ID is required for update.")]
        public int ResponseID { get; set; }

        [Required(ErrorMessage = "Survey ID is required.")]
        public int SurveyID { get; set; }

        [Required(ErrorMessage = "Employee ID is required.")]
        public int EmployeeID { get; set; }

        public string ResponseText { get; set; }

        [Required(ErrorMessage = "Rating is required.")]
        public int Rating { get; set; }
    }
}




