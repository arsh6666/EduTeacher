namespace EduTeacher.Shared.Education.Dtos;

public class NotificationPreferenceDto
{
    public Guid Id { get; set; }
    public bool AttendanceAlerts { get; set; } = true;
    public bool GradeAlerts { get; set; } = true;
    public bool FeeAlerts { get; set; } = true;
    public bool AssignmentAlerts { get; set; } = true;
    public bool AnnouncementAlerts { get; set; } = true;
    public bool ChatAlerts { get; set; } = true;
    public bool ScheduleAlerts { get; set; } = true;
}
