using Library;
using Microsoft.Win32;
using Projet.Modeles;
using System;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Projet;
using Projet.Events;

namespace Projet.ViewModels {
    public class AddEmoteViewModel : NotifyPropertyChangedBase {
        public DelegateCommand OKCommand { get; set; }
        public DelegateCommand CancelCommand { get; set; }
        public DelegateCommand LoadCommand { get; set; }
        private Emote emot;
        public bool valid = false;

        public Emote Emote {
            get { return emot; }
            set { emot = value; }
        }

        public AddEmoteViewModel(Emote emote) {
            Emote = emote;

            OKCommand = new DelegateCommand(OnOKAction, CanExecuteOK);
            CancelCommand = new DelegateCommand(OnCancelCommand, CanCancelCommand);
            LoadCommand = new DelegateCommand(OnLoadCommand, CanLoadCommand);
        }

        private void OnCancelCommand(object o) {
            valid = false;
            Emote = null;
            ButtonPressedEvent.GetEvent().OnButtonPressedHandler(EventArgs.Empty);
        }
        private bool CanCancelCommand(object o) {
            return true;
        }
        
        private void OnOKAction(object o) {
            valid = true;
            emot = Emote;
            ButtonPressedEvent.GetEvent().OnButtonPressedHandler(EventArgs.Empty);
        }
        private bool CanExecuteOK(object o) { 
            return true;
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
        private bool CanLoadCommand(object o) {
            return true;
        }
    }
}

