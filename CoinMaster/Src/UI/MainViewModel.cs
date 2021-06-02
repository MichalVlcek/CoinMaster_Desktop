using CoinMaster.Src.Core;

namespace CoinMaster.Src.UI
{
    public class MainViewModel : ObservableObject
    {
        private object _currentView;

        private object CurrentView
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
            
        }
    }
}