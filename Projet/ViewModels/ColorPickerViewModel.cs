using Library;
using Projet.Events;
using Projet.Modeles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet.ViewModels {
    public class ColorPickerViewModel {
        public DelegateCommand OKCommand { get; set; }
        public DelegateCommand CancelCommand { get; set; }
        public bool Ans = false;

        private System.Windows.Media.Brush _themeBackup;
        private User _user;
        public User User {
            get { return _user; }
            set { _user = value; }
        }

        public ColorPickerViewModel(User user) {
            User = user;
            _themeBackup = User.Theme;
            OKCommand = new DelegateCommand(OnOKAction, CanOKCommand);
            CancelCommand = new DelegateCommand(OnCancelAction, CanCancelCommand);
        }

        private void OnOKAction(object obj) {
            Ans = true;
            ButtonPressedEvent.GetEvent().OnButtonPressedHandler(EventArgs.Empty);
        }
        private void OnCancelAction(object obj) {
            User.Theme = _themeBackup;
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
