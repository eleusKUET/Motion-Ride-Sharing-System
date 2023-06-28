namespace Motion.Models.Domain
{
    public class DLocation
    {
        public DLocation() { }
        public Guid Id { get; set; }
        public double lat { get; set; }
        public double lng { get; set; }
        public string Status { get; set; }
        public string Vehicle { get; set; }
    }
}
