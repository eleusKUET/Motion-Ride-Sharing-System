namespace Motion.Models
{
    public class AddRequestModel
    {
        public Guid rid {  get; set; }
        public Guid did { get; set; }
        public double SLatitude { get; set; }
        public double SLongitude { get; set; }
        public double ELatitude { get; set; }
        public double ELongitude { get; set; }
        public string Status { get; set; }
    }
}
