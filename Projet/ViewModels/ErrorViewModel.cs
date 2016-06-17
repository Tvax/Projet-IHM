using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace Projet.ViewModels {
    public class ErrorViewModel {

        private string _error;
        private BitmapImage _image;

        public string Error {
            get { return _error; }
            set { _error = value; }
        }
        public BitmapImage Image {
            get { return _image; }
            set { _image = value; }
        }

        public ErrorViewModel(string error) {
            Error = error;
            Image = new BitmapImage(new Uri(Path.GetFullPath("error_red.png")));
        }
    }
}
