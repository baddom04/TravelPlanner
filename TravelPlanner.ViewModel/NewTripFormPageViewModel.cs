using System.Collections.ObjectModel;
using TravelPlanner.Model;
using TravelPlanner.Persistor;

namespace TravelPlanner.ViewModel
{
    public class NewTripFormPageViewModel : BindingSource
    {
        private readonly TravelPlannerModel _model;
        public List<Transport> Transports { get; }
        public string Destination { get; set; } = string.Empty;
        public string Activities { get; set; } = string.Empty;
        public DateTime Beginning { get; set; } = DateTime.Now;
        public DateTime Ending { get; set; } = DateTime.Now;
        public ObservableCollection<TransportViewModel> SelectedTransports { get; set; } = [];
        public DelegateCommand AddTransportCommand { get; }
        public DelegateCommand NextPageCommand { get; }
        public DelegateCommand GoBackOneCommand { get; }
        public ItemListViewModel ItemListViewModel { get; }

        public event EventHandler? GoBackOne;
        public event EventHandler? ItemListPage;
        public event EventHandler? BackToMainPageEvent;

        private string? _errorMessage;
        public string? ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                if (_errorMessage != value)
                {
                    _errorMessage = value;
                    OnPropertyChanged();
                }
            }
        }

        public NewTripFormPageViewModel(TravelPlannerModel model)
        {
            _model = model;
            Transports = Enum.GetValues(typeof(Transport)).Cast<Transport>().ToList();
            AddTransportCommand = new DelegateCommand(AddSelectedTransportCommand);
            NextPageCommand = new DelegateCommand(TurnNextPageCommand);
            GoBackOneCommand = new DelegateCommand(GoBackOne_Command);

            ItemListViewModel = new();
            ItemListViewModel.SaveTrip += ItemListViewModel_SaveTrip;

            InitializeForm();
        }
        public void InitializeForm()
        {
            SelectedTransports.Clear();
            AddSelectedTransportCommand();
            Destination = string.Empty;
            Activities = string.Empty;
            Beginning = DateTime.Now;
            Ending = DateTime.Now;
            ErrorMessage = null;
            ItemListViewModel.InitializeList();
        }
        private void ItemListViewModel_SaveTrip(object? sender, EventArgs e)
        {
            Trip newTrip = new(
                Destination,
                Activities,
                SelectedTransports.Select(transport => transport.SelectedTransport),
                ItemListViewModel.Items.Where(item => !string.IsNullOrWhiteSpace(item.Name) && !string.IsNullOrWhiteSpace(item.Quantity)).Select(item => new Item(item.Name, false, Int32.Parse(item.Quantity))),
                Beginning,
                Ending);
            _model.AddTrip(newTrip);
            OnBackToMainPage();
        }
        private void OnBackToMainPage()
        {
            BackToMainPageEvent?.Invoke(this, EventArgs.Empty);
        }
        private void GoBackOne_Command(object? obj)
        {
            OnGoBackOne();
        }
        private void OnGoBackOne()
        {
            GoBackOne?.Invoke(this, EventArgs.Empty);
        }
        private void OnItemListPage()
        {
            ItemListPage?.Invoke(this, EventArgs.Empty);
        }
        private void DeleteSelectedTransportCommand(object? obj)
        {
            if (obj is not TransportViewModel transport) return;

            SelectedTransports.Remove(transport);
        }
        private void AddSelectedTransportCommand(object? obj = null)
        {
            SelectedTransports.Add(new TransportViewModel(Transports, new DelegateCommand(DeleteSelectedTransportCommand)));
        }
        private void TurnNextPageCommand(object? obj)
        {
            Validate();

            if (ErrorMessage != null) return;

            OnItemListPage();
        }
        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(Destination))
                ErrorMessage = "The destination cannot be empty!";
            else if (Ending < Beginning)
                ErrorMessage = "The ending cannot be earlier than the beginning!";
            else if (!SelectedTransports.Any())
                ErrorMessage = "I don't think that you travel by nothing🤨";
            else
                ErrorMessage = null;
        }
    }
}
