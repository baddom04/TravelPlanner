using TravelPlanner.Persistor;

namespace TravelPlanner.ViewModel
{
    public class TransportViewModel : BindingSource
    {
        private Transport _selectedTransport;

        public Transport SelectedTransport
        {
            get { return _selectedTransport; }
            set
            {
                if (_selectedTransport != value)
                {
                    _selectedTransport = value;
                    OnPropertyChanged();
                }
            }
        }
        public List<Transport> Transports { get; }
        public DelegateCommand DeleteCommand { get; }

        public TransportViewModel(List<Transport> transports, DelegateCommand deleteCommand)
        {
            Transports = transports;
            DeleteCommand = deleteCommand;
            SelectedTransport = Transports[0];   
        }
    }
}
