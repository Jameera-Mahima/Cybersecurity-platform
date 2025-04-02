namespace CSPlatform.Models.Domain
{
    public class User
    {
        public string? UserId { get; set; }
        public required string UserName { get; set; }    
        public string? Email { get; set; }
        public required string Password { get; set; }
        public required string UserType { get; set;}
        public string? PhoneNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? CountryRegion { get; set; }
    }
}
