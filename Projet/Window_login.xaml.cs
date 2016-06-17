using Projet.Modeles;
using Projet.ViewModels;
using System.Collections.Generic;
using System.Windows;

namespace Projet {
    /// <summary>
    /// Interaction logic for Window_login.xaml
    /// </summary>
    public partial class Window_login : Window {
        public LoginViewModel ViewModel;

        public Window_login(User user, Dictionary<string, User> settings) {
            InitializeComponent();
            ViewModel = new LoginViewModel(user, settings);
           DataContext = ViewModel;

        }
    }
}
