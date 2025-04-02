namespace CSPlatform.Models.Domain
{
    public class Enrollment
    {
        public string? EnrollmentId { get; set; }
        public string? UserId { get; set; }
        public string? CourseId { get; set; }
        public DateTime? EnrollmentDate { get; set; }
        public string? EnrollmentStatus { get; set; }
    }
}
