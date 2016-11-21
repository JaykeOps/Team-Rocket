using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Domain.Entities;
using Domain.Services;
using FootballManager.Admin.Extensions;
using FootballManager.Admin.Utility;
using FootballManager.Admin.View;

namespace FootballManager.Admin.ViewModel
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
