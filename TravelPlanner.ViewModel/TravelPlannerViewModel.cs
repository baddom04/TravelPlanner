using System.Collections.ObjectModel;
using TravelPlanner.Model;
using TravelPlanner.Persistor;

namespace TravelPlanner.ViewModel
{
    public class TravelPlannerViewModel : BindingSource
    {
        #region Fields

        private readonly TravelPlannerModel _model;
        private ObservableCollection<TripViewModel> _trips = [];
        private TripViewModel? _selectedTrip;

        #endregion

        #region Properties

        public ObservableCollection<TripViewModel> Trips
        {
            get { return _trips; }
            private set
            {
                if (_trips != value)
                {
                    _trips = value;
                    OnPropertyChanged();
                }
            }
        }

        public TripViewModel? SelectedTrip
        {
            get { return _selectedTrip; }
            set
            {
                if(value is null)
                {
                    _selectedTrip = value;
                    OnPropertyChanged();
                    return;
                }
                _selectedTrip = value;
                OnSelectionChanged();
            }
        }

        public NewTripFormPageViewModel NewTripFormPageViewModel { get; }
        public TripDetailsPageViewModel? TripDetailsPageViewModel { get; set; }

        #endregion

        #region Commands
        public DelegateCommand AddNewItemCommand { get; }

        #endregion

        #region Events
        public event EventHandler? AddNewItemEvent;
        public event EventHandler? TripDetailsPageEvent;
        public event EventHandler? GoBackOne;
        public event EventHandler? CheckItemListEvent;
        #endregion
        public TravelPlannerViewModel(TravelPlannerModel model)
        {
            _model = model;
            _trips = [.. model.Trips.Select(trip => new TripViewModel(trip))];
            model.TripsUpdated += (s, e) => Trips = [.. _model.Trips.Select(trip => new TripViewModel(trip))];

            AddNewItemCommand = new DelegateCommand(AddNewItem);

            NewTripFormPageViewModel = new(_model);

            NewTripFormPageViewModel.GoBackOne += OnGoBackOne;
            NewTripFormPageViewModel.ItemListViewModel.GoBackOne += OnGoBackOne;
        }
        #region Command Methods
        private void AddNewItem(object? obj)
        {
            NewTripFormPageViewModel.InitializeForm();
            OnAddNewItem();
        }

        #endregion

        #region Private Methods
        private void OnAddNewItem()
        {
            AddNewItemEvent?.Invoke(this, EventArgs.Empty);
        }
        private void DeleteTrip(Trip trip)
        {
            _model.RemoveTrip(trip);
        }

        private void OnGoBackOne(object? sender, EventArgs e)
        {
            GoBackOne?.Invoke(sender, e);
        }
        private void OnCheckItemListEvent(object? sender, EventArgs e)
        {
            CheckItemListEvent?.Invoke(sender, e);
        }

        private void OnSelectionChanged()
        {
            if (TripDetailsPageViewModel is not null)
            {
                TripDetailsPageViewModel.GoBackOne -= OnGoBackOne;
                TripDetailsPageViewModel.GoToCheckItemListEvent -= OnCheckItemListEvent;
            }

            TripDetailsPageViewModel = new(SelectedTrip!, DeleteTrip);

            TripDetailsPageViewModel.GoBackOne += OnGoBackOne;
            TripDetailsPageViewModel.GoToCheckItemListEvent += OnCheckItemListEvent;

            TripDetailsPageEvent?.Invoke(this, EventArgs.Empty);
            SelectedTrip = null;
        }
        #endregion
    }
}
