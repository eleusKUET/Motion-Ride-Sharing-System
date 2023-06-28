namespace Motion.Models.Domain
{
    public class Ride
    {
        public Ride() { }
        public Guid Id { get; set; }
        public Guid Rid { get; set; }
        public Guid Did { get; set; }
        public float SLatitude { get; set; }
        public float SLongitude { get; set; }
        public float ELatitude { get; set; }
        public float ELongitude { get; set; }
        public DateTime Stime { get; set; }
        public DateTime ETime { get; set; }
        public float Distance { get; set; }
        public float Fare { get; set; }
    }
}
