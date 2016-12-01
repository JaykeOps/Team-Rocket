using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Domain.Entities;
using Domain.Services;

namespace FootballManager.App.View
{
    /// <summary>
    /// Interaction logic for MatchView.xaml
    /// </summary>
    public partial class MatchView : UserControl
    {
        private List<Match> matches;
        private MatchService matchService = new MatchService();

        public MatchView()
        {
            this.InitializeComponent();
            this.matches = matchService.GetAll().ToList();
            DataGrid.ItemsSource = this.matches;
        }
    }
}
