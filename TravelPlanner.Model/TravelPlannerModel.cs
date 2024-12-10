using TravelPlanner.Persistor;

namespace TravelPlanner.Model
{
    public class TravelPlannerModel
    {
        private readonly TravelPlannerPersistor _persistor;
        private List<Trip> _trips;

        public List<Item> Items { get; private set; }

        public event EventHandler? ItemsUpdated;
        public event EventHandler? TripsUpdated;

        public TravelPlannerModel()
        {
            _persistor = new TravelPlannerPersistor();
            _trips = [];
            Items = [];
        }
        public async Task LoadStateAsync()
        {
            AppState? trips = await _persistor.Load();

            if (trips is null) return;

            _trips = trips.Trips;
            OnTripsUpdated();

            Items = trips.Items;
            OnItemsUpdated();
        }
        public async Task SaveStateAsync()
        {
            await _persistor.Save(new AppState(_trips, Items));
        }
        public List<Trip> Trips => [.. _trips];
        public void AddTrip(Trip trip)
        {
            _trips.Add(trip);
            OnTripsUpdated();
        }
        public void RemoveTrip(Trip trip)
        {
            _trips.Remove(trip);
            OnTripsUpdated();
        }
        private void OnTripsUpdated() => TripsUpdated?.Invoke(this, new EventArgs());
        private void OnItemsUpdated() => ItemsUpdated?.Invoke(this, new EventArgs());
    }
}
