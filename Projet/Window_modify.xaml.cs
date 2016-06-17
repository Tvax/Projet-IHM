using System.Windows;
using Projet.Modeles;
using Projet.ViewModels;

namespace Projet {
    /// <summary>
    /// Logique d'interaction pour Window_modify.xaml
    /// </summary>
    public partial class Window_modify : Window {
        public AddEmoteViewModel ViewModel;

        public Window_modify(Emote emot, User user) {
            InitializeComponent();
            ViewModel = new AddEmoteViewModel(emot, user);
            DataContext = ViewModel;
        }
    }
}

