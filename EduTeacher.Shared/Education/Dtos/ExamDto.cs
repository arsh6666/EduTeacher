namespace EduTeacher.Shared.Education.Dtos;

public class ExamDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public Guid? TermId { get; set; }
    public string? TermName { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool IsPublished { get; set; }
    public DateTime CreationTime { get; set; }
}

public class ExamScheduleEntryDto
{
    public Guid Id { get; set; }
    public Guid ExamId { get; set; }
    public Guid? CourseOfferingId { get; set; }
    public string? CourseName { get; set; }
    public string? ExamName { get; set; }
    public DateTime Date { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public string? RoomName { get; set; }
    public double TotalMarks { get; set; }
}
