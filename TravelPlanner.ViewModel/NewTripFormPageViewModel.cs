using System.Collections.ObjectModel;
using TravelPlanner.Persistor;

namespace TravelPlanner.ViewModel
{
    public class NewTripFormPageViewModel : BindingSource
    {
        public List<Transport> Transports { get; }
        public string Destination { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime Beginning { get; set; } = DateTime.Now;
        public DateTime Ending { get; set; } = DateTime.Now;
        public ObservableCollection<TransportViewModel> SelectedTransports { get; set; } = [];
        public DelegateCommand AddTransportCommand { get; }
        public DelegateCommand NextPageCommand { get; }

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

        public NewTripFormPageViewModel()
        {
            Transports = Enum.GetValues(typeof(Transport)).Cast<Transport>().ToList();
            AddTransportCommand = new DelegateCommand(AddSelectedTransportCommand);
            NextPageCommand = new DelegateCommand(Validate);

            InitializeForm();
        }
        private void InitializeForm()
        {
            SelectedTransports.Clear();
            AddSelectedTransportCommand();
            Destination = string.Empty;
            Description = string.Empty;
            Beginning = DateTime.Now;
            Ending = DateTime.Now;
            ErrorMessage = null;
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
        private void Validate(object? obj = null)
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
