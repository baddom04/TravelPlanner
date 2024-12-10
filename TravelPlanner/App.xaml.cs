using TravelPlanner.ViewModel;
using TravelPlanner.Model;

namespace TravelPlanner
{
    public partial class App : Application
    {
        #region Fields
        private readonly NavigationPage _rootPage;

        private readonly TravelPlannerViewModel _viewModel;
        private readonly TravelPlannerModel _model;
        #endregion
        public App()
        {
            InitializeComponent();

            _model = new();
            _viewModel = new(_model);
            _viewModel.AddNewItemEvent += _viewModel_AddNewItemEvent;
            _viewModel.GoBackOne += _viewModel_GoBackOne;
            _viewModel.NewTripFormPageViewModel.ItemListPage += NewTripFormPageViewModel_ItemListPageEvent;
            _viewModel.NewTripFormPageViewModel.BackToMainPageEvent += _viewModel_BackToMainPageEvent;
            _viewModel.TripDetailsPageEvent += _viewModel_TripDetailsPageEvent;
            _viewModel.CheckItemListEvent += _viewModel_CheckItemListEvent;
            _rootPage = new(new MainPage()) { BindingContext = _viewModel };

            MainPage = _rootPage;
        }

        private async void _viewModel_CheckItemListEvent(object? sender, EventArgs e)
        {
            if (_rootPage.CurrentPage is not CheckItemList)
                await _rootPage.Navigation.PushAsync(new CheckItemList());
        }

        private async void _viewModel_GoBackOne(object? sender, EventArgs e)
        {
            await _rootPage.Navigation.PopAsync();
        }
        private async void _viewModel_TripDetailsPageEvent(object? sender, EventArgs e)
        {
            if (_rootPage.CurrentPage is not TripDetailsPage)
                await _rootPage.Navigation.PushAsync(new TripDetailsPage());
        }

        private async void _viewModel_BackToMainPageEvent(object? sender, EventArgs e)
        {
            if (_rootPage.CurrentPage is not TravelPlanner.MainPage)
                await _rootPage.Navigation.PopToRootAsync();
        }
        private async void ItemListViewModel_BackToNewTripFormEvent(object? sender, EventArgs e)
        {
            if (_rootPage.CurrentPage is not NewTripFormPage)
                await _rootPage.Navigation.PopAsync();
        }
        private async void _viewModel_AddNewItemEvent(object? sender, EventArgs e)
        {
            if (_rootPage.CurrentPage is not NewTripFormPage)
                await _rootPage.Navigation.PushAsync(new NewTripFormPage());
        }
        private async void NewTripFormPageViewModel_BackToMainPageEvent(object? sender, EventArgs e)
        {
            if(_rootPage.CurrentPage is not TravelPlanner.MainPage)
                await _rootPage.Navigation.PopAsync();
        }
        private async void NewTripFormPageViewModel_ItemListPageEvent(object? sender, EventArgs e)
        {
            if (_rootPage.CurrentPage is not ItemList)
                await _rootPage.Navigation.PushAsync(new ItemList());
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            Window window = base.CreateWindow(activationState);

            window.Created += Window_Created;
            window.Deactivated += Window_Deactivated;
            window.Destroying += Window_Destroying;

            return window;
        }

        private async void Window_Created(object? sender, EventArgs e) => await _model.LoadStateAsync();
        private async void Window_Deactivated(object? sender, EventArgs e) => await _model.SaveStateAsync();
        private async void Window_Destroying(object? sender, EventArgs e) => await _model.SaveStateAsync();
    }
}
