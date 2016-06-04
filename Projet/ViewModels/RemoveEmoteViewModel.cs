using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projet.Factorys;
using Projet.Events;

namespace Projet.ViewModels {
    public class RemoveEmoteViewModel : NotifyPropertyChangedBase {
        public DelegateCommand Yes { get; set; }
        public DelegateCommand No { get; set; }
        public bool ans { get; set; }

        public RemoveEmoteViewModel(bool ans) {
            this.ans = ans;
            Yes = new DelegateCommand(OnYesAction, CanExecuteYes);
            No = new DelegateCommand(OnNoAction, CanExecuteNo);
        }


        private void OnNoAction(object obj) {
            ans = false;
            ButtonPressedEvent.GetEvent().OnButtonPressedHandler(EventArgs.Empty);
        }

        private void OnYesAction(object obj) {
            ans = true;
            ButtonPressedEvent.GetEvent().OnButtonPressedHandler(EventArgs.Empty);
        }

        private bool CanExecuteNo(object obj) {
            return true;
        }
        private bool CanExecuteYes(object obj) {
            return true;
        }


    }





}
