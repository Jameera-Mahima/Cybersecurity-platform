namespace CSPlatform.Models.Domain
{
    public class Course
    {
        public string? UserGeneratedId { get; set; }
        public string? CourseId { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public string? Instructor { get; set; }
        public string? CourseDuration { get; set; }
    }
}
