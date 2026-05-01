namespace EduTeacher.Shared.Education.Dtos;

public class AttendanceRecordDto
{
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public Guid? CourseOfferingId { get; set; }
    public Guid? SectionId { get; set; }
    public DateTime Date { get; set; }
    public AttendanceStatus Status { get; set; }
    public string? Remarks { get; set; }
    public string? CourseName { get; set; }
    public string? StudentName { get; set; }
}

public enum AttendanceStatus
{
    Present = 0,
    Absent = 1,
    Late = 2,
    Excused = 3
}
