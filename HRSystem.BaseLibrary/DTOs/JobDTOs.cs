// DTOs for TPLJob Entity

using System;
using System.ComponentModel.DataAnnotations;

namespace HRSystem.BaseLibrary.DTOs
{
    // Read DTO (OUTPUT)
    public class JobReadDto
    {
        public int JobID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int DepartmentID { get; set; }
        public DateTime PostedDate { get; set; }
        public string Status { get; set; }
    }

    // Create DTO (INPUT)
    public class JobCreateDto
    {
        [Required(ErrorMessage = "Title is required.")]
        [StringLength(100)]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Department ID is required.")]
        public int DepartmentID { get; set; }

        [Required(ErrorMessage = "Posted Date is required.")]
        public DateTime PostedDate { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        [StringLength(50)]
        public string Status { get; set; }
    }

    // Update DTO (INPUT)
    public class JobUpdateDto
    {
        [Required(ErrorMessage = "Job ID is required for update.")]
        public int JobID { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(100)]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Department ID is required.")]
        public int DepartmentID { get; set; }

        [Required(ErrorMessage = "Posted Date is required.")]
        public DateTime PostedDate { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        [StringLength(50)]
        public string Status { get; set; }
    }
}




