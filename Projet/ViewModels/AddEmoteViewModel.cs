using Library;
using Microsoft.Win32;
using Projet.Modeles;
using System;
using System.Windows.Media.Imaging;
using Projet.Events;

namespace Projet.ViewModels {
    public class AddEmoteViewModel : NotifyPropertyChangedBase {
        public DelegateCommand OKCommand { get; set; }
        public DelegateCommand CancelCommand { get; set; }
        public DelegateCommand LoadCommand { get; set; }
        public bool Valid = false;

        private Emote _emot;

        public Emote Emote {
            get { return _emot; }
            set { _emot = value; }
        }

        public AddEmoteViewModel(Emote emote) {
            Emote = emote;

            OKCommand = new DelegateCommand(OnOKAction, CanExecuteOK);
            CancelCommand = new DelegateCommand(OnCancelCommand, CanCancelCommand);
            LoadCommand = new DelegateCommand(OnLoadCommand, CanLoadCommand);
        }

        #region OnActions
        private void OnCancelCommand(object o) {
            Valid = false;
            ButtonPressedEvent.GetEvent().OnButtonPressedHandler(EventArgs.Empty);
        }

        private void OnOKAction(object o) {
            Valid = true;
            _emot = Emote;
            ButtonPressedEvent.GetEvent().OnButtonPressedHandler(EventArgs.Empty);
        }

        private void OnLoadCommand(object o) {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true) {
                Emote.Image = new BitmapImage(new Uri(op.FileName));
            }
        } 
        #endregion

        #region CanExecuteCommands
        private bool CanCancelCommand(object o) {
            return true;
        }
        private bool CanExecuteOK(object o) {
            return true;
        }
        private bool CanLoadCommand(object o) {
            return true;
        } 
        #endregion
    }
}

