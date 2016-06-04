using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Projet.Extension {
    public static class ListExtension
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> list)
        {
            var retour = new ObservableCollection<T>();

            foreach (var parcours in list)
            {
                retour.Add(parcours);
            }
            return retour;
        }
    }
}
