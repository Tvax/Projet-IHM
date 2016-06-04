using Library;
using System.Windows.Media.Imaging;

namespace Projet.Modeles {
    public class Emote : NotifyPropertyChangedBase {

        private string _nom { get; set; }
        private string _description { get; set; }
        private BitmapImage _image { get; set; }

        public string Nom {
            get { return _nom; }
            set { _nom = value; }
        }
        public string Description {
            get { return _description; }
            set { _description = value; }
        }
        public BitmapImage Image {
            set { _image = value; }
            get { return _image; }
        }

        public override string ToString() {
            return string.Format(Nom);
        }
    }
}
