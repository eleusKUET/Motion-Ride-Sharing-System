using System.Security.Cryptography.X509Certificates;

namespace Motion.Models.Domain
{
    public class Driver
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string Password { get; set; }
        public string Vehicle_type { get; set; }
        public string License_plate { get; set; }
    }
}
