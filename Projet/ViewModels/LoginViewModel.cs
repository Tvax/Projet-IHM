﻿using Library;
using Projet.Events;
using Projet.Modeles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Projet.ViewModels {
    public class LoginViewModel : NotifyPropertyChangedBase {
        public DelegateCommand OnCreateAccountCommand { get; set; }
        public DelegateCommand OnLoginCommand { get; set; }
        public Dictionary<string, User> Settings { get; set; }
        public List<string> Usernames;

        private User _user;
        private string _xmlUsersFile = "users.xml";
        private Window_error _errWindow { get; set; }

        public User User {
            get { return _user; }
            set { _user = value; }
        }

        public LoginViewModel(User user, Dictionary<string, User> settings) {
            Settings = settings;
            User = user;
            LoadingUserXML();
            OnCreateAccountCommand = new DelegateCommand(OnCreateAction, CanExecuteCreate);
            OnLoginCommand = new DelegateCommand(OnLoginAction, CanExecuteLogin);
        }

        private void LoadingUserXML() {
            Usernames = new List<string>();
            var xmlString = XDocument.Load(Path.GetFullPath(_xmlUsersFile));

            Usernames = XDocument.Parse(xmlString.ToString()).Descendants("user").
                Select(e => (string)e.Attribute("username")).ToList();
        }

        private void OnLoginAction(object obj) {
            ButtonPressedEvent.GetEvent().Handler += CloseErrorView;
            if (User.Username == null) {
                _errWindow = new Window_error("Username is null.");
                _errWindow.ShowDialog();
            }
            else if (User.Password == null) {
                _errWindow = new Window_error("Password is null.");
                _errWindow.ShowDialog();
            } 
            else {
                var doc = XDocument.Load(Path.GetFullPath(_xmlUsersFile));
                var Settings = doc.Descendants("user").ToDictionary(
                datum => datum.Attribute("username").Value,
                datum => datum.Attribute("password").Value);
                ButtonPressedEvent.GetEvent().OnButtonPressedHandler(EventArgs.Empty);
            }
        }

        private void OnCreateAction(object obj) {
            ButtonPressedEvent.GetEvent().Handler += CloseErrorView;
            User.Theme = System.Windows.Media.Brushes.White;
            
            if (User.Username == null) {
                _errWindow = new Window_error("Username is null.");
                _errWindow.ShowDialog();
            }
            else if (User.Password == null) {
                _errWindow = new Window_error("Password is null.");
                _errWindow.ShowDialog();
            }
            else if (Usernames.Contains(User.Username)) {
                _errWindow = new Window_error("Username already exists.");
                _errWindow.ShowDialog();
            }
            else {
                var xDoc = XElement.Load(Path.GetFullPath(_xmlUsersFile));
                if (xDoc == null)
                    return;
                var myNewElement = new XElement("user",
                    new XAttribute("username", User.Username), new XAttribute("password", User.Password), new XAttribute("theme", User.Theme), new XAttribute("list", ""));
                xDoc.Add(myNewElement);
                xDoc.Save(Path.GetFullPath(_xmlUsersFile));
                Settings.Add(User.Username, User);//ajoute en key le nom du user, et ajoute les autres infos en value
                ButtonPressedEvent.GetEvent().OnButtonPressedHandler(EventArgs.Empty);
            }
        }

        private void CloseErrorView(object sender, EventArgs e) {
            _errWindow.Close();
            ButtonPressedEvent.GetEvent().Handler -= CloseErrorView;
        }

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