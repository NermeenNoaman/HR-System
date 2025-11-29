// DTOs for LkpBenefitType Entity

using System.ComponentModel.DataAnnotations;

namespace HRSystem.BaseLibrary.DTOs
{
    // Read DTO (OUTPUT)
    public class BenefitTypeReadDto
    {
        public int BenefitTypeID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }

    // Create DTO (INPUT)
    public class BenefitTypeCreateDto
    {
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(500)]
        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }
    }

    // Update DTO (INPUT)
    public class BenefitTypeUpdateDto
    {
        [Required(ErrorMessage = "Benefit Type ID is required for update.")]
        public int BenefitTypeID { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(500)]
        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }
    }
}