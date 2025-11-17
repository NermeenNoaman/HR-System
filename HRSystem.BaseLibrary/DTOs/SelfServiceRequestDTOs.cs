using System;
using System.ComponentModel.DataAnnotations;

namespace HRSystem.BaseLibrary.DTOs
{
    public class SelfServiceRequestReadDto
    {
        public int RequestID { get; set; }
        public int EmployeeID { get; set; }
        public string RequestType { get; set; }
        public DateOnly RequestDate { get; set; }
        public string Status { get; set; }
        public int? ApprovedBy { get; set; }
    }

    public class SelfServiceRequestCreateDto
    {
        [Required(ErrorMessage = "Employee ID is required.")]
        public int EmployeeID { get; set; }

        [Required(ErrorMessage = "Request Type is required.")]
        [StringLength(100)]
        public string RequestType { get; set; }

        [Required(ErrorMessage = "Request Date is required.")]
        public DateOnly RequestDate { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        [StringLength(50)]
        public string Status { get; set; }

        public int? ApprovedBy { get; set; }
    }

    public class SelfServiceRequestUpdateDto
    {
        [Required(ErrorMessage = "Request ID is required.")]
        public int RequestID { get; set; }

        [Required(ErrorMessage = "Employee ID is required.")]
        public int EmployeeID { get; set; }

        [Required(ErrorMessage = "Request Type is required.")]
        [StringLength(100)]
        public string RequestType { get; set; }

        [Required(ErrorMessage = "Request Date is required.")]
        public DateOnly RequestDate { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        [StringLength(50)]
        public string Status { get; set; }

        public int? ApprovedBy { get; set; }
    }
}


