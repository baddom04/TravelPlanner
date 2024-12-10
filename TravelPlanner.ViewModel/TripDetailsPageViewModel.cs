using TravelPlanner.Persistor;

namespace TravelPlanner.ViewModel
{
    public class TripDetailsPageViewModel
    {
        private readonly Action<Trip> _deleteTrip;
        public TripViewModel TripViewModel { get; }

        public DelegateCommand DeleteThisTripCommand { get; }
        public DelegateCommand CheckItemListCommand { get; }
        public DelegateCommand GoBackOneCommand { get; }
        public CheckItemListViewModel CheckItemListViewModel { get; }

        public event EventHandler? GoToCheckItemListEvent;
        public event EventHandler? GoBackOne;

        public TripDetailsPageViewModel(TripViewModel trip, Action<Trip> deleteTrip)
        {
            TripViewModel = trip;
            _deleteTrip = deleteTrip;
            DeleteThisTripCommand = new DelegateCommand(DeleteThisTrip_Command);
            CheckItemListCommand = new DelegateCommand(CheckItemList_Command);
            GoBackOneCommand = new DelegateCommand(GoBackOne_Command);

            CheckItemListViewModel = new(trip.Items);
            CheckItemListViewModel.GoBackOne += CheckItemListViewModel_GoBackOne;
        }

        private void CheckItemListViewModel_GoBackOne(object? sender, EventArgs e)
        {
            GoBackOne?.Invoke(sender, e);
        }

        private void GoBackOne_Command(object? obj)
        {
            OnGoBackOne();
        }
        private void DeleteThisTrip_Command(object? obj)
        {
            _deleteTrip(TripViewModel.Trip);
            OnGoBackOne();
        }
        private void CheckItemList_Command(object? obj)
        {
            OnGoToCheckItemList();
        }
        private void OnGoToCheckItemList()
        {
            GoToCheckItemListEvent?.Invoke(this, EventArgs.Empty);
        }
        private void OnGoBackOne()
        {
            GoBackOne?.Invoke(this, EventArgs.Empty);
        }
    }
}
