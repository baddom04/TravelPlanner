using System.Collections.ObjectModel;
using TravelPlanner.Model;
using TravelPlanner.Persistor;

namespace TravelPlanner.ViewModel
{
    public class TravelPlannerViewModel : BindingSource
    {
        #region Fields

        private readonly TravelPlannerModel _model;
        private ObservableCollection<Trip> _trips = [];
        private ObservableCollection<Item> _items = [];

        #endregion

        #region Properties

        public ObservableCollection<Trip> Trips
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

        public ObservableCollection<Item> Items
        {
            get { return _items; }
            private set
            {
                if (_items != value)
                {
                    _items = value;
                    OnPropertyChanged();
                }
            }
        }

        public NewTripFormPageViewModel NewTripFormPageViewModel { get; }

        #endregion

        #region Commands
        public DelegateCommand AddNewItemCommand { get; }
        #endregion

        #region Events
        public event EventHandler? AddNewItemEvent;
        #endregion
        public TravelPlannerViewModel(TravelPlannerModel model)
        {
            _model = model;
            _trips = [.. model.Trips];
            model.TripsUpdated += (s, e) => Trips = [.. _model.Trips];

            _items = [.. model.Items];
            model.ItemsUpdated += (s, e) => Items = [.. _model.Items];

            AddNewItemCommand = new DelegateCommand(AddNewItem);
            NewTripFormPageViewModel = new();
        }

        #region Command Methods
        private void AddNewItem(object? obj)
        {
            OnAddNewItem();
        }
        #endregion

        #region Private Methods
        private void OnAddNewItem()
        {
            AddNewItemEvent?.Invoke(this, EventArgs.Empty);
        }
        #endregion
    }
}
