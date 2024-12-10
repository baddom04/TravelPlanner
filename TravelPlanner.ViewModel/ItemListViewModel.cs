using System.Collections.ObjectModel;
using TravelPlanner.Persistor;

namespace TravelPlanner.ViewModel
{
    public class ItemListViewModel
    {
        public ObservableCollection<ItemViewModel> Items { get; set; } = [];
        public ItemListViewModel()
        {
            ItemViewModel itemViewModel = new();
            itemViewModel.NameChanged += LastItemViewModel_NameChanged;
            Items.Add(itemViewModel);
        }
        private void LastItemViewModel_NameChanged(object? sender, EventArgs e)
        {
            AddEmptyItem();
        }
        private void AddEmptyItem()
        {
            Items.Last().NameChanged -= LastItemViewModel_NameChanged;
            ItemViewModel itemViewModel = new();
            itemViewModel.NameChanged += LastItemViewModel_NameChanged;
            Items.Add(itemViewModel);
        }
    }
}
