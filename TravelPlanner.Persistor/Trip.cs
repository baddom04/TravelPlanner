namespace TravelPlanner.Persistor
{
    public class Trip(string destination, string activities, IEnumerable<Transport> transports, IEnumerable<Item> items, DateTime beginning, DateTime ending)
    {
        public string Destination { get; set; } = destination;
        public string Activities { get; set; } = activities;
        public IEnumerable<Transport> Transports { get; set; } = transports;
        public IEnumerable<Item> Items { get; set; } = items;
        public DateTime Begin { get; set; } = beginning;
        public DateTime End { get; set; } = ending;
    }
}
