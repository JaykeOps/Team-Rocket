using System.Windows.Input;
using FootballManager.App.Utility;
using FootballManager.App.View;

namespace FootballManager.App.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public ICommand OpenPlayerCommand { get; set; }
        public ICommand OpenTeamCommand { get; set; }

        private object selectedViewModel;

        public object SelectedViewModel
        {
            get { return selectedViewModel; }
            set
            {
                selectedViewModel = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            OpenPlayerCommand = new RelayCommand(PlayerCommand);
            OpenTeamCommand = new RelayCommand(TeamCommand);
        }

        private void PlayerCommand(object obj)
        {
            SelectedViewModel = new PlayerView();
        }
        private void TeamCommand(object obj)
        {
            SelectedViewModel = new TeamView();
        }


    }
}
