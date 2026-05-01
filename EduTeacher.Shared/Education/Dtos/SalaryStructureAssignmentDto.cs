namespace EduTeacher.Shared.Education.Dtos;

public class SalaryStructureAssignmentDto
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public Guid SalaryStructureId { get; set; }
    public string StructureName { get; set; } = string.Empty;
    public decimal Ctc { get; set; }
    public decimal BasicSalary { get; set; }
    public DateTime EffectiveFrom { get; set; }
    public DateTime? EffectiveTo { get; set; }
}
