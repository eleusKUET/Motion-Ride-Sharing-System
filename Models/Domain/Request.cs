namespace Motion.Models.Domain
{
    public class Request
    {
        public Guid RequestId { get; set; }
        public Guid Rid { get; set; }
        public Guid Did { get; set; }
        public double SLatitude { get; set; }
        public double SLongitude { get; set; }
        public double ELatitude { get; set; }
        public double ELongitude { get; set; }
        public string Status { get; set; }
    }
}
