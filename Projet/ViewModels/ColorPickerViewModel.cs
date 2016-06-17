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
        private User _user;
        public bool Ans = false;

        public User User {
            get { return _user; }
            set { _user = value; }
        }

        public ColorPickerViewModel(User user) {
            User = user;
            OKCommand = new DelegateCommand(OnOKAction, CanOKCommand);
        }

        private void OnOKAction(object obj) {
            Ans = true;
            ButtonPressedEvent.GetEvent().OnButtonPressedHandler(EventArgs.Empty);
        }

        private bool CanOKCommand(object obj) {
            return true;
        }
    }

    
}
