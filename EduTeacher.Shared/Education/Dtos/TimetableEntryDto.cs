namespace EduTeacher.Shared.Education.Dtos;

public class TimetableEntryDto
{
    public Guid Id { get; set; }
    public Guid SectionId { get; set; }
    public Guid? CourseOfferingId { get; set; }
    public Guid? InstructorId { get; set; }
    public Guid? RoomId { get; set; }
    public DayOfWeek DayOfWeek { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public string? CourseName { get; set; }
    public string? InstructorName { get; set; }
    public string? RoomName { get; set; }
    public string? SectionName { get; set; }
}
