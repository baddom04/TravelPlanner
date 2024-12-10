using TravelPlanner.Persistor;

namespace TravelPlanner.Model
{
    public class TravelPlannerModel
    {
        private readonly TravelPlannerPersistor _persistor;
        public List<Trip> Trips { get; private set; }
        public List<Item> Items { get; private set; }

        public event EventHandler? ItemsUpdated;
        public event EventHandler? TripsUpdated;

        public TravelPlannerModel()
        {
            _persistor = new TravelPlannerPersistor();
            Trips = [];
            Items = [];
        }
        public async Task LoadStateAsync()
        {
            AppState? trips = await _persistor.Load();

            if (trips is null) return;

            Trips = trips.Trips;
            OnTripsUpdated();

            Items = trips.Items;
            OnItemsUpdated();
        }
        public async Task SaveStateAsync()
        {
            await _persistor.Save(new AppState(Trips, Items));
        }
        private void OnTripsUpdated() => TripsUpdated?.Invoke(this, new EventArgs());
        private void OnItemsUpdated() => ItemsUpdated?.Invoke(this, new EventArgs());
    }
}
