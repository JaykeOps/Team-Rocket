using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FootballManager.App.Extensions
{
    public static class ListExtension
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> coll)
        {   // Not the best choice of variable names, should be more descriptive.
            var c = new ObservableCollection<T>();
            foreach (var e in coll)
                c.Add(e);
            return c;
        }
    }
}