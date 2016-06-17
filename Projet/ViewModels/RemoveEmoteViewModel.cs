using Library;
using System;
using Projet.Events;
using Projet.Modeles;

namespace Projet.ViewModels {
    public class RemoveEmoteViewModel : NotifyPropertyChangedBase {
        public DelegateCommand Yes { get; set; }
        public DelegateCommand No { get; set; }
        public bool Ans { get; set; }

        private User _user;

        public User User {
            get { return _user; }
            set { _user = value; }
        }

        public RemoveEmoteViewModel(bool ans, User user) {
            User = user;
            this.Ans = ans;
            Yes = new DelegateCommand(OnYesAction, CanExecuteYes);
            No = new DelegateCommand(OnNoAction, CanExecuteNo);
        }

        #region OnActions
        private void OnNoAction(object obj) {
            Ans = false;
            ButtonPressedEvent.GetEvent().OnButtonPressedHandler(EventArgs.Empty);
        }
        private void OnYesAction(object obj) {
            Ans = true;
            ButtonPressedEvent.GetEvent().OnButtonPressedHandler(EventArgs.Empty);
        } 
        #endregion

        #region CanExecuteCommands
        private bool CanExecuteNo(object obj) {
            return true;
        }
        private bool CanExecuteYes(object obj) {
            return true;
        } 
        #endregion
    }
}
