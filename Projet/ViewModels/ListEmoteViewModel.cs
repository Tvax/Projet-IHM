using Projet.Modeles;
using Library;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Projet.Factorys;
using BusinessLayer;
using Projet.Events;
using System;
using System.IO;
using System.Windows.Media.Imaging;

//passer list a observable collection

namespace Projet.ViewModels {
    class ListEmoteViewModel : NotifyPropertyChangedBase {
        public DelegateCommand OnAddCommand { get; set; }
        public DelegateCommand EditCommand { get; set; }
        public DelegateCommand DelCommand { get; set; }
        public bool Ans = false;
        private Emote _emot;
        private ObservableCollection<Emote> listeEmote;

        private Window_add _addWindow { get; set; }
        private Window_modify _modWindow { get; set; }
        private Window_remove _rmWindow { get; set; }

        public Emote Emote {
            get {
                return _emot;
            }
            set {
                _emot = value;
                //if (emot.Image == null) emot.Image = new BitmapImage(new Uri(Path.GetFullPath("no_image.png")));
                NotifyPropertyChanged("Emote");
                NotifyPropertyChanged("ListeEmotes");
                OnAddCommand.RaiseCanExecuteChanged();
                DelCommand.RaiseCanExecuteChanged();
                EditCommand.RaiseCanExecuteChanged();
            }
        }

        public ObservableCollection<Emote> ListeEmotes {
            get { return listeEmote; }
            set { listeEmote = value; }
        }

        public ListEmoteViewModel() {
            ListeEmotes = EmoteFactory.AllEmoteEntitieToEmote(EmoteDAO.GetAllEmote());
            OnAddCommand = new DelegateCommand(OnAddAction, CanExecuteAdd);
            EditCommand = new DelegateCommand(OnEditCommand, CanEditCommand);
            DelCommand = new DelegateCommand(OnDelCommand, CanDelCommand);
        }

        private void CloseAddView(object sender, EventArgs e) {
            _addWindow.Close();
            ButtonPressedEvent.GetEvent().Handler -= CloseAddView;
        }
        private void CloseModView(object sender, EventArgs e) {
            _modWindow.Close();
            ButtonPressedEvent.GetEvent().Handler -= CloseModView;
        }
        private void CloseRmView(object sender, EventArgs e) {
            _rmWindow.Close();
            ButtonPressedEvent.GetEvent().Handler -= CloseRmView;
        }

        private void OnDelCommand(object o) {
            ButtonPressedEvent.GetEvent().Handler += CloseRmView;
            _rmWindow = new Window_remove(Ans);
            _rmWindow.Name = "Remove";
            _rmWindow.ShowDialog();

            if (_rmWindow.ViewModel.ans) ListeEmotes.Remove(Emote);
        }

        private void OnAddAction(object o) {
            ButtonPressedEvent.GetEvent().Handler += CloseAddView;
            _addWindow = new Window_add(_emot = new Emote());
            _addWindow.Name = "Add";
            _addWindow.ShowDialog();

            //passer en public viewmodel de Window_Add
            //passer en public class addemoteviewmodel
            // rajouter dans les bindings  UpdateSourceTrigger=PropertyChanged}
            if (_addWindow.ViewModel.valid == true && _addWindow.ViewModel.Emote.Nom != null) {
                listeEmote.Add(_addWindow.ViewModel.Emote);
                NotifyPropertyChanged("ListeEmotes");
                NotifyPropertyChanged("Emote");
            }
        }

        private void OnEditCommand(object o) {
            ButtonPressedEvent.GetEvent().Handler += CloseModView;

            string desc = Emote.Description;
            string nom = Emote.Nom;
            BitmapImage img = Emote.Image;

            _modWindow = new Window_modify(Emote);
            _modWindow.Name = "Edit";
            _modWindow.ShowDialog();
            

            if (_modWindow._viewModel.valid == true && String.IsNullOrWhiteSpace(Emote.Nom) == false) {
                string desc1 = Emote.Description;
                string nom1 = Emote.Nom;
                BitmapImage img1 = Emote.Image;

                ListeEmotes.Remove(Emote);

                Emote tmp = new Emote();
                tmp.Description = desc1;
                tmp.Nom = nom1;
                tmp.Image = img1;
 
                ListeEmotes.Add(tmp);
                NotifyPropertyChanged("ListeEmotes");
                NotifyPropertyChanged("Emote");
            }
            else if (_modWindow._viewModel.valid == false || String.IsNullOrWhiteSpace(Emote.Nom) == true) {
                ListeEmotes.Remove(Emote);
                Emote tmp = new Emote();
                tmp.Nom = nom;
                tmp.Description = desc;
                tmp.Image = img;
                ListeEmotes.Add(tmp);
                NotifyPropertyChanged("ListeEmotes");
                NotifyPropertyChanged("Emote");
            }
        }


        private bool CanExecuteAdd(object o) {
            return true;
        }

        private bool CanEditCommand(object o) {
            return Emote != null;
        }

        private bool CanDelCommand(object o) {
            return Emote != null;
        }
    }
}