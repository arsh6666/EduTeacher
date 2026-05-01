using Rootfly.Mobile.Core.Common.Results;
using Rootfly.Mobile.Core.Common.DTOs;
using EduTeacher.Shared.Education.Dtos;

namespace EduTeacher.Shared.Education.Services;

public interface IPayrollApiService
{
    Task<ApiResult<SalaryStructureAssignmentDto>> GetSalaryStructureAsync(Guid employeeId);
    Task<ApiResult<AbpPagedResultDto<SalarySlipDto>>> GetPayslipsAsync(Guid employeeId, int maxResultCount = 12);
    Task<ApiResult<SalarySlipDto>> GetPayslipDetailAsync(Guid slipId);
    Task<ApiResult<TaxDeclarationDto>> GetTaxDeclarationAsync(Guid employeeId, string fiscalYear);
    Task<ApiResult<object>> SubmitTaxDeclarationAsync(Guid id);
    Task<ApiResult<FbpDeclarationDto>> GetFbpDeclarationAsync(Guid employeeId, string fiscalYear);
    Task<ApiResult<object>> SubmitFbpDeclarationAsync(Guid id);
    Task<ApiResult<SalaryAdvanceDto>> RequestSalaryAdvanceAsync(decimal amount, string reason, int repaymentMonths);
    Task<ApiResult<AbpPagedResultDto<ReimbursementClaimDto>>> GetReimbursementClaimsAsync(Guid employeeId, int skipCount = 0, int maxResultCount = 20);
    Task<ApiResult<ReimbursementClaimDto>> SubmitReimbursementClaimAsync(ReimbursementClaimDto input);
    Task<ApiResult<OvertimeEntryDto>> LogOvertimeAsync(DateTime date, double hours, Guid? ruleId);
    Task<ApiResult<AbpPagedResultDto<OvertimeEntryDto>>> GetOvertimeEntriesAsync(Guid employeeId, int skipCount = 0, int maxResultCount = 20);
}
