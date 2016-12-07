using FootballManager.Admin.ViewModel;

namespace FootballManager.Admin
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

        public static PlayerInfoViewModel PlayerInfoViewModel
        {
            get { return new PlayerInfoViewModel(); }
        }

        public static PlayerEditViewModel PlayerEditViewModel
        {
            get { return new PlayerEditViewModel(); }
        }

        public static TeamViewModel TeamViewModel
        {
            get { return new TeamViewModel(); }
        }

        public static TeamAddViewModel TeamAddViewModel
        {
            get { return new TeamAddViewModel(); }
        }

        public static TeamInfoViewModel TeamInfoViewModel
        {
            get { return new TeamInfoViewModel(); }
        }

        public static SeriesViewModel SeriesViewModel
        {
            get { return new SeriesViewModel(); }
        }

        public static SeriesScheduleViewModel SeriesScheduleViewModel
        {
            get { return new SeriesScheduleViewModel(); }
        }

        public static SeriesScheduleEditViewModel SeriesScheduleEditViewModel
        {
            get { return new SeriesScheduleEditViewModel(); }
        }

        public static SeriesGameProtocolViewModel SeriesGameProtocolViewModel
        {
            get { return new SeriesGameProtocolViewModel(); }
        }

        public static TeamInfoEditPlayerViewModel TeamInfoEditPlayerViewModel
        {
            get { return new TeamInfoEditPlayerViewModel(); }
        }

        public static TeamEditViewModel TeamEditViewModel
        {
            get { return new TeamEditViewModel(); }
        }
    }
}