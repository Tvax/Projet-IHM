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

//passer list a observable collection

namespace Projet.ViewModels {
    class ListEmoteViewModel : NotifyPropertyChangedBase {
        public DelegateCommand OnAddCommand { get; set; }
        public DelegateCommand EditCommand { get; set; }
        public DelegateCommand DelCommand { get; set; }
        public bool Ans = false;
        public Dictionary<string, User> Settings = new Dictionary<string, User>();

        private string _xmlListFile = "lists.xml";
        private User _user;
        private Emote _emot;
        private ObservableCollection<Emote> _listeEmote;
        private Window_add _addWindow { get; set; }
        private Window_modify _modWindow { get; set; }
        private Window_remove _rmWindow { get; set; }
        private Window_login _loginWindow { get; set; }

        public User User {
            get { return _user; }
            set { _user = value; NotifyPropertyChanged("Emote"); }
        }

        public Emote Emote {
            get {  return _emot; }
            set { _emot = value;
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
            ListeEmotes = EmoteFactory.AllEmoteEntitieToEmote(EmoteDAO.GetAllEmote());
            OnAddCommand = new DelegateCommand(OnAddAction, CanExecuteAdd);
            EditCommand = new DelegateCommand(OnEditCommand, CanEditCommand);
            DelCommand = new DelegateCommand(OnDelCommand, CanDelCommand);
        }

        private void ListLoading() {
            var xmlString = XDocument.Load(Path.GetFullPath(_xmlListFile));
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlString.ToString());
            XmlElement root = doc.DocumentElement;
            string lel = doc.DocumentElement.GetAttribute("username");

            var Usernames = XDocument.Parse(xmlString.ToString()).Descendants("database").Descendants("user").
                           Select(e => (string)e.Attribute("username") == User.List).ToList();


            var node = (from file in xmlString.
                        Descendants("user")
                        where (string)file.Attribute("list") == User.List
                        select file).Single();


            var doc1 = XDocument.Load(_xmlListFile);
            /*
            var node = doc1.XPathSelectElements("Stock/Tyre/Manufacturer")
              .FirstOrDefault(x => x.Value == manufacturer);


            IEnumerable<XElement> address =
                from el in root1.Elements("database")
                where (string)el.Attribute("Type") == "Billing"
                select el;
            foreach (XElement el in address)
                Console.WriteLine(el);


            string s = root.Attributes["database"].Value;
            var lists = xmlString.Descendants("user").ToDictionary(
               datum => datum.Attribute("username").Value,
               datum => datum.Attribute("password").Value);
            User.List

        ;
        */
        }

        private void Login() {
            ButtonPressedEvent.GetEvent().Handler += CloseLoginView;
            _loginWindow = new Window_login(_user = new User(), Settings);
            _loginWindow.ShowDialog();
            if (User.Username == null)  App.Current.Shutdown();
        }

        #region OnActions
        private void OnDelCommand(object o) {
            ButtonPressedEvent.GetEvent().Handler += CloseRmView;
            _rmWindow = new Window_remove(Ans);
            _rmWindow.Name = "Remove";
            _rmWindow.ShowDialog();

            if (_rmWindow.ViewModel.Ans)
                ListeEmotes.Remove(Emote);
        }
        private void OnAddAction(object o) {
            ButtonPressedEvent.GetEvent().Handler += CloseAddView;
            _addWindow = new Window_add(_emot = new Emote());
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
            BitmapImage imgBackup = Emote.Image;

            _modWindow = new Window_modify(Emote);
            _modWindow.Name = "Edit";
            _modWindow.ShowDialog();


            if (_modWindow.ViewModel.Valid && !String.IsNullOrWhiteSpace(Emote.Nom)) {
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
            else if (!_modWindow.ViewModel.Valid || String.IsNullOrWhiteSpace(Emote.Nom)) {
                ListeEmotes.Remove(Emote);

                Emote tmp = new Emote();
                tmp.Nom = nomBackup;
                tmp.Description = descBackup;
                tmp.Image = imgBackup;
                ListeEmotes.Add(tmp);

                NotifyPropertyChanged("ListeEmotes");
                NotifyPropertyChanged("Emote");
            }
        }
        #endregion

        #region CloseEvents

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
        private bool CanExecuteAdd(object o) {
            return true;
        }

        private bool CanEditCommand(object o) {
            return Emote != null;
        }

        private bool CanDelCommand(object o) {
            return Emote != null;
        }
        #endregion
    }
}