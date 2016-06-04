using Library;
using Projet.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Projet.ViewModels {
    public class ErrorViewModel {
        private string _error;
        private BitmapImage _image;
        public DelegateCommand OKCommandError { get; set; }

        public string Error {
            get { return _error; }
            set { _error = value; }
        }
        public BitmapImage Image {
            get { return _image; }
            set { _image = value; }
        }

        public ErrorViewModel(string error) {
            OKCommandError = new DelegateCommand(OnOKActionErrors, CanOKCommand);
            Error = error;
            Image = new BitmapImage(new Uri(Path.GetFullPath("error_red.png")));
        }

        private bool CanOKCommand(object obj) {
            return true;
        }

        private void OnOKActionErrors(object o) {
            ButtonPressedEvent.GetEvent().OnButtonPressedHandler(EventArgs.Empty);
        }
    }
}
