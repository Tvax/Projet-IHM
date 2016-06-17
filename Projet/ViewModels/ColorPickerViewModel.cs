using Library;
using Projet.Events;
using Projet.Modeles;
using System;

namespace Projet.ViewModels {
    public class ColorPickerViewModel {
        public DelegateCommand OKCommand { get; set; }
        public DelegateCommand CancelCommand { get; set; }
        public bool Ans = false;

        private System.Windows.Media.Brush _theme;
        private User _user;

        public User User {
            get { return _user; }
            set { _user = value; }
        }

        public System.Windows.Media.Brush Theme {
            get { return _theme; }
            set { _theme = value; }
        }

        public ColorPickerViewModel(User user) {
            Theme = user.Theme;
            User = user;

            OKCommand = new DelegateCommand(OnOKAction, CanOKCommand);
            CancelCommand = new DelegateCommand(OnCancelAction, CanCancelCommand);
        }

        private void OnOKAction(object obj) {
            Ans = true;
            User.Theme = Theme;
            ButtonPressedEvent.GetEvent().OnButtonPressedHandler(EventArgs.Empty);
        }
        private void OnCancelAction(object obj) {
            ButtonPressedEvent.GetEvent().OnButtonPressedHandler(EventArgs.Empty);
        }

        private bool CanOKCommand(object obj) {
            return true;
        }
        private bool CanCancelCommand(object obj) {
            return true;
        }
    }


}
