namespace EduTeacher.Shared.Education.Dtos;

public class SectionAttendanceSummaryDto
{
    public Guid SectionId { get; set; }
    public string SectionName { get; set; } = string.Empty;
    public int TotalStudents { get; set; }
    public int PresentCount { get; set; }
    public int AbsentCount { get; set; }
    public int LateCount { get; set; }
    public int ExcusedCount { get; set; }
    public double AttendancePercentage { get; set; }
    public DateTime Date { get; set; }
}
