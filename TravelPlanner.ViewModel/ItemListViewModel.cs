﻿using System.Collections.ObjectModel;

namespace TravelPlanner.ViewModel
{
    public class ItemListViewModel
    {
        public ObservableCollection<ItemViewModel> Items { get; set; } = [];
        public DelegateCommand GoBackOneCommand { get; }
        public DelegateCommand SaveTripCommand { get; }

        public event EventHandler? GoBackOne;
        public event EventHandler? SaveTrip;
        public ItemListViewModel()
        {
            ItemViewModel itemViewModel = new();
            itemViewModel.NameChanged += LastItemViewModel_NameChanged;
            Items.Add(itemViewModel);

            GoBackOneCommand = new DelegateCommand(GoBackOne_Command);
            SaveTripCommand = new DelegateCommand(SaveTrip_Command);
        }
        private void GoBackOne_Command(object? obj)
        {
            OnBackToNewTripForm();
        }
        private void SaveTrip_Command(object? obj)
        {
            OnSaveTrip();
        }
        private void OnBackToNewTripForm()
        {
            GoBackOne?.Invoke(this, new EventArgs());
        }
        private void OnSaveTrip()
        {
            SaveTrip?.Invoke(this, new EventArgs());
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
