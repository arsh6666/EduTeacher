namespace EduTeacher.Shared.Education.Dtos;

public class SectionDto
{
    public Guid Id { get; set; }
    public Guid ProgramId { get; set; }
    public Guid? TermId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? ProgramName { get; set; }
    public string? TermName { get; set; }
    public int StudentCount { get; set; }
}
