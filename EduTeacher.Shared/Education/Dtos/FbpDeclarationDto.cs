namespace EduTeacher.Shared.Education.Dtos;

public class FbpDeclarationDto
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public string FiscalYear { get; set; } = string.Empty;
    public decimal TotalBudget { get; set; }
    public string Status { get; set; } = string.Empty;
    public List<FbpAllocationDto> Allocations { get; set; } = [];
}

public class FbpAllocationDto
{
    public Guid Id { get; set; }
    public string ComponentName { get; set; } = string.Empty;
    public decimal AllocatedAmount { get; set; }
    public decimal MinAmount { get; set; }
    public decimal MaxAmount { get; set; }
}
