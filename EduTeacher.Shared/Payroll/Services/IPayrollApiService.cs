using Rootfly.Mobile.Core.Common.Results;
using Rootfly.Mobile.Core.Common.DTOs;
using EduTeacher.Shared.Payroll.Dtos;

namespace EduTeacher.Shared.Payroll.Services;

public interface IPayrollApiService
{
    // Salary structure
    Task<ApiResult<AbpPagedResultDto<SalaryStructureAssignmentDto>>> GetSalaryStructureAsync(Guid employeeId);

    // Payslips
    Task<ApiResult<AbpPagedResultDto<SalarySlipDto>>> GetPayslipsAsync(Guid employeeId, int skipCount = 0, int maxResultCount = 12);
    Task<ApiResult<SalarySlipDto>> GetPayslipDetailAsync(Guid slipId);

    // Tax declaration
    Task<ApiResult<AbpPagedResultDto<EmployeeTaxDeclarationDto>>> GetTaxDeclarationsAsync(Guid employeeId, string? fiscalYear = null);
    Task<ApiResult<EmployeeTaxDeclarationDto>> GetTaxDeclarationDetailAsync(Guid id);
    Task<ApiResult<object>> SubmitTaxDeclarationAsync(Guid id);
    Task<ApiResult<AbpPagedResultDto<TaxExemptionCategoryDto>>> GetTaxExemptionCategoriesAsync();

    // FBP declaration
    Task<ApiResult<AbpPagedResultDto<EmployeeFBPDeclarationDto>>> GetFBPDeclarationsAsync(Guid employeeId, string? fiscalYear = null);
    Task<ApiResult<object>> SubmitFBPDeclarationAsync(Guid id);
    Task<ApiResult<AbpPagedResultDto<FlexibleBenefitPlanDto>>> GetFlexibleBenefitPlansAsync();

    // Salary advance
    Task<ApiResult<AbpPagedResultDto<SalaryAdvanceDto>>> GetSalaryAdvancesAsync(Guid employeeId, int skipCount = 0, int maxResultCount = 20);
    Task<ApiResult<SalaryAdvanceDto>> RequestSalaryAdvanceAsync(CreateSalaryAdvanceDto input);

    // Reimbursements
    Task<ApiResult<AbpPagedResultDto<ReimbursementClaimDto>>> GetReimbursementsAsync(Guid employeeId, int skipCount = 0, int maxResultCount = 20);
    Task<ApiResult<ReimbursementClaimDto>> SubmitReimbursementAsync(CreateReimbursementClaimDto input);
    Task<ApiResult<AbpPagedResultDto<ReimbursementTypeDto>>> GetReimbursementTypesAsync();

    // Overtime
    Task<ApiResult<AbpPagedResultDto<OvertimeEntryDto>>> GetOvertimeEntriesAsync(Guid employeeId, int skipCount = 0, int maxResultCount = 20);
    Task<ApiResult<OvertimeEntryDto>> LogOvertimeAsync(CreateOvertimeEntryDto input);
    Task<ApiResult<AbpPagedResultDto<OvertimeRuleDto>>> GetOvertimeRulesAsync();
}
