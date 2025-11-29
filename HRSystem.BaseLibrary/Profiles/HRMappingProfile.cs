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

            // HRSystem.BaseLibrary/Profiles/HRMappingProfile.cs

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

            // =========================================================================
            // 19. TPLSelfServiceRequest MAPPINGS
            // =========================================================================
            CreateMap<SelfServiceRequestCreateDto, TPLSelfServiceRequest>()
                .ForMember(dest => dest.RequestID, opt => opt.Ignore());
            CreateMap<SelfServiceRequestUpdateDto, TPLSelfServiceRequest>();
            CreateMap<TPLSelfServiceRequest, SelfServiceRequestReadDto>();

            // =========================================================================
            // 20. TPLDocumentManagement MAPPINGS
            // =========================================================================
            CreateMap<DocumentManagementCreateDto, TPLDocumentManagement>()
                .ForMember(dest => dest.DocumentID, opt => opt.Ignore());
            CreateMap<DocumentManagementUpdateDto, TPLDocumentManagement>();
            CreateMap<TPLDocumentManagement, DocumentManagementReadDto>();

            // =========================================================================
            // 21. TPLBenefitsCompensation MAPPINGS
            // =========================================================================
            CreateMap<BenefitsCompensationCreateDto, TPLBenefitsCompensation>()
                .ForMember(dest => dest.BenefitID, opt => opt.Ignore());
            CreateMap<BenefitsCompensationUpdateDto, TPLBenefitsCompensation>();
            CreateMap<TPLBenefitsCompensation, BenefitsCompensationReadDto>();

            // =========================================================================
            // 22. LkpBenefitType MAPPINGS
            // =========================================================================
            CreateMap<BenefitTypeCreateDto, LkpBenefitType>()
                .ForMember(dest => dest.BenefitTypeID, opt => opt.Ignore());
            CreateMap<BenefitTypeUpdateDto, LkpBenefitType>();
            CreateMap<LkpBenefitType, BenefitTypeReadDto>();

            // =========================================================================
            // 23. LKPSalary MAPPINGS
            // =========================================================================
            CreateMap<SalaryCreateDto, LKPSalary>()
                .ForMember(dest => dest.SalaryID, opt => opt.Ignore());
            CreateMap<SalaryUpdateDto, LKPSalary>();
            CreateMap<LKPSalary, SalaryReadDto>();

            // =========================================================================
            // 24. TPLPerformanceEvaluation MAPPINGS
            // =========================================================================
            CreateMap<PerformanceEvaluationCreateDto, TPLPerformanceEvaluation>()
                .ForMember(dest => dest.EvaluationID, opt => opt.Ignore());
            CreateMap<PerformanceEvaluationUpdateDto, TPLPerformanceEvaluation>();
            CreateMap<TPLPerformanceEvaluation, PerformanceEvaluationReadDto>();

            // =========================================================================
            // 25. TPLEvaluationCriterion MAPPINGS
            // =========================================================================
            CreateMap<EvaluationCriteriaCreateDto, TPLEvaluationCriterion>()
                .ForMember(dest => dest.CriteriaID, opt => opt.Ignore());
            CreateMap<EvaluationCriteriaUpdateDto, TPLEvaluationCriterion>();
            CreateMap<TPLEvaluationCriterion, EvaluationCriteriaReadDto>();

            // =========================================================================
            // 26. TPLSurvey MAPPINGS
            // =========================================================================
            CreateMap<SurveyCreateDto, TPLSurvey>()
                .ForMember(dest => dest.SurveyID, opt => opt.Ignore());
            CreateMap<SurveyUpdateDto, TPLSurvey>();
            CreateMap<TPLSurvey, SurveyReadDto>();

            // =========================================================================
            // 27. TPLSurvey_Response MAPPINGS
            // =========================================================================
            CreateMap<SurveyResponseCreateDto, TPLSurvey_Response>()
                .ForMember(dest => dest.ResponseID, opt => opt.Ignore());
            CreateMap<SurveyResponseUpdateDto, TPLSurvey_Response>();
            CreateMap<TPLSurvey_Response, SurveyResponseReadDto>();

            // =========================================================================
            // 28. TPLJob MAPPINGS
            // =========================================================================
            CreateMap<JobCreateDto, TPLJob>()
                .ForMember(dest => dest.JobID, opt => opt.Ignore());
            CreateMap<JobUpdateDto, TPLJob>();
            CreateMap<TPLJob, JobReadDto>();

            // =========================================================================
            // 29. TPLHRNeedRequest MAPPINGS
            // =========================================================================
            CreateMap<HRNeedRequestCreateDto, TPLHRNeedRequest>()
                .ForMember(dest => dest.HRNeedID, opt => opt.Ignore());
            CreateMap<HRNeedRequestUpdateDto, TPLHRNeedRequest>();
            CreateMap<TPLHRNeedRequest, HRNeedRequestReadDto>();

            // =========================================================================
            // 30. TPLRecruitmentPortal MAPPINGS
            // =========================================================================
            CreateMap<RecruitmentPortalCreateDto, TPLRecruitmentPortal>()
                .ForMember(dest => dest.PortalID, opt => opt.Ignore());
            CreateMap<RecruitmentPortalUpdateDto, TPLRecruitmentPortal>();
            CreateMap<TPLRecruitmentPortal, RecruitmentPortalReadDto>();

            // =========================================================================
            // 31. TPLCVBank MAPPINGS
            // =========================================================================
            CreateMap<CVBankCreateDto, TPLCVBank>()
                .ForMember(dest => dest.CV_ID, opt => opt.Ignore());
            CreateMap<CVBankUpdateDto, TPLCVBank>();
            CreateMap<TPLCVBank, CVBankReadDto>();

            // =========================================================================
            // 32. LkpJobApplication MAPPINGS
            // =========================================================================
            CreateMap<JobApplicationCreateDto, LkpJobApplication>()
                .ForMember(dest => dest.JobApplicationId, opt => opt.Ignore());
            CreateMap<JobApplicationUpdateDto, LkpJobApplication>();
            CreateMap<LkpJobApplication, JobApplicationReadDto>();

            // =========================================================================
            // 33. TPLCandidate MAPPINGS
            // =========================================================================
            CreateMap<CandidateCreateDto, TPLCandidate>()
                .ForMember(dest => dest.CandidateID, opt => opt.Ignore());
            CreateMap<CandidateUpdateDto, TPLCandidate>();
            CreateMap<TPLCandidate, CandidateReadDto>();

            // =========================================================================
            // 34. TPLInterview MAPPINGS
            // =========================================================================
            CreateMap<InterviewCreateDto, TPLInterview>()
                .ForMember(dest => dest.InterviewID, opt => opt.Ignore());
            CreateMap<InterviewUpdateDto, TPLInterview>();
            CreateMap<TPLInterview, InterviewReadDto>();

        }
    }
}