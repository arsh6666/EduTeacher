using Rootfly.Mobile.Core.Common.Results;
using Rootfly.Mobile.Core.Networking.REST;
using EduTeacher.Shared.Education.Dtos;
using Volo.Abp.DependencyInjection;

namespace EduTeacher.Shared.Education.Services;

public class PayrollApiService : IPayrollApiService, ISingletonDependency
{
    private readonly IApiClient _api;

    public PayrollApiService(IApiClient apiClient) => _api = apiClient;

    public Task<ApiResult<SalaryStructureAssignmentDto>> GetSalaryStructureAsync(Guid employeeId)
        => _api.GetAsync<SalaryStructureAssignmentDto>($"api/payroll/salary-structure-assignment?EmployeeId={employeeId}&MaxResultCount=1");

    public Task<ApiResult<AbpPagedResultDto<SalarySlipDto>>> GetPayslipsAsync(Guid employeeId, int maxResultCount = 12)
        => _api.GetAsync<AbpPagedResultDto<SalarySlipDto>>($"api/payroll/salary-slip?EmployeeId={employeeId}&MaxResultCount={maxResultCount}&Sorting=Year+DESC,Month+DESC");

    public Task<ApiResult<SalarySlipDto>> GetPayslipDetailAsync(Guid slipId)
        => _api.GetAsync<SalarySlipDto>($"api/payroll/salary-slip/{slipId}");

    public Task<ApiResult<TaxDeclarationDto>> GetTaxDeclarationAsync(Guid employeeId, string fiscalYear)
        => _api.GetAsync<TaxDeclarationDto>($"api/payroll/employee-tax-declaration?EmployeeId={employeeId}&FiscalYear={fiscalYear}");

    public Task<ApiResult<object>> SubmitTaxDeclarationAsync(Guid id)
        => _api.PostAsync<object>($"api/payroll/employee-tax-declaration/{id}/submit", new { });

    public Task<ApiResult<FbpDeclarationDto>> GetFbpDeclarationAsync(Guid employeeId, string fiscalYear)
        => _api.GetAsync<FbpDeclarationDto>($"api/payroll/employee-fbp-declaration?EmployeeId={employeeId}&FiscalYear={fiscalYear}");

    public Task<ApiResult<object>> SubmitFbpDeclarationAsync(Guid id)
        => _api.PostAsync<object>($"api/payroll/employee-fbp-declaration/{id}/submit", new { });

    public Task<ApiResult<SalaryAdvanceDto>> RequestSalaryAdvanceAsync(decimal amount, string reason, int repaymentMonths)
        => _api.PostAsync<SalaryAdvanceDto>("api/payroll/salary-advance", new { amount, reason, repaymentMonths });

    public Task<ApiResult<AbpPagedResultDto<ReimbursementClaimDto>>> GetReimbursementClaimsAsync(Guid employeeId, int skipCount = 0, int maxResultCount = 20)
        => _api.GetAsync<AbpPagedResultDto<ReimbursementClaimDto>>($"api/payroll/reimbursement-claim?EmployeeId={employeeId}&SkipCount={skipCount}&MaxResultCount={maxResultCount}");

    public Task<ApiResult<ReimbursementClaimDto>> SubmitReimbursementClaimAsync(ReimbursementClaimDto input)
        => _api.PostAsync<ReimbursementClaimDto>("api/payroll/reimbursement-claim", input);

    public Task<ApiResult<OvertimeEntryDto>> LogOvertimeAsync(DateTime date, double hours, Guid? ruleId)
        => _api.PostAsync<OvertimeEntryDto>("api/payroll/overtime-entry", new { date, hours, overtimeRuleId = ruleId });

    public Task<ApiResult<AbpPagedResultDto<OvertimeEntryDto>>> GetOvertimeEntriesAsync(Guid employeeId, int skipCount = 0, int maxResultCount = 20)
        => _api.GetAsync<AbpPagedResultDto<OvertimeEntryDto>>($"api/payroll/overtime-entry?EmployeeId={employeeId}&SkipCount={skipCount}&MaxResultCount={maxResultCount}");
}
