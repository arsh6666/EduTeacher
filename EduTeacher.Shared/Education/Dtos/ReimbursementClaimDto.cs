namespace EduTeacher.Shared.Education.Dtos;

public class ReimbursementClaimDto
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public string Type { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string Status { get; set; } = string.Empty; // Draft, Submitted, Approved, Rejected, Paid
    public string? Description { get; set; }
    public DateTime RequestDate { get; set; }
    public List<ReimbursementLineDto> Lines { get; set; } = [];
}

public class ReimbursementLineDto
{
    public Guid Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string? ReceiptBlobName { get; set; }
    public DateTime? ExpenseDate { get; set; }
}
