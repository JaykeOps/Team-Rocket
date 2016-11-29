using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Domain.Services;

namespace ServiceHostRunner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ServiceHost gameServiceHost;
        private ServiceHost matchServiceHost;
        private ServiceHost playerServiceHost;
        private ServiceHost seriesServiceHost;
        private ServiceHost teamServiceHost;

        public MainWindow()
        {
            InitializeComponent();
            new ServiceHost(typeof(GameService));
            new ServiceHost(typeof(MatchService));
            new ServiceHost(typeof(PlayerService));
            new ServiceHost(typeof(SeriesService));
            new ServiceHost(typeof(TeamService));
        }

        private void StartService_OnClick(object sender, RoutedEventArgs e)
        {
            gameServiceHost.Open();
            matchServiceHost.Open();
            playerServiceHost.Open();
            seriesServiceHost.Open();
            teamServiceHost.Open();
            StatusTextBlock.Text = $"Service started at {DateTime.Now}.";
        }

        private void StopService_OnClick(object sender, RoutedEventArgs e)
        {
            gameServiceHost.Close();
            matchServiceHost.Close();
            playerServiceHost.Close();
            seriesServiceHost.Close();
            teamServiceHost.Close();
            StatusTextBlock.Text = $"Service stopped at {DateTime.Now}.";
        }
    }
}
