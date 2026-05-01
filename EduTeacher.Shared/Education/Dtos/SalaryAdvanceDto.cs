namespace EduTeacher.Shared.Education.Dtos;

public class SalaryAdvanceDto
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public decimal Amount { get; set; }
    public string Reason { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty; // Draft, Submitted, Approved, Rejected, Disbursed
    public int RepaymentMonths { get; set; }
    public DateTime RequestDate { get; set; }
    public DateTime? ApprovedDate { get; set; }
}
