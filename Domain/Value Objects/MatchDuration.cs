using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Value_Objects
{
    public class MatchDuration
    {
        public TimeSpan Value { get; set; }

        public MatchDuration(TimeSpan matchDuration)
        {
            this.Value = matchDuration;  
        }

        public MatchDuration(string matchDuration)
        {
            
        }

        public static bool IsMatchDuration(TimeSpan matchDuration)
        {
            if (matchDuration.Minutes>90&&matchDuration.Minutes<15)
            {
                return true;
            }
            return false;
        }
    }
}
