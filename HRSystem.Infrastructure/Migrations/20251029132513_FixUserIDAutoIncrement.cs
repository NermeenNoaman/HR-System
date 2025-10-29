using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixUserIDAutoIncrement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LkpBenefitTypes",
                columns: table => new
                {
                    BenefitTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LkpBenefitTypes", x => x.BenefitTypeID);
                });

            migrationBuilder.CreateTable(
                name: "LkpGeneralDataCompanyProfiles",
                columns: table => new
                {
                    CompanyProfileId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    InsuranceNumber = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    TaxNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FaxNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    WebSite = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ImageURL = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    CompanyCode = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LkpGeneralDataCompanyProfiles", x => x.CompanyProfileId);
                });

            migrationBuilder.CreateTable(
                name: "LKPLeaveTypes",
                columns: table => new
                {
                    LeaveTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    RequiresMedicalNote = table.Column<bool>(type: "bit", nullable: false),
                    IsDeductFromBalance = table.Column<bool>(type: "bit", nullable: false),
                    MaxDaysPerYear = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveTypes", x => x.LeaveTypeId);
                });

            migrationBuilder.CreateTable(
                name: "LKPPermissionTypes",
                columns: table => new
                {
                    permission_type_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    permission_type_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    monthly_limit_in_hours = table.Column<int>(type: "int", nullable: false),
                    is_deductible = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LKPPermissionTypes", x => x.permission_type_id);
                });

            migrationBuilder.CreateTable(
                name: "TPLCVBank",
                columns: table => new
                {
                    CV_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CV_File = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    AddedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TPLCVBank", x => x.CV_ID);
                });

            migrationBuilder.CreateTable(
                name: "TPLEvaluationCriteria",
                columns: table => new
                {
                    CriteriaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CriteriaName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Weight = table.Column<decimal>(type: "decimal(5,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TPLEvaluationCriteria", x => x.CriteriaID);
                });

            migrationBuilder.CreateTable(
                name: "TPLJob",
                columns: table => new
                {
                    JobID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartmentID = table.Column<int>(type: "int", nullable: false),
                    PostedDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TPLJob__056690E20F0CC5B9", x => x.JobID);
                });

            migrationBuilder.CreateTable(
                name: "TPLSurvey",
                columns: table => new
                {
                    SurveyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TPLSurve__A5481F9D7B7BA7D2", x => x.SurveyID);
                });

            migrationBuilder.CreateTable(
                name: "TPLTraining",
                columns: table => new
                {
                    TrainingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TPLTrain__E8D71DE221E8C971", x => x.TrainingID);
                });

            migrationBuilder.CreateTable(
                name: "LkpGeneralDataBranches",
                columns: table => new
                {
                    BranchId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NameEn = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LkpGeneralDataBranches", x => x.BranchId);
                    table.ForeignKey(
                        name: "FK_LkpGeneralDataBranches_LkpGeneralDataCompanyProfiles",
                        column: x => x.CompanyId,
                        principalTable: "LkpGeneralDataCompanyProfiles",
                        principalColumn: "CompanyProfileId");
                });

            migrationBuilder.CreateTable(
                name: "LkpJobApplication",
                columns: table => new
                {
                    JobApplicationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nchar(50)", fixedLength: true, maxLength: 50, nullable: false),
                    Phone = table.Column<int>(type: "int", nullable: false),
                    CVFile = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ApplyDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CV_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LkpJobApplication", x => x.JobApplicationId);
                    table.ForeignKey(
                        name: "FK_LkpJobApplication_TPLCVBank",
                        column: x => x.CV_ID,
                        principalTable: "TPLCVBank",
                        principalColumn: "CV_ID");
                });

            migrationBuilder.CreateTable(
                name: "LkpHRDepartments",
                columns: table => new
                {
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    NameEn = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    NameAr = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Location = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ManagerId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LkpDepartments", x => x.DepartmentId);
                    table.ForeignKey(
                        name: "FK_LkpHRDepartments_LkpGeneralDataBranches",
                        column: x => x.BranchId,
                        principalTable: "LkpGeneralDataBranches",
                        principalColumn: "BranchId");
                });

            migrationBuilder.CreateTable(
                name: "TPLCandidate",
                columns: table => new
                {
                    CandidateID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    JobApplicationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TPLCandi__DF539BFC3A8F1883", x => x.CandidateID);
                    table.ForeignKey(
                        name: "FK_TPLCandidate_LkpJobApplication",
                        column: x => x.JobApplicationId,
                        principalTable: "LkpJobApplication",
                        principalColumn: "JobApplicationId");
                });

            migrationBuilder.CreateTable(
                name: "TPLEmployee",
                columns: table => new
                {
                    EmployeeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    HireDate = table.Column<DateOnly>(type: "date", nullable: false),
                    JobID = table.Column<int>(type: "int", nullable: false),
                    DepartmentID = table.Column<int>(type: "int", nullable: false),
                    EmploymentStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Employee__7AD04FF148254588", x => x.EmployeeID);
                    table.ForeignKey(
                        name: "FK_TPLEmployee_Job",
                        column: x => x.JobID,
                        principalTable: "TPLJob",
                        principalColumn: "JobID");
                    table.ForeignKey(
                        name: "FK_TPLEmployee_LkpHRDepartments",
                        column: x => x.DepartmentID,
                        principalTable: "LkpHRDepartments",
                        principalColumn: "DepartmentId");
                });

            migrationBuilder.CreateTable(
                name: "TPLHRNeedRequests",
                columns: table => new
                {
                    HRNeedID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HRNeedRequests", x => x.HRNeedID);
                    table.ForeignKey(
                        name: "FK_TPLHRNeedRequests_LkpHRDepartments",
                        column: x => x.DepartmentId,
                        principalTable: "LkpHRDepartments",
                        principalColumn: "DepartmentId");
                });

            migrationBuilder.CreateTable(
                name: "LKPSalary",
                columns: table => new
                {
                    SalaryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeID = table.Column<int>(type: "int", nullable: false),
                    BaseSalary = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Bonus = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Deductions = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    NetSalary = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    PayDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__LKPSalar__4BE204B719479BCC", x => x.SalaryID);
                    table.ForeignKey(
                        name: "FK_LKPSalary_TPLEmployee",
                        column: x => x.EmployeeID,
                        principalTable: "TPLEmployee",
                        principalColumn: "EmployeeID");
                });

            migrationBuilder.CreateTable(
                name: "TPLAssetManagement",
                columns: table => new
                {
                    AssetID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssetName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SerialNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AssignedTo = table.Column<int>(type: "int", nullable: false),
                    AssignedDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ReturnDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TPLAsset__434923721A60BBB8", x => x.AssetID);
                    table.ForeignKey(
                        name: "FK_TPLAssetManagement_TPLEmployee",
                        column: x => x.AssignedTo,
                        principalTable: "TPLEmployee",
                        principalColumn: "EmployeeID");
                });

            migrationBuilder.CreateTable(
                name: "TPLAttendance",
                columns: table => new
                {
                    AttendanceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeID = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    CheckIn = table.Column<TimeOnly>(type: "time", nullable: true),
                    CheckOut = table.Column<TimeOnly>(type: "time", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TPLAtten__8B69263C191476DC", x => x.AttendanceID);
                    table.ForeignKey(
                        name: "FK_TPLAttendance_TPLEmployee",
                        column: x => x.EmployeeID,
                        principalTable: "TPLEmployee",
                        principalColumn: "EmployeeID");
                });

            migrationBuilder.CreateTable(
                name: "TPLBenefitsCompensation",
                columns: table => new
                {
                    BenefitID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeID = table.Column<int>(type: "int", nullable: false),
                    BenefitTypeID = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Value = table.Column<decimal>(type: "decimal(10,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Benefits__5754C53AEB6B105B", x => x.BenefitID);
                    table.ForeignKey(
                        name: "FK_TPLBenefitsCompensation_LkpBenefitTypes",
                        column: x => x.BenefitTypeID,
                        principalTable: "LkpBenefitTypes",
                        principalColumn: "BenefitTypeID");
                    table.ForeignKey(
                        name: "FK_TPLBenefitsCompensation_TPLEmployee",
                        column: x => x.EmployeeID,
                        principalTable: "TPLEmployee",
                        principalColumn: "EmployeeID");
                });

            migrationBuilder.CreateTable(
                name: "TPLDisciplinaryActions",
                columns: table => new
                {
                    ActionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeID = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    ActionType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TakenBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TPLDisci__FFE3F4B90953BFED", x => x.ActionID);
                    table.ForeignKey(
                        name: "FK_TPLDisciplinaryActions_Employee",
                        column: x => x.TakenBy,
                        principalTable: "TPLEmployee",
                        principalColumn: "EmployeeID");
                    table.ForeignKey(
                        name: "FK_TPLDisciplinaryActions_Employee1",
                        column: x => x.EmployeeID,
                        principalTable: "TPLEmployee",
                        principalColumn: "EmployeeID");
                });

            migrationBuilder.CreateTable(
                name: "TPLDocumentManagement",
                columns: table => new
                {
                    DocumentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeID = table.Column<int>(type: "int", nullable: false),
                    DocumentType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UploadDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ExpiryDate = table.Column<DateOnly>(type: "date", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TPLDocum__1ABEEF6F44C1B7DC", x => x.DocumentID);
                    table.ForeignKey(
                        name: "FK_TPLDocumentManagement_Employee",
                        column: x => x.EmployeeID,
                        principalTable: "TPLEmployee",
                        principalColumn: "EmployeeID");
                });

            migrationBuilder.CreateTable(
                name: "TPLEmployee_Training",
                columns: table => new
                {
                    EmployeeID = table.Column<int>(type: "int", nullable: false),
                    TrainingID = table.Column<int>(type: "int", nullable: false),
                    CompletionStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TPLEmplo__445D3E2F19C3B2EB", x => new { x.EmployeeID, x.TrainingID });
                    table.ForeignKey(
                        name: "FK_TPLEmployee_Training_TPLEmployee",
                        column: x => x.EmployeeID,
                        principalTable: "TPLEmployee",
                        principalColumn: "EmployeeID");
                    table.ForeignKey(
                        name: "FK__TPLEmploy__Train__2180FB33",
                        column: x => x.TrainingID,
                        principalTable: "TPLTraining",
                        principalColumn: "TrainingID");
                });

            migrationBuilder.CreateTable(
                name: "TPLInterview",
                columns: table => new
                {
                    InterviewID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CandidateID = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Result = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InterviewerID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Intervie__C97C5832B0092694", x => x.InterviewID);
                    table.ForeignKey(
                        name: "FK_TPLInterview_TPLCandidate",
                        column: x => x.CandidateID,
                        principalTable: "TPLCandidate",
                        principalColumn: "CandidateID");
                    table.ForeignKey(
                        name: "FK_TPLInterview_TPLEmployee1",
                        column: x => x.InterviewerID,
                        principalTable: "TPLEmployee",
                        principalColumn: "EmployeeID");
                });

            migrationBuilder.CreateTable(
                name: "TPLLeaveBalance",
                columns: table => new
                {
                    BalanceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    LeaveTypeId = table.Column<int>(type: "int", nullable: false),
                    AllocatedDays = table.Column<int>(type: "int", nullable: false),
                    UsedDays = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TPLLeaveBalance", x => x.BalanceId);
                    table.ForeignKey(
                        name: "FK_TPLLeaveBalance_TPLEmployee",
                        column: x => x.EmployeeId,
                        principalTable: "TPLEmployee",
                        principalColumn: "EmployeeID");
                });

            migrationBuilder.CreateTable(
                name: "TPLOffboarding",
                columns: table => new
                {
                    ExitID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeID = table.Column<int>(type: "int", nullable: false),
                    ResignationDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ExitReason = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ClearanceStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ExitInterviewNotes = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TPLOffbo__26D64E9808277847", x => x.ExitID);
                    table.ForeignKey(
                        name: "FK_TPLOffboarding_TPLEmployee",
                        column: x => x.EmployeeID,
                        principalTable: "TPLEmployee",
                        principalColumn: "EmployeeID");
                });

            migrationBuilder.CreateTable(
                name: "TPLOnboarding",
                columns: table => new
                {
                    OnboardingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeID = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    AssignedMentor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ChecklistStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Onboardi__43F2371E2B631C75", x => x.OnboardingID);
                    table.ForeignKey(
                        name: "FK_TPLOnboarding_TPLEmployee",
                        column: x => x.EmployeeID,
                        principalTable: "TPLEmployee",
                        principalColumn: "EmployeeID");
                });

            migrationBuilder.CreateTable(
                name: "TPLPerformanceEvaluation",
                columns: table => new
                {
                    EvaluationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeID = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CriteriaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TPLPerfo__36AE68D36D7296F8", x => x.EvaluationID);
                    table.ForeignKey(
                        name: "FK_TPLPerformanceEvaluation_TPLEmployee2",
                        column: x => x.EmployeeID,
                        principalTable: "TPLEmployee",
                        principalColumn: "EmployeeID");
                    table.ForeignKey(
                        name: "FK_TPLPerformanceEvaluation_TPLEvaluationCriteria",
                        column: x => x.CriteriaID,
                        principalTable: "TPLEvaluationCriteria",
                        principalColumn: "CriteriaID");
                });

            migrationBuilder.CreateTable(
                name: "TPLPermissions",
                columns: table => new
                {
                    permission_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    employee_id = table.Column<int>(type: "int", nullable: false),
                    permission_type_id = table.Column<int>(type: "int", nullable: false),
                    request_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    start_time = table.Column<DateTime>(type: "datetime", nullable: false),
                    end_time = table.Column<DateTime>(type: "datetime", nullable: true),
                    total_hours = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TPLPermissions", x => x.permission_id);
                    table.ForeignKey(
                        name: "FK_TPLPermissions_LKPPermissionTypes",
                        column: x => x.permission_type_id,
                        principalTable: "LKPPermissionTypes",
                        principalColumn: "permission_type_id");
                    table.ForeignKey(
                        name: "FK_TPLPermissions_TPLEmployee",
                        column: x => x.employee_id,
                        principalTable: "TPLEmployee",
                        principalColumn: "EmployeeID");
                });

            migrationBuilder.CreateTable(
                name: "TPLProject",
                columns: table => new
                {
                    ProjectID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: true),
                    ManagerID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TPLProje__761ABED0A98D3175", x => x.ProjectID);
                    table.ForeignKey(
                        name: "FK_TPLProject_Manager",
                        column: x => x.ManagerID,
                        principalTable: "TPLEmployee",
                        principalColumn: "EmployeeID");
                });

            migrationBuilder.CreateTable(
                name: "TPLRequests",
                columns: table => new
                {
                    request_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    employee_id = table.Column<int>(type: "int", nullable: false),
                    leave_type_id = table.Column<int>(type: "int", nullable: false),
                    start_date = table.Column<DateOnly>(type: "date", nullable: false),
                    end_date = table.Column<DateOnly>(type: "date", nullable: false),
                    number_of_days = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    ApprovedBy = table.Column<int>(type: "int", nullable: true),
                    submission_date = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TPLRequests", x => x.request_id);
                    table.ForeignKey(
                        name: "FK_TPLRequests_LKPLeaveTypes",
                        column: x => x.leave_type_id,
                        principalTable: "LKPLeaveTypes",
                        principalColumn: "LeaveTypeId");
                    table.ForeignKey(
                        name: "FK_TPLRequests_TPLEmployee",
                        column: x => x.employee_id,
                        principalTable: "TPLEmployee",
                        principalColumn: "EmployeeID");
                });

            migrationBuilder.CreateTable(
                name: "TPLSelfServiceRequests",
                columns: table => new
                {
                    RequestID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeID = table.Column<int>(type: "int", nullable: false),
                    RequestType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RequestDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ApprovedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TPLSelfS__33A8519A2AA9D005", x => x.RequestID);
                    table.ForeignKey(
                        name: "FK_TPLSelfServiceRequests_TPLEmployee",
                        column: x => x.EmployeeID,
                        principalTable: "TPLEmployee",
                        principalColumn: "EmployeeID");
                });

            migrationBuilder.CreateTable(
                name: "TPLSurvey_Response",
                columns: table => new
                {
                    ResponseID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SurveyID = table.Column<int>(type: "int", nullable: false),
                    EmployeeID = table.Column<int>(type: "int", nullable: false),
                    ResponseText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rating = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TPLSurve__1AAA640CF9F8B7BF", x => x.ResponseID);
                    table.ForeignKey(
                        name: "FK_TPLSurvey_Response_TPLEmployee",
                        column: x => x.EmployeeID,
                        principalTable: "TPLEmployee",
                        principalColumn: "EmployeeID");
                    table.ForeignKey(
                        name: "FK__TPLSurvey__Surve__31B762FC",
                        column: x => x.SurveyID,
                        principalTable: "TPLSurvey",
                        principalColumn: "SurveyID");
                });

            migrationBuilder.CreateTable(
                name: "TPLUser",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeID = table.Column<int>(type: "int", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__User__1788CCAC2EB55247", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_TPLUser_TPLEmployee",
                        column: x => x.EmployeeID,
                        principalTable: "TPLEmployee",
                        principalColumn: "EmployeeID");
                });

            migrationBuilder.CreateTable(
                name: "TPLRecruitmentPortal",
                columns: table => new
                {
                    PortalID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HRNeedID = table.Column<int>(type: "int", nullable: false),
                    PublishDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ExpiryDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TPLRecru__B87D58338DAADC9C", x => x.PortalID);
                    table.ForeignKey(
                        name: "FK_TPLRecruitmentPortal_TPLHRNeedRequests",
                        column: x => x.HRNeedID,
                        principalTable: "TPLHRNeedRequests",
                        principalColumn: "HRNeedID");
                });

            migrationBuilder.CreateTable(
                name: "TPLProject_Assignment",
                columns: table => new
                {
                    assignment_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeID = table.Column<int>(type: "int", nullable: false),
                    ProjectID = table.Column<int>(type: "int", nullable: false),
                    RoleInProject = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    HoursWorked = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TPLProject_Assignment", x => x.assignment_id);
                    table.ForeignKey(
                        name: "FK_TPLProject_Assignment_TPLEmployee",
                        column: x => x.EmployeeID,
                        principalTable: "TPLEmployee",
                        principalColumn: "EmployeeID");
                    table.ForeignKey(
                        name: "FK_TPLProject_Assignment_TPLProject",
                        column: x => x.ProjectID,
                        principalTable: "TPLProject",
                        principalColumn: "ProjectID");
                });

            migrationBuilder.CreateTable(
                name: "TPLLeave",
                columns: table => new
                {
                    LeaveID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeID = table.Column<int>(type: "int", nullable: false),
                    LeaveTypeId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    request_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Leave__796DB97974021ABD", x => x.LeaveID);
                    table.ForeignKey(
                        name: "FK_TPLLeave_TPLRequests",
                        column: x => x.request_id,
                        principalTable: "TPLRequests",
                        principalColumn: "request_id");
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Expires = table.Column<DateTime>(type: "datetime", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    Revoked = table.Column<DateTime>(type: "datetime", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__RefreshT__3214EC07E7828848", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_TPLUser_UserId",
                        column: x => x.UserId,
                        principalTable: "TPLUser",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LkpGeneralDataBranches_CompanyId",
                table: "LkpGeneralDataBranches",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_LkpHRDepartments_BranchId",
                table: "LkpHRDepartments",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_LkpJobApplication_CV_ID",
                table: "LkpJobApplication",
                column: "CV_ID");

            migrationBuilder.CreateIndex(
                name: "IX_LKPSalary_EmployeeID",
                table: "LKPSalary",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TPLAssetManagement_AssignedTo",
                table: "TPLAssetManagement",
                column: "AssignedTo");

            migrationBuilder.CreateIndex(
                name: "IX_TPLAttendance_EmployeeID",
                table: "TPLAttendance",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_TPLBenefitsCompensation_BenefitTypeID",
                table: "TPLBenefitsCompensation",
                column: "BenefitTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_TPLBenefitsCompensation_EmployeeID",
                table: "TPLBenefitsCompensation",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_TPLCandidate_JobApplicationId",
                table: "TPLCandidate",
                column: "JobApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_TPLDisciplinaryActions_EmployeeID",
                table: "TPLDisciplinaryActions",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_TPLDisciplinaryActions_TakenBy",
                table: "TPLDisciplinaryActions",
                column: "TakenBy");

            migrationBuilder.CreateIndex(
                name: "IX_TPLDocumentManagement_EmployeeID",
                table: "TPLDocumentManagement",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_TPLEmployee_DepartmentID",
                table: "TPLEmployee",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_TPLEmployee_JobID",
                table: "TPLEmployee",
                column: "JobID");

            migrationBuilder.CreateIndex(
                name: "UQ__Employee__A9D1053436BBDCA7",
                table: "TPLEmployee",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TPLEmployee_Training_TrainingID",
                table: "TPLEmployee_Training",
                column: "TrainingID");

            migrationBuilder.CreateIndex(
                name: "IX_TPLHRNeedRequests_DepartmentId",
                table: "TPLHRNeedRequests",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_TPLInterview_CandidateID",
                table: "TPLInterview",
                column: "CandidateID");

            migrationBuilder.CreateIndex(
                name: "IX_TPLInterview_InterviewerID",
                table: "TPLInterview",
                column: "InterviewerID");

            migrationBuilder.CreateIndex(
                name: "IX_Leave",
                table: "TPLLeave",
                column: "request_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TPLLeaveBalance_EmployeeId",
                table: "TPLLeaveBalance",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_TPLOffboarding_EmployeeID",
                table: "TPLOffboarding",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_TPLOnboarding_EmployeeID",
                table: "TPLOnboarding",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_TPLPerformanceEvaluation_CriteriaID",
                table: "TPLPerformanceEvaluation",
                column: "CriteriaID");

            migrationBuilder.CreateIndex(
                name: "IX_TPLPerformanceEvaluation_EmployeeID",
                table: "TPLPerformanceEvaluation",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_TPLPermissions_employee_id",
                table: "TPLPermissions",
                column: "employee_id");

            migrationBuilder.CreateIndex(
                name: "IX_TPLPermissions_permission_type_id",
                table: "TPLPermissions",
                column: "permission_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_TPLProject_ManagerID",
                table: "TPLProject",
                column: "ManagerID");

            migrationBuilder.CreateIndex(
                name: "IX_TPLProject_Assignment_EmployeeID",
                table: "TPLProject_Assignment",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_TPLProject_Assignment_ProjectID",
                table: "TPLProject_Assignment",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_TPLRecruitmentPortal_HRNeedID",
                table: "TPLRecruitmentPortal",
                column: "HRNeedID");

            migrationBuilder.CreateIndex(
                name: "IX_TPLRequests_employee_id",
                table: "TPLRequests",
                column: "employee_id");

            migrationBuilder.CreateIndex(
                name: "IX_TPLRequests_leave_type_id",
                table: "TPLRequests",
                column: "leave_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_TPLSelfServiceRequests_EmployeeID",
                table: "TPLSelfServiceRequests",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_TPLSurvey_Response_EmployeeID",
                table: "TPLSurvey_Response",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_TPLSurvey_Response_SurveyID",
                table: "TPLSurvey_Response",
                column: "SurveyID");

            migrationBuilder.CreateIndex(
                name: "IX_User",
                table: "TPLUser",
                column: "EmployeeID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__User__536C85E4A1E9872A",
                table: "TPLUser",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LKPSalary");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "TPLAssetManagement");

            migrationBuilder.DropTable(
                name: "TPLAttendance");

            migrationBuilder.DropTable(
                name: "TPLBenefitsCompensation");

            migrationBuilder.DropTable(
                name: "TPLDisciplinaryActions");

            migrationBuilder.DropTable(
                name: "TPLDocumentManagement");

            migrationBuilder.DropTable(
                name: "TPLEmployee_Training");

            migrationBuilder.DropTable(
                name: "TPLInterview");

            migrationBuilder.DropTable(
                name: "TPLLeave");

            migrationBuilder.DropTable(
                name: "TPLLeaveBalance");

            migrationBuilder.DropTable(
                name: "TPLOffboarding");

            migrationBuilder.DropTable(
                name: "TPLOnboarding");

            migrationBuilder.DropTable(
                name: "TPLPerformanceEvaluation");

            migrationBuilder.DropTable(
                name: "TPLPermissions");

            migrationBuilder.DropTable(
                name: "TPLProject_Assignment");

            migrationBuilder.DropTable(
                name: "TPLRecruitmentPortal");

            migrationBuilder.DropTable(
                name: "TPLSelfServiceRequests");

            migrationBuilder.DropTable(
                name: "TPLSurvey_Response");

            migrationBuilder.DropTable(
                name: "TPLUser");

            migrationBuilder.DropTable(
                name: "LkpBenefitTypes");

            migrationBuilder.DropTable(
                name: "TPLTraining");

            migrationBuilder.DropTable(
                name: "TPLCandidate");

            migrationBuilder.DropTable(
                name: "TPLRequests");

            migrationBuilder.DropTable(
                name: "TPLEvaluationCriteria");

            migrationBuilder.DropTable(
                name: "LKPPermissionTypes");

            migrationBuilder.DropTable(
                name: "TPLProject");

            migrationBuilder.DropTable(
                name: "TPLHRNeedRequests");

            migrationBuilder.DropTable(
                name: "TPLSurvey");

            migrationBuilder.DropTable(
                name: "LkpJobApplication");

            migrationBuilder.DropTable(
                name: "LKPLeaveTypes");

            migrationBuilder.DropTable(
                name: "TPLEmployee");

            migrationBuilder.DropTable(
                name: "TPLCVBank");

            migrationBuilder.DropTable(
                name: "TPLJob");

            migrationBuilder.DropTable(
                name: "LkpHRDepartments");

            migrationBuilder.DropTable(
                name: "LkpGeneralDataBranches");

            migrationBuilder.DropTable(
                name: "LkpGeneralDataCompanyProfiles");
        }
    }
}
