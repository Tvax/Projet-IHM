using Projet.Modeles;
using Library;
using System.Collections.ObjectModel;
using Projet.Factorys;
using BusinessLayer;
using Projet.Events;
using System;
using System.Windows.Media.Imaging;
using System.Collections.Generic;
using System.Xml.Linq;
using System.IO;
using System.Linq;
using System.Xml;

namespace Projet.ViewModels {
    class ListEmoteViewModel : NotifyPropertyChangedBase {
        public DelegateCommand OnAddCommand { get; set; }
        public DelegateCommand EditCommand { get; set; }
        public DelegateCommand DelCommand { get; set; }
        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand ColorBoxCommand { get; set; }
        public bool Ans = false;
        public Dictionary<string, User> Settings = new Dictionary<string, User>();

        private Emote _tmp;
        private string _xmlListFile = "../../lists.xml";
        private string _xmlUsersFile = "../../users.xml";
        private User _user;
        private Emote _emot;
        private ObservableCollection<Emote> _listeEmote;
        private Window_add _addWindow { get; set; }
        private Window_modify _modWindow { get; set; }
        private Window_remove _rmWindow { get; set; }
        private Window_login _loginWindow { get; set; }
        private Window_colorPicker _colorWindow { get; set; }

        public User User {
            get { return _user; }
            set { _user = value; NotifyPropertyChanged("Emote"); }
        }

        public Emote Emote {
            get { return _emot; }
            set {
                _emot = value;
                NotifyPropertyChanged("Emote");
                NotifyPropertyChanged("ListeEmotes");
                OnAddCommand.RaiseCanExecuteChanged();
                DelCommand.RaiseCanExecuteChanged();
                EditCommand.RaiseCanExecuteChanged();
            }
        }

        public ObservableCollection<Emote> ListeEmotes {
            get { return _listeEmote; }
            set { _listeEmote = value; }
        }

        public ListEmoteViewModel() {
            Login();
            ListLoading();
            ColorBoxCommand = new DelegateCommand(OnColorAction, CanExecuteColor);
            OnAddCommand = new DelegateCommand(OnAddAction, CanExecuteAdd);
            EditCommand = new DelegateCommand(OnEditCommand, CanEditCommand);
            DelCommand = new DelegateCommand(OnDelCommand, CanDelCommand);
            SaveCommand = new DelegateCommand(OnSaveCommand, CanSaveCommand);
        }
       

        private void ListLoading() {
            ListeEmotes = new ObservableCollection<Emote>();
            var xmlString = XDocument.Load(Path.GetFullPath(_xmlListFile));
            var result = xmlString.Root.Elements("user").
                Where(o => (string)o.Attribute("list") == User.List).
                Elements("list");

            string nom = null;
            BitmapImage image = new BitmapImage();
            string description = null;
            string origine = null;
            string emotemin = null;
            string abonnement = null;

            foreach (var r in result) {
                _tmp = new Emote();
                nom = r.Attribute("nom").Value.ToString();
                description = r.Attribute("description").Value;
                image = new BitmapImage(new Uri((r.Attribute("image").Value.ToString())));
                origine = r.Attribute("origine").Value.ToString();
                emotemin = r.Attribute("emotemin").Value.ToString();
                abonnement = r.Attribute("abonnement").Value.ToString();

                _tmp.Nom = nom;
                _tmp.Description = description;
                _tmp.Origine = origine;
                _tmp.EmoteMin = emotemin;
                _tmp.Abonnement = abonnement;
                _tmp.Image = image;
                ListeEmotes.Add(_tmp);
            }
        }

        private void Login() {
            ButtonPressedEvent.GetEvent().Handler += CloseLoginView;
            _loginWindow = new Window_login(_user = new User(), Settings);
            _loginWindow.ShowDialog();
            if (User.Username == null || User.Password == null) App.Current.Shutdown();
        }

