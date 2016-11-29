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
            InitializeComponent();
            gameServiceHost = new ServiceHost(typeof(GameService));
            matchServiceHost = new ServiceHost(typeof(MatchService));
            playerServiceHost = new ServiceHost(typeof(PlayerService));
            seriesServiceHost = new ServiceHost(typeof(SeriesService));
            teamServiceHost = new ServiceHost(typeof(TeamService));
            validationServiceHost = new ServiceHost(typeof(ValidationService));
        }

        private void StartService_OnClick(object sender, RoutedEventArgs e)
        {
            var state = gameServiceHost.State;

            if (gameServiceHost.State == CommunicationState.Created )
            {
                OpenClosedCheck.Text = "";
                gameServiceHost.Open();
                matchServiceHost.Open();
                playerServiceHost.Open();
                seriesServiceHost.Open();
                teamServiceHost.Open();
                validationServiceHost.Open();
                StatusTextBlock.Text = $"Service started at {DateTime.Now} ";
                StatusImage.Source = new BitmapImage(new Uri(@"Resources/correct.png", UriKind.RelativeOrAbsolute));
            }
            else if (gameServiceHost.State == CommunicationState.Closed)
            {
                gameServiceHost = new ServiceHost(typeof(GameService));
                matchServiceHost = new ServiceHost(typeof(MatchService));
                playerServiceHost = new ServiceHost(typeof(PlayerService));
                seriesServiceHost = new ServiceHost(typeof(SeriesService));
                teamServiceHost = new ServiceHost(typeof(TeamService));
                validationServiceHost = new ServiceHost(typeof(ValidationService));

                OpenClosedCheck.Text = "";
                gameServiceHost.Open();
                matchServiceHost.Open();
                playerServiceHost.Open();
                seriesServiceHost.Open();
                teamServiceHost.Open();
                validationServiceHost.Open();
                StatusTextBlock.Text = $"Service started at {DateTime.Now} ";
                StatusImage.Source = new BitmapImage(new Uri(@"Resources/correct.png", UriKind.RelativeOrAbsolute));
            }
            else
            {
                OpenClosedCheck.Text = "Already started.";
            }

        }

    

        private void StopService_OnClick(object sender, RoutedEventArgs e)
        {
            if (gameServiceHost.State == CommunicationState.Opened)
            {
                OpenClosedCheck.Text = "";
                gameServiceHost.Close();
                matchServiceHost.Close();
                playerServiceHost.Close();
                seriesServiceHost.Close();
                teamServiceHost.Close();
                validationServiceHost.Close();
                StatusTextBlock.Text = $"Service stopped at {DateTime.Now} ";
                StatusImage.Source = new BitmapImage(new Uri(@"Resources/cancel.png", UriKind.RelativeOrAbsolute));
            }
            else 
            {
                OpenClosedCheck.Text = "Already stopped.";
                
            }
            
        }
    
    }
}