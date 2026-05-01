namespace EduTeacher.Shared.Education.Dtos;

public class SalarySlipDto
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }
    public decimal GrossEarnings { get; set; }
    public decimal GrossDeductions { get; set; }
    public decimal NetPay { get; set; }
    public string? Status { get; set; }
    public List<SalarySlipComponentDto> Earnings { get; set; } = [];
    public List<SalarySlipComponentDto> Deductions { get; set; } = [];
    public DateTime? PaidOn { get; set; }
}

public class SalarySlipComponentDto
{
    public string ComponentName { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string Type { get; set; } = string.Empty; // Earning or Deduction
}
