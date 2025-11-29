// DTOs for TPLRecruitmentPortal Entity

using System;
using System.ComponentModel.DataAnnotations;

namespace HRSystem.BaseLibrary.DTOs
{
    // Read DTO (OUTPUT)
    public class RecruitmentPortalReadDto
    {
        public int PortalID { get; set; }
        public int HRNeedID { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string Notes { get; set; }
    }

    // Create DTO (INPUT)
    public class RecruitmentPortalCreateDto
    {
        [Required(ErrorMessage = "HR Need ID is required.")]
        public int HRNeedID { get; set; }

        [Required(ErrorMessage = "Publish Date is required.")]
        public DateTime PublishDate { get; set; }

        [Required(ErrorMessage = "Expiry Date is required.")]
        public DateTime ExpiryDate { get; set; }

        [StringLength(200)]
        public string Notes { get; set; }
    }

    // Update DTO (INPUT)
    public class RecruitmentPortalUpdateDto
    {
        [Required(ErrorMessage = "Portal ID is required for update.")]
        public int PortalID { get; set; }

        [Required(ErrorMessage = "HR Need ID is required.")]
        public int HRNeedID { get; set; }

        [Required(ErrorMessage = "Publish Date is required.")]
        public DateTime PublishDate { get; set; }

        [Required(ErrorMessage = "Expiry Date is required.")]
        public DateTime ExpiryDate { get; set; }

        [StringLength(200)]
        public string Notes { get; set; }
    }
}




