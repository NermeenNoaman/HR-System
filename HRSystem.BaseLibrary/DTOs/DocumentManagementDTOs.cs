// DTOs for TPLDocumentManagement Entity

using System;
using System.ComponentModel.DataAnnotations;

namespace HRSystem.BaseLibrary.DTOs
{
    // Read DTO (OUTPUT)
    public class DocumentManagementReadDto
    {
        public int DocumentID { get; set; }
        public int EmployeeID { get; set; }
        public string DocumentType { get; set; }
        public DateTime UploadDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string FilePath { get; set; }
    }

    // Create DTO (INPUT)
    public class DocumentManagementCreateDto
    {
        [Required(ErrorMessage = "Employee ID is required.")]
        public int EmployeeID { get; set; }

        [Required(ErrorMessage = "Document Type is required.")]
        [StringLength(100)]
        public string DocumentType { get; set; }

        [Required(ErrorMessage = "Upload Date is required.")]
        public DateTime UploadDate { get; set; }

        [Required(ErrorMessage = "Expiry Date is required.")]
        public DateTime ExpiryDate { get; set; }

        [Required(ErrorMessage = "File Path is required.")]
        [StringLength(200)]
        public string FilePath { get; set; }
    }

    // Update DTO (INPUT)
    public class DocumentManagementUpdateDto
    {
        [Required(ErrorMessage = "Document ID is required for update.")]
        public int DocumentID { get; set; }

        [Required(ErrorMessage = "Employee ID is required.")]
        public int EmployeeID { get; set; }

        [Required(ErrorMessage = "Document Type is required.")]
        [StringLength(100)]
        public string DocumentType { get; set; }

        [Required(ErrorMessage = "Upload Date is required.")]
        public DateTime UploadDate { get; set; }

        [Required(ErrorMessage = "Expiry Date is required.")]
        public DateTime ExpiryDate { get; set; }

        [Required(ErrorMessage = "File Path is required.")]
        [StringLength(200)]
        public string FilePath { get; set; }
    }
}




