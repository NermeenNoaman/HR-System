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
            CreateMap<TPLUser, UserReadDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserID))
                .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.EmployeeID));

            // Registration Mapping - We exclude Password and ConfirmPassword
            // Password hashing will be done in the controller
            CreateMap<UserRegisterDto, TPLUser>()
                .ForMember(dest => dest.UserID, opt => opt.Ignore())
                .ForMember(dest => dest.EmployeeID, opt => opt.MapFrom(src => src.EmployeeId))
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore()); // Set manually in controller

            // Login Mapping is not needed - we use the DTO directly for validation
            // UserLoginDto ->> Manual validation in controller

            // =========================================================================
            // 5. DTOs for TPLRequests (Leave Requests)
            // =========================================================================

            // Read Mapping: Entity to ReadDto (Output) - Including JOIN
            CreateMap<TPLRequest, RequestReadDto>()
                // Mapping the LeaveTypeName property using the LeaveType navigation property
                .ForMember(dest => dest.LeaveTypeName,
                           opt => opt.MapFrom(src => src.leave_type.Name));

            // Create Mapping: CreateDto to Entity (Input)
            CreateMap<LeaveRequestCreateDto, TPLRequest>()
                // Ignore properties that are calculated by the system or added later
                .ForMember(dest => dest.number_of_days, opt => opt.Ignore());

            // Update Mapping: UpdateDto to Entity (Input)
            CreateMap<LeaveRequestUpdateDto, TPLRequest>()
                // Ignore properties that are calculated by the system or added later
                .ForMember(dest => dest.number_of_days, opt => opt.Ignore());


            // =========================================================================
            // 6. DTOs for TPLLeaveBalance (Employee Leave Balances)
            // =========================================================================
            // Read Mapping: Entity to ReadDto (Output) - Including JOIN
            CreateMap<TPLLeaveBalance, LeaveBalanceReadDto>()
                // Mapping Calculation: AvailableBalance = AllocatedDays - UsedDays
                .ForMember(dest => dest.AvailableBalance,
                           opt => opt.MapFrom(src => src.AllocatedDays - src.UsedDays));
            // Note: LeaveTypeName mapping relies on TPLRequests (via TPLRequest->LKPLeaveType)

            CreateMap<LeaveBalanceCreateDto, TPLLeaveBalance>()
                .ForMember(dest => dest.UsedDays, opt => opt.MapFrom(src => 0)); // Initialize UsedDays to 0

            // Update Mapping (INTERNAL USE ONLY)
            CreateMap<LeaveBalanceInternalUpdateDto, TPLLeaveBalance>()
                .ForMember(dest => dest.UsedDays, opt => opt.MapFrom(src => src.UsedDays))
                .ForMember(dest => dest.BalanceId, opt => opt.MapFrom(src => src.BalanceId))
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            // =========================================================================
            // 7. DTOs for LKPLeaveType (Leave Type - CRUD) - NEW MAPPING
            // =========================================================================
            // Read Mapping
            CreateMap<LKPLeaveType, LeaveTypeReadDto>()
                 .ForMember(dest => dest.RequiresMedicalNote, opt => opt.MapFrom(src => src.RequiresMedicalNote)); // Mapping Boolean

            // Create/Update Mapping
            CreateMap<LeaveTypeCreateDto, LKPLeaveType>();
            CreateMap<LeaveTypeUpdateDto, LKPLeaveType>();

            // =========================================================================
            // 8. DTOs for TPLLeave (Leave Log) - NEW MAPPING
            // =========================================================================
            CreateMap<TPLLeave, LeaveLogReadDto>()
                // Mapping the Leave Type Name through the Request Navigation Property
                .ForMember(dest => dest.LeaveTypeName,
                           opt => opt.MapFrom(src => src.request.leave_type.Name));

            // Create/Update DTOs are not needed for TPLLeave as it's a log table.

            // =========================================================================
            // 9. DTOs for TPLEmployee (Employee)
            // =========================================================================
            CreateMap<TPLEmployee, EmployeeReadDto>()
                // Mapping the names instead of IDs for better readability (JOINs)
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.NameEn))
                .ForMember(dest => dest.JobTitle, opt => opt.MapFrom(src => src.Job.Title));

            CreateMap<EmployeeCreateDto, TPLEmployee>();
            CreateMap<EmployeeUpdateDto, TPLEmployee>();

            // =========================================================================
            // 10. Responsible for the process of converting data between DTOs and Entities
            // =========================================================================

            // في HRSystem.BaseLibrary/Profiles/HRMappingProfile.cs

            // ... (Your existing Mappings for LeaveType and LeaveBalance)

            // --- TPLEmployee MAPPINGS ---
            // Create DTO to Entity
            CreateMap<EmployeeCreateDto, TPLEmployee>()
                .ForMember(dest => dest.EmployeeID, opt => opt.Ignore());

            // Update DTO to Existing Entity
            CreateMap<EmployeeUpdateDto, TPLEmployee>();

            // Entity to Read DTO
            CreateMap<TPLEmployee, EmployeeReadDto>()
                .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.EmployeeID));
            // Note: DepartmentName and JobTitle need mapping from Navigation Properties if they exist in the DTO

            // =========================================================================
            // 11. TPLLeave MAPPINGS (Leave Log)
            // =========================================================================
            CreateMap<TPLLeave, LeaveLogReadDto>()
            .ForMember(dest => dest.LeaveId, opt => opt.MapFrom(src => src.LeaveID))
            .ForMember(dest => dest.RequestId, opt => opt.MapFrom(src => src.request_id));

        }
    }
}
