namespace EduTeacher.Shared.Education.Dtos;

public class TaxDeclarationDto
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public string FiscalYear { get; set; } = string.Empty;
    public string Regime { get; set; } = string.Empty; // Old or New
    public string Status { get; set; } = string.Empty; // Draft, Submitted, Approved
    public List<TaxDeclarationLineDto> Declarations { get; set; } = [];
    public decimal TotalDeclaredAmount { get; set; }
    public decimal TotalApprovedAmount { get; set; }
}

public class TaxDeclarationLineDto
{
    public Guid Id { get; set; }
    public string SectionCode { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal DeclaredAmount { get; set; }
    public decimal ApprovedAmount { get; set; }
    public string? ProofBlobName { get; set; }
}
