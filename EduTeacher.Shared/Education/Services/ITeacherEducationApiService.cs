using Rootfly.Mobile.Core.Common.Results;
using EduTeacher.Shared.Education.Dtos;

namespace EduTeacher.Shared.Education.Services;

public interface ITeacherEducationApiService
{
    // Instructor
    Task<ApiResult<InstructorDto>> GetCurrentInstructorAsync();

    // Schedule
    Task<ApiResult<AbpPagedResultDto<TimetableEntryDto>>> GetMyScheduleAsync(Guid instructorId, Guid? termId = null, int maxResultCount = 50);

    // Sections
    Task<ApiResult<AbpPagedResultDto<SectionDto>>> GetMySectionsAsync(Guid instructorId, int maxResultCount = 20);
    Task<ApiResult<AbpPagedResultDto<StudentDto>>> GetSectionStudentsAsync(Guid sectionId, int skipCount = 0, int maxResultCount = 50);

    // Attendance
    Task<ApiResult<object>> BulkMarkAttendanceAsync(BulkAttendanceInput input);
    Task<ApiResult<SectionAttendanceSummaryDto>> GetSectionAttendanceSummaryAsync(Guid sectionId, Guid? termId = null);

    // Assessments
    Task<ApiResult<AssessmentDto>> CreateAssessmentAsync(CreateAssessmentInput input);
    Task<ApiResult<AbpPagedResultDto<AssessmentDto>>> GetMyAssessmentsAsync(Guid? courseOfferingId = null, int skipCount = 0, int maxResultCount = 20);
    Task<ApiResult<AbpPagedResultDto<AssessmentSubmissionDto>>> GetSubmissionsAsync(Guid assessmentId, int skipCount = 0, int maxResultCount = 50);
    Task<ApiResult<AssessmentSubmissionDto>> GradeSubmissionAsync(Guid submissionId, double score, string? feedback);

    // Grades
    Task<ApiResult<AbpPagedResultDto<StudentCourseGradeDto>>> GetCourseGradesAsync(Guid courseOfferingId, int skipCount = 0, int maxResultCount = 50);
    Task<ApiResult<StudentCourseGradeDto>> UpdateGradeAsync(Guid id, UpdateGradeInput input);

    // Announcements
    Task<ApiResult<AnnouncementDto>> CreateAnnouncementAsync(CreateAnnouncementInput input);
    Task<ApiResult<object>> PublishAnnouncementAsync(Guid id);

    // Materials
    Task<ApiResult<CourseMaterialDto>> UploadMaterialAsync(Guid courseOfferingId, string title, string blobName);

    // Announcement listing
    Task<ApiResult<AbpPagedResultDto<AnnouncementDto>>> GetAnnouncementsAsync(int skipCount = 0, int maxResultCount = 20);

    // Notifications
    Task<ApiResult<NotificationPreferenceDto>> GetNotificationPreferencesAsync();
    Task<ApiResult<NotificationPreferenceDto>> UpdateNotificationPreferencesAsync(NotificationPreferenceDto preferences);
}
