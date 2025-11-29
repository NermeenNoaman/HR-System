// DTOs for TPLCVBank Entity

using System;
using System.ComponentModel.DataAnnotations;

namespace HRSystem.BaseLibrary.DTOs
{
    // Read DTO (OUTPUT)
    public class CVBankReadDto
    {
        public int CV_ID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string CV_File { get; set; }
        public DateTime AddedDate { get; set; }
        public string Notes { get; set; }
    }

    // Create DTO (INPUT)
    public class CVBankCreateDto
    {
        [Required(ErrorMessage = "Full Name is required.")]
        [StringLength(100)]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [StringLength(100)]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone Number is required.")]
        [StringLength(100)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "CV File is required.")]
        [StringLength(255)]
        public string CV_File { get; set; }

        [Required(ErrorMessage = "Added Date is required.")]
        public DateTime AddedDate { get; set; }

        [Required(ErrorMessage = "Notes is required.")]
        [StringLength(200)]
        public string Notes { get; set; }
    }

    // Update DTO (INPUT)
    public class CVBankUpdateDto
    {
        [Required(ErrorMessage = "CV ID is required for update.")]
        public int CV_ID { get; set; }

        [Required(ErrorMessage = "Full Name is required.")]
        [StringLength(100)]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [StringLength(100)]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone Number is required.")]
        [StringLength(100)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "CV File is required.")]
        [StringLength(255)]
        public string CV_File { get; set; }

        [Required(ErrorMessage = "Added Date is required.")]
        public DateTime AddedDate { get; set; }

        [Required(ErrorMessage = "Notes is required.")]
        [StringLength(200)]
        public string Notes { get; set; }
    }
}




