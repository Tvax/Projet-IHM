using Projet.Modeles;
using Projet.ViewModels;
using System.Windows;

namespace Projet {
    /// <summary>
    /// Logique d'interaction pour Window_remove.xaml
    /// </summary>
    public partial class Window_remove : Window {
        public RemoveEmoteViewModel ViewModel { get; set; }
        
        public Window_remove(bool ans, User user) {
            InitializeComponent();
            ViewModel = new RemoveEmoteViewModel(ans, user);
            DataContext = ViewModel;
        }
    }
}
