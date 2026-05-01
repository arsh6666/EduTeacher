namespace EduTeacher.Shared.Education.Dtos;

public class CourseMaterialDto
{
    public Guid Id { get; set; }
    public Guid CourseOfferingId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? BlobName { get; set; }
    public string? FileUrl { get; set; }
    public string? FileType { get; set; }
    public DateTime CreationTime { get; set; }
}
