using Library;
using System.Windows.Media.Imaging;

namespace Projet.Modeles
{
    public class Emote : NotifyPropertyChangedBase
    {

        private string _nom { get; set; }
        private string _origine { get; set; }
        private int _emotemin { get; set; }
        private string _abonnement { get; set; }
        private string _description { get; set; }
        private BitmapImage _image { get; set; }

        public string Nom
        {
            get { return _nom; }
            set { _nom = value; }
        }

        public string Origine
        {
            get { return _origine; }
            set { _origine = value; }
        }

        public int EmoteMin
        {
            get { return _emotemin; }
            set { _emotemin = value; }
        }

        public string Abonnement
        {
            get { return _abonnement; }
            set { _abonnement = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public BitmapImage Image
        {
            set { _image = value; }
            get { return _image; }
        }

        public override string ToString()
        {
            return string.Format(Nom);
        }
    }
}
