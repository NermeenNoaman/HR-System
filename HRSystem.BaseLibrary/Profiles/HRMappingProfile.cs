// HR Mapping Profile for Entities and DTOs

using AutoMapper;
using HRSystem.BaseLibrary.Models;
// Import all DTOs from the DTOs folder
using HRSystem.BaseLibrary.DTOs;

namespace HRSystem.BaseLibrary.Profiles // Using the specified Profiles namespace
{
    // This class inherits from AutoMapper's Profile
    public class HRMappingProfile : Profile
    {
        public HRMappingProfile()
        {
            // =========================================================================
            // 1. DTOs for LkpGeneralDataCompanyProfile (Company Data)
            // =========================================================================

            // Read: Convert Entity (DB) to ReadDto (Output to Frontend)
            CreateMap<LkpGeneralDataCompanyProfile, CompanyProfileReadDto>();

            // Create: Convert CreateDto (Input) to Entity (for adding new record)
            CreateMap<CompanyProfileCreateDto, LkpGeneralDataCompanyProfile>();

            // Update: Convert UpdateDto (Input, includes ID) to Entity (for modifying existing record)
            CreateMap<CompanyProfileUpdateDto, LkpGeneralDataCompanyProfile>();


            // =========================================================================
            // 2. DTOs for LkpGeneralDataBranch (Branches)
            // =========================================================================

            // Read Mapping
            CreateMap<LkpGeneralDataBranch, BranchReadDto>();

            // Create Mapping
            CreateMap<BranchCreateDto, LkpGeneralDataBranch>();

            // Update Mapping
            CreateMap<BranchUpdateDto, LkpGeneralDataBranch>();


            // =========================================================================
            // 3. DTOs for LkpHRDepartment (HR Departments)
            // =========================================================================

            // Read Mapping
            CreateMap<LkpHRDepartment, HRDepartmentReadDto>();

            // Create Mapping
            CreateMap<HRDepartmentCreateDto, LkpHRDepartment>();

            // Update Mapping
            CreateMap<HRDepartmentUpdateDto, LkpHRDepartment>();


            // =========================================================================
            // 4. DTOs for TPLUser (Authentication - Keep simple for Security Team)
            // =========================================================================

            // Read Mapping
            CreateMap<TPLUser, UserReadDto>();

            // Registration Mapping (Mapping DTO to Entity)
            CreateMap<UserRegisterDto, TPLUser>();

            // Login Mapping (Mapping DTO to Entity)
            CreateMap<UserLoginDto, TPLUser>();
        }
    }
}
