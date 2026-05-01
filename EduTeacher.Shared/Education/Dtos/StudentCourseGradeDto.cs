namespace EduTeacher.Shared.Education.Dtos;

public class StudentCourseGradeDto
{
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public Guid CourseOfferingId { get; set; }
    public string? StudentName { get; set; }
    public string? CourseName { get; set; }
    public double? Score { get; set; }
    public string? Grade { get; set; }
    public double? GradePoints { get; set; }
    public bool IsPublished { get; set; }
    public string? Remarks { get; set; }
}

public class UpdateGradeInput
{
    public Guid StudentId { get; set; }
    public Guid CourseOfferingId { get; set; }
    public double? Score { get; set; }
    public string? Grade { get; set; }
    public double? GradePoints { get; set; }
    public string? Remarks { get; set; }
}
