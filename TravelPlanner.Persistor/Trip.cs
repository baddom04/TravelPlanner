namespace TravelPlanner.Persistor
{
    public class Trip
    {
        public string Destination { get; set; } = string.Empty;
        public string Activities { get; set; } = string.Empty;
        public IEnumerable<Transport> Transports { get; set; } = [];
        public IEnumerable<Item> Items { get; set; } = [];
        public DateTime Begin { get; set; } = DateTime.Now;
        public DateTime End { get; set; } = DateTime.Now;
    }
}
