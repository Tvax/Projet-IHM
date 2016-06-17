using Library;
using Projet.Events;
using Projet.Modeles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Windows.Media;

namespace Projet.ViewModels {
    public class LoginViewModel : NotifyPropertyChangedBase {
        public DelegateCommand OnCreateAccountCommand { get; set; }
        public DelegateCommand OnLoginCommand { get; set; }

        private Dictionary<string, User> _settings { get; set; }       
        private Dictionary<string, string> _usernamesPassword;
        private List<string> _usernames;
        private User _user;
        private string _xmlUsersFile = "../../users.xml";
        private Window_error _errWindow { get; set; }

        public User User {
            get { return _user; }
            set { _user = value; }
        }

        public LoginViewModel(User user, Dictionary<string, User> settings) {
            _settings = settings;
            User = user;
            LoadingUserXML();
            OnCreateAccountCommand = new DelegateCommand(OnCreateAction, CanExecuteCreate);
            OnLoginCommand = new DelegateCommand(OnLoginAction, CanExecuteLogin);
        }

        private void LoadingUserXML() {
            _usernames = new List<string>();
            var xmlString = XDocument.Load(Path.GetFullPath(_xmlUsersFile));

            _usernames = XDocument.Parse(xmlString.ToString()).Descendants("user").
                Select(e => (string)e.Attribute("username")).ToList();

            _usernamesPassword = XDocument.Parse(xmlString.ToString()).Descendants("user").
                Select(e => new {
                    Username = (string)(e.Attribute("username")),
                    Password = (string)(e.Attribute("password"))
                }).ToDictionary(e => e.Username, e => e.Password);
        }

        #region Connection
        private void OnLoginAction(object obj) {

            string value = null;
            _usernamesPassword.TryGetValue(User.Username, out value);

            if (User.Username == null) {
                _errWindow = new Window_error("Username is null.");
                _errWindow.ShowDialog();
            }
            else if (User.Password == null) {
                _errWindow = new Window_error("Password is null.");
                _errWindow.ShowDialog();
            }
            else if (!_usernames.Contains(User.Username)) {
                _errWindow = new Window_error("Unknown user.");
                _errWindow.ShowDialog();
            }

            else if (value != User.Password) {
                _errWindow = new Window_error("Wrong Password");
                _errWindow.ShowDialog();
            }
            else {
                var doc = XDocument.Load(Path.GetFullPath(_xmlUsersFile));
                var result = doc.Root.Elements("user").
                Where(o => (string)o.Attribute("username") == User.Username);

                var converter = new System.Windows.Media.BrushConverter();

                foreach (var r in result) {
                    var brush = (Brush)converter.ConvertFromString(r.Attribute("theme").Value.ToString());
                    User.Theme = brush;
                    User.List = r.Attribute("list").Value.ToString();
                }
                ButtonPressedEvent.GetEvent().OnButtonPressedHandler(EventArgs.Empty);
            }
        }

        private void OnCreateAction(object obj) {
            User.Theme = System.Windows.Media.Brushes.White;
            User.List = User.Username;
            if (User.Username == null) {
                _errWindow = new Window_error("Username is null.");
                _errWindow.ShowDialog();
            }
            else if (User.Password == null) {
                _errWindow = new Window_error("Password is null.");
                _errWindow.ShowDialog();
            }
            else if (_usernames.Contains(User.Username)) {
                _errWindow = new Window_error("Username already exists.");
                _errWindow.ShowDialog();
            }
            else {
                var xDoc = XElement.Load(Path.GetFullPath(_xmlUsersFile));
                if (xDoc == null) return;
                var myNewElement = new XElement("user",
                    new XAttribute("username", User.Username), new XAttribute("password", User.Password), new XAttribute("theme", User.Theme), new XAttribute("list", User.List));
                xDoc.Add(myNewElement);
                xDoc.Save(Path.GetFullPath(_xmlUsersFile));
                _settings.Add(User.Username, User);
                ButtonPressedEvent.GetEvent().OnButtonPressedHandler(EventArgs.Empty);
            }
        } 
        #endregion

        #region CanExecuteCommands
        private bool CanExecuteLogin(object obj) {
            return true;
        }
        private bool CanExecuteCreate(object obj) {
            return true;
        }
        #endregion
    }
}