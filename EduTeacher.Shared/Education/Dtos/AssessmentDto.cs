namespace EduTeacher.Shared.Education.Dtos;

public class AssessmentDto
{
    public Guid Id { get; set; }
    public Guid CourseOfferingId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string AssessmentType { get; set; } = string.Empty;
    public DateTime? DueDate { get; set; }
    public double TotalMarks { get; set; }
    public double WeightPercentage { get; set; }
    public bool IsPublished { get; set; }
    public string? CourseName { get; set; }
    public DateTime CreationTime { get; set; }
    public int SubmissionCount { get; set; }
    public int GradedCount { get; set; }
}

public class AssessmentSubmissionDto
{
    public Guid Id { get; set; }
    public Guid AssessmentId { get; set; }
    public Guid StudentId { get; set; }
    public string? StudentName { get; set; }
    public string? SubmissionText { get; set; }
    public string? AttachmentBlobName { get; set; }
    public DateTime SubmittedAt { get; set; }
    public double? Score { get; set; }
    public string? Feedback { get; set; }
    public bool IsGraded { get; set; }
}

public class CreateAssessmentInput
{
    public Guid CourseOfferingId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string AssessmentType { get; set; } = "Assignment";
    public DateTime? DueDate { get; set; }
    public double TotalMarks { get; set; }
    public double WeightPercentage { get; set; }
}
