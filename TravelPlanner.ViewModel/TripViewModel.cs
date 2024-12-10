using System.Collections.ObjectModel;
using TravelPlanner.Persistor;

namespace TravelPlanner.ViewModel
{
    public class TripViewModel
    {
        private readonly Trip _trip;
        public Trip Trip => _trip;
        public string Destination { get; set; }
        public string Activities { get; set; }
        public IEnumerable<Transport> Transports { get; set; }
        public ObservableCollection<ItemViewModel> Items { get; set; }
        public DateTime Begin { get; set; }
        public DateTime End { get; set; }
        public TripViewModel(Trip trip)
        {
            _trip = trip;
            Destination = trip.Destination;
            Activities = trip.Activities;
            Transports = trip.Transports;
            Items = [.. trip.Items.Select(item => new ItemViewModel(item))];
            Begin = trip.Begin;
            End = trip.End;
        }
    }
}
