using Rootfly.Mobile.Core.Common.Results;
using Rootfly.Mobile.Core.Networking.REST;
using EduTeacher.Shared.Education.Dtos;
using Volo.Abp.DependencyInjection;

namespace EduTeacher.Shared.Education.Services;

public class TeacherEducationApiService : ITeacherEducationApiService, ISingletonDependency
{
    private readonly IApiClient _api;

    public TeacherEducationApiService(IApiClient apiClient) => _api = apiClient;

    public Task<ApiResult<InstructorDto>> GetCurrentInstructorAsync()
        => _api.GetAsync<InstructorDto>("api/education/instructor/by-user");

    public Task<ApiResult<AbpPagedResultDto<TimetableEntryDto>>> GetMyScheduleAsync(Guid instructorId, Guid? termId = null, int maxResultCount = 50)
    {
        var url = $"api/education/timetable-entry?InstructorId={instructorId}&MaxResultCount={maxResultCount}";
        if (termId.HasValue) url += $"&TermId={termId}";
        return _api.GetAsync<AbpPagedResultDto<TimetableEntryDto>>(url);
    }

    public Task<ApiResult<AbpPagedResultDto<SectionDto>>> GetMySectionsAsync(Guid instructorId, int maxResultCount = 20)
        => _api.GetAsync<AbpPagedResultDto<SectionDto>>($"api/education/section?InstructorId={instructorId}&MaxResultCount={maxResultCount}");

    public Task<ApiResult<AbpPagedResultDto<StudentDto>>> GetSectionStudentsAsync(Guid sectionId, int skipCount = 0, int maxResultCount = 50)
        => _api.GetAsync<AbpPagedResultDto<StudentDto>>($"api/education/student?SectionId={sectionId}&SkipCount={skipCount}&MaxResultCount={maxResultCount}");

    public Task<ApiResult<object>> BulkMarkAttendanceAsync(BulkAttendanceInput input)
        => _api.PostAsync<object>("api/education/attendance-record/bulk-mark", input);

    public Task<ApiResult<SectionAttendanceSummaryDto>> GetSectionAttendanceSummaryAsync(Guid sectionId, Guid? termId = null)
    {
        var url = $"api/education/attendance-record/section-summary?SectionId={sectionId}";
        if (termId.HasValue) url += $"&TermId={termId}";
        return _api.GetAsync<SectionAttendanceSummaryDto>(url);
    }

    public Task<ApiResult<AssessmentDto>> CreateAssessmentAsync(CreateAssessmentInput input)
        => _api.PostAsync<AssessmentDto>("api/education/assessment", input);

    public Task<ApiResult<AbpPagedResultDto<AssessmentDto>>> GetMyAssessmentsAsync(Guid? courseOfferingId = null, int skipCount = 0, int maxResultCount = 20)
    {
        var url = $"api/education/assessment?SkipCount={skipCount}&MaxResultCount={maxResultCount}";
        if (courseOfferingId.HasValue) url += $"&CourseOfferingId={courseOfferingId}";
        return _api.GetAsync<AbpPagedResultDto<AssessmentDto>>(url);
    }

    public Task<ApiResult<AbpPagedResultDto<AssessmentSubmissionDto>>> GetSubmissionsAsync(Guid assessmentId, int skipCount = 0, int maxResultCount = 50)
        => _api.GetAsync<AbpPagedResultDto<AssessmentSubmissionDto>>($"api/education/assessment-submission?AssessmentId={assessmentId}&SkipCount={skipCount}&MaxResultCount={maxResultCount}");

    public Task<ApiResult<AssessmentSubmissionDto>> GradeSubmissionAsync(Guid submissionId, double score, string? feedback)
        => _api.PutAsync<AssessmentSubmissionDto>($"api/education/assessment-submission/{submissionId}/grade", new { score, feedback });

    public Task<ApiResult<AbpPagedResultDto<StudentCourseGradeDto>>> GetCourseGradesAsync(Guid courseOfferingId, int skipCount = 0, int maxResultCount = 50)
        => _api.GetAsync<AbpPagedResultDto<StudentCourseGradeDto>>($"api/education/student-course-grade?CourseOfferingId={courseOfferingId}&SkipCount={skipCount}&MaxResultCount={maxResultCount}");

    public Task<ApiResult<StudentCourseGradeDto>> UpdateGradeAsync(Guid id, UpdateGradeInput input)
        => _api.PutAsync<StudentCourseGradeDto>($"api/education/student-course-grade/{id}", input);

    public Task<ApiResult<AnnouncementDto>> CreateAnnouncementAsync(CreateAnnouncementInput input)
        => _api.PostAsync<AnnouncementDto>("api/education/announcement", input);

    public Task<ApiResult<object>> PublishAnnouncementAsync(Guid id)
        => _api.PostAsync<object>($"api/education/announcement/{id}/publish", new { });

    public Task<ApiResult<CourseMaterialDto>> UploadMaterialAsync(Guid courseOfferingId, string title, string blobName)
        => _api.PostAsync<CourseMaterialDto>("api/education/course-material", new { courseOfferingId, title, blobName });

    public Task<ApiResult<AbpPagedResultDto<AnnouncementDto>>> GetAnnouncementsAsync(int skipCount = 0, int maxResultCount = 20)
        => _api.GetAsync<AbpPagedResultDto<AnnouncementDto>>($"api/education/announcement?SkipCount={skipCount}&MaxResultCount={maxResultCount}");

    public Task<ApiResult<NotificationPreferenceDto>> GetNotificationPreferencesAsync()
        => _api.GetAsync<NotificationPreferenceDto>("api/education/notification-preference/my");

    public Task<ApiResult<NotificationPreferenceDto>> UpdateNotificationPreferencesAsync(NotificationPreferenceDto preferences)
        => _api.PutAsync<NotificationPreferenceDto>("api/education/notification-preference/my", preferences);
}
