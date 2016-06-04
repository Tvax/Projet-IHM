using Library;
using Projet.Events;
using Projet.Modeles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet.ViewModels {
    public class LoginViewModel : NotifyPropertyChangedBase {
        public DelegateCommand OnCreateAccountCommand { get; set; }
        public DelegateCommand OnLoginCommand { get; set; }
        private User _user;

        public User User {
            get { return _user; }
            set { _user = value; }
        }

        public LoginViewModel(User user) {
            User = user;
            OnCreateAccountCommand = new DelegateCommand(OnCreateAction, CanExecuteCreate);
            OnLoginCommand = new DelegateCommand(OnLoginAction, CanExecuteLogin);
        }

        private void OnLoginAction(object obj) {
            ButtonPressedEvent.GetEvent().OnButtonPressedHandler(EventArgs.Empty);
        }

        private void OnCreateAction(object obj) {
            ButtonPressedEvent.GetEvent().OnButtonPressedHandler(EventArgs.Empty);
        }

        #region CanExecuteCommands
        private bool CanExecuteLogin(object obj) {
            return User != null;
        }
        private bool CanExecuteCreate(object obj) {
            return User != null;
        } 
        #endregion
    }
}