using Rootfly.Mobile.Core.Common.Results;
using Rootfly.Mobile.Core.Common.DTOs;
using Rootfly.Mobile.Core.Networking.REST;
using EduTeacher.Shared.Payroll.Dtos;
using Volo.Abp.DependencyInjection;

namespace EduTeacher.Shared.Payroll.Services;

public class PayrollApiService : IPayrollApiService, ISingletonDependency
{
    private readonly IApiClient _api;
    private const string Base = "api/payroll";

    public PayrollApiService(IApiClient api) => _api = api;

    // Salary structure
    public Task<ApiResult<AbpPagedResultDto<SalaryStructureAssignmentDto>>> GetSalaryStructureAsync(Guid employeeId)
        => _api.GetAsync<AbpPagedResultDto<SalaryStructureAssignmentDto>>($"{Base}/salary-structure-assignment?EmployeeId={employeeId}&MaxResultCount=1");

    // Payslips
    public Task<ApiResult<AbpPagedResultDto<SalarySlipDto>>> GetPayslipsAsync(Guid employeeId, int skipCount = 0, int maxResultCount = 12)
        => _api.GetAsync<AbpPagedResultDto<SalarySlipDto>>($"{Base}/salary-slip?EmployeeId={employeeId}&SkipCount={skipCount}&MaxResultCount={maxResultCount}&Sorting=PayDate+DESC");

    public Task<ApiResult<SalarySlipDto>> GetPayslipDetailAsync(Guid slipId)
        => _api.GetAsync<SalarySlipDto>($"{Base}/salary-slip/{slipId}");

    // Tax declaration
    public Task<ApiResult<AbpPagedResultDto<EmployeeTaxDeclarationDto>>> GetTaxDeclarationsAsync(Guid employeeId, string? fiscalYear = null)
    {
        var url = $"{Base}/employee-tax-declaration?EmployeeId={employeeId}&MaxResultCount=10";
        if (!string.IsNullOrEmpty(fiscalYear)) url += $"&FiscalYear={fiscalYear}";
        return _api.GetAsync<AbpPagedResultDto<EmployeeTaxDeclarationDto>>(url);
    }

    public Task<ApiResult<EmployeeTaxDeclarationDto>> GetTaxDeclarationDetailAsync(Guid id)
        => _api.GetAsync<EmployeeTaxDeclarationDto>($"{Base}/employee-tax-declaration/{id}");

    public Task<ApiResult<object>> SubmitTaxDeclarationAsync(Guid id)
        => _api.PostAsync<object>($"{Base}/employee-tax-declaration/{id}/submit", new { });

    public Task<ApiResult<AbpPagedResultDto<TaxExemptionCategoryDto>>> GetTaxExemptionCategoriesAsync()
        => _api.GetAsync<AbpPagedResultDto<TaxExemptionCategoryDto>>($"{Base}/tax-exemption-category?MaxResultCount=100&IsActive=true");

    // FBP declaration
    public Task<ApiResult<AbpPagedResultDto<EmployeeFBPDeclarationDto>>> GetFBPDeclarationsAsync(Guid employeeId, string? fiscalYear = null)
    {
        var url = $"{Base}/employee-fbp-declaration?EmployeeId={employeeId}&MaxResultCount=10";
        if (!string.IsNullOrEmpty(fiscalYear)) url += $"&FiscalYear={fiscalYear}";
        return _api.GetAsync<AbpPagedResultDto<EmployeeFBPDeclarationDto>>(url);
    }

    public Task<ApiResult<object>> SubmitFBPDeclarationAsync(Guid id)
        => _api.PostAsync<object>($"{Base}/employee-fbp-declaration/{id}/submit", new { });

    public Task<ApiResult<AbpPagedResultDto<FlexibleBenefitPlanDto>>> GetFlexibleBenefitPlansAsync()
        => _api.GetAsync<AbpPagedResultDto<FlexibleBenefitPlanDto>>($"{Base}/flexible-benefit-plan?MaxResultCount=50&IsActive=true");

    // Salary advance
    public Task<ApiResult<AbpPagedResultDto<SalaryAdvanceDto>>> GetSalaryAdvancesAsync(Guid employeeId, int skipCount = 0, int maxResultCount = 20)
        => _api.GetAsync<AbpPagedResultDto<SalaryAdvanceDto>>($"{Base}/salary-advance?EmployeeId={employeeId}&SkipCount={skipCount}&MaxResultCount={maxResultCount}");

    public Task<ApiResult<SalaryAdvanceDto>> RequestSalaryAdvanceAsync(CreateSalaryAdvanceDto input)
        => _api.PostAsync<SalaryAdvanceDto>($"{Base}/salary-advance", input);

    // Reimbursements
    public Task<ApiResult<AbpPagedResultDto<ReimbursementClaimDto>>> GetReimbursementsAsync(Guid employeeId, int skipCount = 0, int maxResultCount = 20)
        => _api.GetAsync<AbpPagedResultDto<ReimbursementClaimDto>>($"{Base}/reimbursement-claim?EmployeeId={employeeId}&SkipCount={skipCount}&MaxResultCount={maxResultCount}");

    public Task<ApiResult<ReimbursementClaimDto>> SubmitReimbursementAsync(CreateReimbursementClaimDto input)
        => _api.PostAsync<ReimbursementClaimDto>($"{Base}/reimbursement-claim", input);

    public Task<ApiResult<AbpPagedResultDto<ReimbursementTypeDto>>> GetReimbursementTypesAsync()
        => _api.GetAsync<AbpPagedResultDto<ReimbursementTypeDto>>($"{Base}/reimbursement-type?MaxResultCount=50&IsActive=true");

    // Overtime
    public Task<ApiResult<AbpPagedResultDto<OvertimeEntryDto>>> GetOvertimeEntriesAsync(Guid employeeId, int skipCount = 0, int maxResultCount = 20)
        => _api.GetAsync<AbpPagedResultDto<OvertimeEntryDto>>($"{Base}/overtime-entry?EmployeeId={employeeId}&SkipCount={skipCount}&MaxResultCount={maxResultCount}");

    public Task<ApiResult<OvertimeEntryDto>> LogOvertimeAsync(CreateOvertimeEntryDto input)
        => _api.PostAsync<OvertimeEntryDto>($"{Base}/overtime-entry", input);

    public Task<ApiResult<AbpPagedResultDto<OvertimeRuleDto>>> GetOvertimeRulesAsync()
        => _api.GetAsync<AbpPagedResultDto<OvertimeRuleDto>>($"{Base}/overtime-rule?MaxResultCount=50&IsActive=true");
}
