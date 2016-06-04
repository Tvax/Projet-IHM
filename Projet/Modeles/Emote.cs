using System.Windows.Controls;
using Library;
using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace Projet.Modeles {
    public class Emote : NotifyPropertyChangedBase {

        private string nom { get; set; }
        private string description { get; set; }
        private BitmapImage image { get; set; }

        public string Nom {
            get { return nom; }
            set { nom = value; }
        }
        public string Description {
            get { return description; }
            set { description = value; }
        }
        public BitmapImage Image {
            set { image = value; }
            get { return image; }
        }

        public override string ToString() {
            return string.Format(Nom);
        }
    }
}
