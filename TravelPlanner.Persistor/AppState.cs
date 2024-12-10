namespace TravelPlanner.Persistor
{
    public class AppState(List<Trip> trips, List<Item> items)
    {

        public List<Trip> Trips { get; set; } = trips;
        public List<Item> Items { get; set; } = items;
    }
}
