using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using FootballManager.Admin.Utility;
using FootballManager.Admin.View;

namespace FootballManager.Admin.ViewModel
{
    public class SeriesScheduleViewModel : ViewModelBase
    {
        private ICommand openSeriesGameProtocolViewCommand;

        public ICommand OpenSeriesGameProtocolViewCommand
        {
            get
            {
                if (openSeriesGameProtocolViewCommand == null)
                {
                    openSeriesGameProtocolViewCommand = new RelayCommand(OpenSeriesGameProtocolView);
                }
                return openSeriesGameProtocolViewCommand;
            }
        }

        private void OpenSeriesGameProtocolView(object obj)
        {
            var view = new SeriesGameProtocolView();
            view.ShowDialog();
        }

        private ICommand openSeriesScheduleEditViewCommand;

        public ICommand OpenSeriesScheduleEditViewCommand
        {
            get
            {
                if (openSeriesScheduleEditViewCommand == null)
                {
                    openSeriesScheduleEditViewCommand = new RelayCommand(OpenSeriesScheduleEditView);
                }
                return openSeriesScheduleEditViewCommand;
            }
        }

        private void OpenSeriesScheduleEditView(object obj)
        {
            var view = new SeriesScheduleEditView();
            view.ShowDialog();
        }
    }
}
