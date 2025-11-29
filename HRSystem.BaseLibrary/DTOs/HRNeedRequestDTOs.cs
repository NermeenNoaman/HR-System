// DTOs for TPLHRNeedRequest Entity

using System;
using System.ComponentModel.DataAnnotations;

namespace HRSystem.BaseLibrary.DTOs
{
    // Read DTO (OUTPUT)
    public class HRNeedRequestReadDto
    {
        public int HRNeedID { get; set; }
        public int DepartmentId { get; set; }
        public string Title { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    // Create DTO (INPUT)
    public class HRNeedRequestCreateDto
    {
        [Required(ErrorMessage = "Department ID is required.")]
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(50)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        public int Quantity { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        [StringLength(20)]
        public string Status { get; set; }

        [Required(ErrorMessage = "Created Date is required.")]
        public DateTime CreatedDate { get; set; }
    }

    // Update DTO (INPUT)
    public class HRNeedRequestUpdateDto
    {
        [Required(ErrorMessage = "HR Need ID is required for update.")]
        public int HRNeedID { get; set; }

        [Required(ErrorMessage = "Department ID is required.")]
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(50)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        public int Quantity { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        [StringLength(20)]
        public string Status { get; set; }

        [Required(ErrorMessage = "Created Date is required.")]
        public DateTime CreatedDate { get; set; }
    }
}




