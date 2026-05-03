namespace EduTeacher.Shared.Payroll.Dtos;

// ─── Salary Structure ───

public class SalaryStructureAssignmentDto
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public Guid SalaryStructureId { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public decimal BaseSalary { get; set; }
    public decimal VariablePay { get; set; }
    public decimal CTC { get; set; }
    public string? PayGrade { get; set; }
    public string? Currency { get; set; }
    public string? Notes { get; set; }
}

// ─── Salary Slip ───

public class SalarySlipDto
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public Guid PayrollEntryId { get; set; }
    public string SlipNumber { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime PayDate { get; set; }
    public decimal TotalEarnings { get; set; }
    public decimal TotalDeductions { get; set; }
    public decimal TotalStatutory { get; set; }
    public decimal TotalTax { get; set; }
    public decimal NetPay { get; set; }
    public decimal GrossPay { get; set; }
    public decimal CTC { get; set; }
    public SalarySlipStatus Status { get; set; }
    public string? HeldReason { get; set; }
    public PaymentMethodType? PaymentMode { get; set; }
    public string? PaymentReference { get; set; }
    public string? Notes { get; set; }
    public List<SalarySlipEarningDto> Earnings { get; set; } = [];
    public List<SalarySlipDeductionDto> Deductions { get; set; } = [];
    public List<SalarySlipLoanDto> Loans { get; set; } = [];
}

public class SalarySlipEarningDto
{
    public Guid Id { get; set; }
    public Guid SalarySlipId { get; set; }
    public Guid SalaryComponentId { get; set; }
    public string ComponentName { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public ComponentCalculationType CalculationType { get; set; }
    public string? FormulaUsed { get; set; }
}

public class SalarySlipDeductionDto
{
    public Guid Id { get; set; }
    public Guid SalarySlipId { get; set; }
    public Guid SalaryComponentId { get; set; }
    public string ComponentName { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public bool IsTaxDeduction { get; set; }
    public bool IsStatutory { get; set; }
}

public class SalarySlipLoanDto
{
    public Guid Id { get; set; }
    public Guid SalarySlipId { get; set; }
    public Guid? LoanId { get; set; }
    public string? LoanName { get; set; }
    public decimal EMIAmount { get; set; }
    public decimal PrincipalAmount { get; set; }
    public decimal InterestAmount { get; set; }
    public decimal BalanceAfter { get; set; }
}

// ─── Salary Advance ───

public class SalaryAdvanceDto
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public decimal Amount { get; set; }
    public string? Currency { get; set; }
    public DateTime RequestDate { get; set; }
    public string? Reason { get; set; }
    public AdvanceStatus Status { get; set; }
    public DateTime? DisbursedDate { get; set; }
    public int RepaymentMonths { get; set; }
    public DateTime? RepaymentStartMonth { get; set; }
    public string? Notes { get; set; }
    public List<SalaryAdvanceRepaymentDto> Repayments { get; set; } = [];
}

public class SalaryAdvanceRepaymentDto
{
    public Guid Id { get; set; }
    public Guid SalaryAdvanceId { get; set; }
    public DateTime MonthYear { get; set; }
    public decimal Amount { get; set; }
    public AdvanceRepaymentStatus Status { get; set; }
}

public class CreateSalaryAdvanceDto
{
    public Guid EmployeeId { get; set; }
    public decimal Amount { get; set; }
    public string? Reason { get; set; }
    public int RepaymentMonths { get; set; }
    public DateTime? RepaymentStartMonth { get; set; }
}

// ─── Tax Declaration ───

public class EmployeeTaxDeclarationDto
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public string? FiscalYear { get; set; }
    public DateTime DeclarationDate { get; set; }
    public TaxRegimeType RegimeType { get; set; }
    public decimal TotalDeclaredAmount { get; set; }
    public TaxDeclarationStatus Status { get; set; }
    public List<TaxDeclarationLineDto> Lines { get; set; } = [];
}

public class TaxDeclarationLineDto
{
    public Guid Id { get; set; }
    public Guid EmployeeTaxDeclarationId { get; set; }
    public Guid TaxExemptionCategoryId { get; set; }
    public decimal DeclaredAmount { get; set; }
    public decimal? ApprovedAmount { get; set; }
    public string? ProofDocumentUrl { get; set; }
    public string? Remarks { get; set; }
}

public class TaxExemptionCategoryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string? Section { get; set; }
    public decimal? MaxLimit { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; }
}

// ─── FBP Declaration ───

public class EmployeeFBPDeclarationDto
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public Guid FlexibleBenefitPlanId { get; set; }
    public string? FiscalYear { get; set; }
    public FBPDeclarationStatus Status { get; set; }
    public DateTime? ApprovalDate { get; set; }
    public List<EmployeeFBPLineDto> Lines { get; set; } = [];
}

public class EmployeeFBPLineDto
{
    public Guid Id { get; set; }
    public Guid EmployeeFBPDeclarationId { get; set; }
    public Guid SalaryComponentId { get; set; }
    public decimal DeclaredAmount { get; set; }
}

public class FlexibleBenefitPlanDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal MaxBenefitAmount { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; }
}

// ─── Reimbursements ───

public class ReimbursementClaimDto
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public DateTime ClaimDate { get; set; }
    public DateTime ClaimMonth { get; set; }
    public decimal TotalAmount { get; set; }
    public ReimbursementStatus Status { get; set; }
    public string? Notes { get; set; }
    public List<ReimbursementLineDto> Lines { get; set; } = [];
}

public class ReimbursementLineDto
{
    public Guid Id { get; set; }
    public Guid ReimbursementClaimId { get; set; }
    public Guid ReimbursementTypeId { get; set; }
    public string? Description { get; set; }
    public decimal Amount { get; set; }
    public string? ReceiptUrl { get; set; }
    public DateTime ExpenseDate { get; set; }
}

public class CreateReimbursementClaimDto
{
    public Guid EmployeeId { get; set; }
    public DateTime ClaimDate { get; set; } = DateTime.UtcNow;
    public DateTime ClaimMonth { get; set; }
    public decimal TotalAmount { get; set; }
    public string? Notes { get; set; }
    public List<CreateReimbursementLineDto> Lines { get; set; } = [];
}

public class CreateReimbursementLineDto
{
    public Guid ReimbursementTypeId { get; set; }
    public string? Description { get; set; }
    public decimal Amount { get; set; }
    public string? ReceiptUrl { get; set; }
    public DateTime ExpenseDate { get; set; }
}

public class ReimbursementTypeDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal? MaxAmount { get; set; }
    public bool RequiresReceipt { get; set; }
    public bool TaxExempt { get; set; }
    public bool IsActive { get; set; }
}

// ─── Overtime ───

public class OvertimeEntryDto
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public DateTime Date { get; set; }
    public decimal Hours { get; set; }
    public Guid OvertimeRuleId { get; set; }
    public decimal Amount { get; set; }
    public OvertimeEntryStatus Status { get; set; }
    public string? Notes { get; set; }
}

public class CreateOvertimeEntryDto
{
    public Guid EmployeeId { get; set; }
    public DateTime Date { get; set; }
    public decimal Hours { get; set; }
    public Guid OvertimeRuleId { get; set; }
    public decimal Amount { get; set; }
    public string? Notes { get; set; }
}

public class OvertimeRuleDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal RateMultiplier { get; set; }
    public OvertimeDayType DayType { get; set; }
    public decimal HoursThreshold { get; set; }
    public bool IsActive { get; set; }
}
