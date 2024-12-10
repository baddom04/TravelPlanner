using System.Collections.ObjectModel;

namespace TravelPlanner.ViewModel
{
    public class CheckItemListViewModel
    {
        public ObservableCollection<ItemViewModel> ItemsViewModels { get; }

        public event EventHandler? GoBackOne;
        public DelegateCommand GoBackOneCommand { get; }
        public CheckItemListViewModel(IEnumerable<ItemViewModel> items)
        {
            ItemsViewModels = [.. items];
            GoBackOneCommand = new(GoBackOne_Command);
        }
        private void GoBackOne_Command(object? obj)
        {
            OnGoBackOne();
        }
        private void OnGoBackOne()
        {
            GoBackOne?.Invoke(this, EventArgs.Empty);
        }
    }
}
