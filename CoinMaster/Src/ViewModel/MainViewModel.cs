using CoinMaster.Src.Core;

namespace CoinMaster.Src.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        public RelayCommand HomeScreenCommand { get; set; }
        
        public HomeScreenViewModel HomeScreenViewModel { get; set; }
        
        private object _currentView;
        public object CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            HomeScreenViewModel = new HomeScreenViewModel();
            CurrentView = HomeScreenViewModel;

            HomeScreenCommand = new RelayCommand(_ => CurrentView = HomeScreenViewModel);
        }
    }
}