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
        private ServiceHost validationServiceHost;

        public MainWindow()
        {
            this.InitializeComponent();
            this.gameServiceHost = new ServiceHost(typeof(GameService));
            this.matchServiceHost = new ServiceHost(typeof(MatchService));
            this.playerServiceHost = new ServiceHost(typeof(PlayerService));
            this.seriesServiceHost = new ServiceHost(typeof(SeriesService));
            this.teamServiceHost = new ServiceHost(typeof(TeamService));
            this.validationServiceHost = new ServiceHost(typeof(ValidationService));
        }

        private void StartService_OnClick(object sender, RoutedEventArgs e)
        {
            var state = this.gameServiceHost.State;

            if (this.gameServiceHost.State == CommunicationState.Created )
            {
                this.OpenClosedCheck.Text = "";
                this.gameServiceHost.Open();
                this.matchServiceHost.Open();
                this.playerServiceHost.Open();
                this.seriesServiceHost.Open();
                this.teamServiceHost.Open();
                this.validationServiceHost.Open();
                this.StatusTextBlock.Text = $"Service started at {DateTime.Now} ";
                this.StatusImage.Source = new BitmapImage(new Uri(@"Resources/correct.png", UriKind.RelativeOrAbsolute));
            }
            else if (this.gameServiceHost.State == CommunicationState.Closed)
            {
                this.gameServiceHost = new ServiceHost(typeof(GameService));
                this.matchServiceHost = new ServiceHost(typeof(MatchService));
                this.playerServiceHost = new ServiceHost(typeof(PlayerService));
                this.seriesServiceHost = new ServiceHost(typeof(SeriesService));
                this.teamServiceHost = new ServiceHost(typeof(TeamService));
                this.validationServiceHost = new ServiceHost(typeof(ValidationService));

                this.OpenClosedCheck.Text = "";
                this.gameServiceHost.Open();
                this.matchServiceHost.Open();
                this.playerServiceHost.Open();
                this.seriesServiceHost.Open();
                this.teamServiceHost.Open();
                this.validationServiceHost.Open();
                this.StatusTextBlock.Text = $"Service started at {DateTime.Now} ";
                this.StatusImage.Source = new BitmapImage(new Uri(@"Resources/correct.png", UriKind.RelativeOrAbsolute));
            }
            else
            {
                this.OpenClosedCheck.Text = "Already started.";
            }

        }

    

        private void StopService_OnClick(object sender, RoutedEventArgs e)
        {
            if (this.gameServiceHost.State == CommunicationState.Opened)
            {
                this.OpenClosedCheck.Text = "";
                this.gameServiceHost.Close();
                this.matchServiceHost.Close();
                this.playerServiceHost.Close();
                this.seriesServiceHost.Close();
                this.teamServiceHost.Close();
                this.validationServiceHost.Close();
                this.StatusTextBlock.Text = $"Service stopped at {DateTime.Now} ";
                this.StatusImage.Source = new BitmapImage(new Uri(@"Resources/cancel.png", UriKind.RelativeOrAbsolute));
            }
            else 
            {
                this.OpenClosedCheck.Text = "Already stopped.";
                
            }
            
        }
    
    }
}