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