namespace EduTeacher.Shared.Education.Dtos;

public class OvertimeEntryDto
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public DateTime Date { get; set; }
    public double Hours { get; set; }
    public string Status { get; set; } = string.Empty; // Draft, Submitted, Approved, Rejected
    public Guid? OvertimeRuleId { get; set; }
    public string? RuleName { get; set; }
    public decimal? CalculatedAmount { get; set; }
}
