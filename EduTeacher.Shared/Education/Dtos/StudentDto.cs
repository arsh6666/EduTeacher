namespace EduTeacher.Shared.Education.Dtos;

public class StudentDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? RollNumber { get; set; }
    public Guid? SectionId { get; set; }
    public string? SectionName { get; set; }
}