        #region OnActions
        private void OnColorAction(object obj) { 
            ButtonPressedEvent.GetEvent().Handler += CloseColorView;
            _colorWindow = new Window_colorPicker(_user);
            _colorWindow.Name = "Theme";
            _colorWindow.ShowDialog();

        }
        private void OnDelCommand(object o) {
            ButtonPressedEvent.GetEvent().Handler += CloseRmView;
            _rmWindow = new Window_remove(Ans, User);
            _rmWindow.Name = "Remove";
            _rmWindow.ShowDialog();

            if (_rmWindow.ViewModel.Ans)
                ListeEmotes.Remove(Emote);
        }
        private void OnSaveCommand(object o) {
            //Delete toutes listes
            var xmlString = XDocument.Load(Path.GetFullPath(_xmlListFile));
            xmlString.Root.Elements("user").
                Where(i => (string)i.Attribute("list") == User.List).Remove();
            xmlString.Save(Path.GetFullPath(_xmlListFile));

            //Ajoute toutes listes une par une
            dynamic myNewElement1 = null;
            dynamic myNewElement2 = new XElement("user",
                   new XAttribute("list", User.Username));

            foreach (var r in ListeEmotes) {
                myNewElement1 = new XElement("list",
                      new XAttribute("nom", r.Nom), new XAttribute("description", r.Description), new XAttribute("image", r.Image), new XAttribute("origine", r.Origine), new XAttribute("emotemin", r.EmoteMin), new XAttribute("abonnement", r.Abonnement));
                myNewElement2.Add(myNewElement1);
            }

            var xmlString1 = XElement.Load(Path.GetFullPath(_xmlListFile));
            xmlString1.Add(myNewElement2);
            xmlString1.Save(Path.GetFullPath(_xmlListFile));

            //Suppression user
            var xmlString2 = XDocument.Load(Path.GetFullPath(_xmlUsersFile));
            xmlString2.Root.Elements("user").
                Where(i => (string)i.Attribute("username") == User.List).Remove();
            xmlString2.Save(Path.GetFullPath(_xmlUsersFile));

            //Ajoute user modifie
            var xDoc = XElement.Load(Path.GetFullPath(_xmlUsersFile));
            if (xDoc == null) return;
            var myNewElement = new XElement("user",
                    new XAttribute("username", User.Username), new XAttribute("password", User.Password), new XAttribute("theme", User.Theme), new XAttribute("list", User.List));
            xDoc.Add(myNewElement);
            xDoc.Save(Path.GetFullPath(_xmlUsersFile));

        }
        private void OnAddAction(object o) {
            ButtonPressedEvent.GetEvent().Handler += CloseAddView;
            _addWindow = new Window_add(_emot = new Emote(), User);
            _addWindow.Name = "Add";
            _addWindow.ShowDialog();

            //passer en public viewmodel de Window_Add
            //passer en public class addemoteviewmodel
            // rajouter dans les bindings  UpdateSourceTrigger=PropertyChanged}
            if (_addWindow.ViewModel.Valid && _addWindow.ViewModel.Emote.Nom != null) {
                _listeEmote.Add(_addWindow.ViewModel.Emote);
                NotifyPropertyChanged("ListeEmotes");
                NotifyPropertyChanged("Emote");
            }
        }
        private void OnEditCommand(object o) {
            ButtonPressedEvent.GetEvent().Handler += CloseModView;

            string descBackup = Emote.Description;
            string nomBackup = Emote.Nom;
            string origBackup = Emote.Origine;
            string subBackup = Emote.Abonnement;
            string eminBackup = Emote.EmoteMin;
            BitmapImage imgBackup = Emote.Image;

            _modWindow = new Window_modify(Emote, User);
            _modWindow.Name = "Edit";
            _modWindow.ShowDialog();


            if (_modWindow.ViewModel.Valid && !String.IsNullOrWhiteSpace(Emote.Nom)) {
                string desc1 = Emote.Description;
                string nom1 = Emote.Nom;
                string orig1 = Emote.Origine;
                string sub1 = Emote.Abonnement;
                string emin1 = Emote.EmoteMin;
                BitmapImage img1 = Emote.Image;

                ListeEmotes.Remove(Emote);

                Emote tmp = new Emote();
                tmp.Description = desc1;
                tmp.Nom = nom1;
                tmp.Origine = orig1;
                tmp.EmoteMin = emin1;
                tmp.Abonnement = sub1;
                tmp.Image = img1;
                ListeEmotes.Add(tmp);

                NotifyPropertyChanged("ListeEmotes");
                NotifyPropertyChanged("Emote");
            }
            else if (!_modWindow.ViewModel.Valid || String.IsNullOrWhiteSpace(Emote.Nom)) {
                ListeEmotes.Remove(Emote);

                Emote tmp = new Emote();
                tmp.Nom = nomBackup;
                tmp.Description = descBackup;
                tmp.Origine = origBackup;
                tmp.Abonnement = subBackup;
                tmp.EmoteMin = eminBackup;
                tmp.Image = imgBackup;
                ListeEmotes.Add(tmp);

                NotifyPropertyChanged("ListeEmotes");
                NotifyPropertyChanged("Emote");
            }
        }
        #endregion

        #region CloseEvents
        private void CloseColorView(object sender, EventArgs e) {
            _colorWindow.Close();
            ButtonPressedEvent.GetEvent().Handler -= CloseColorView;
        }
        private void CloseLoginView(object sender, EventArgs e) {
            _loginWindow.Close();
            ButtonPressedEvent.GetEvent().Handler -= CloseLoginView;
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
        #endregion

        #region CanExecuteCommands
        private bool CanSaveCommand(object obj) {
            return true;
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
        private bool CanExecuteColor(object obj) {
            return true;
        }
        #endregion
    }
}