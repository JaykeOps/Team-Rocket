using FootballManager.App.ViewModel;

namespace FootballManager.App
{
    public class ViewModelLocator
    {
        public static MainViewModel MainViewModel
        {
            get { return new MainViewModel(); }
        }

        public static PlayerViewModel PlayerViewModel
        {
            get { return new PlayerViewModel(); }
        }

        public static PlayerAddViewModel PlayerAddViewModel
        {
            get { return new PlayerAddViewModel(); }
        }

        public static TeamViewModel TeamViewModel
        {
            get { return new TeamViewModel(); }
        }

        public static TeamAddViewModel TeamAddViewModel
        {
            get { return new TeamAddViewModel(); }
        }

        public static SeriesViewModel SeriesViewModel
        {
            get { return new SeriesViewModel(); }
        }

        public static MatchViewModel MatchViewModel
        {
            get { return new MatchViewModel(); }
        }

        public static MatchProtocolViewModel MatchProtocolViewModel
        {
            get { return new MatchProtocolViewModel(); }
        }
    }
}
