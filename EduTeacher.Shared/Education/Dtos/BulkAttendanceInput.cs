namespace EduTeacher.Shared.Education.Dtos;

public class BulkAttendanceInput
{
    public DateTime Date { get; set; }
    public Guid SectionId { get; set; }
    public Guid? CourseOfferingId { get; set; }
    public List<AttendanceRecordInput> Records { get; set; } = [];
}

public class AttendanceRecordInput
{
    public Guid StudentId { get; set; }
    public AttendanceStatus Status { get; set; }
    public string? Remarks { get; set; }
}
