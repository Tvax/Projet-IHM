using Projet.ViewModels;
using System.Windows;

namespace Projet {
    /// <summary>
    /// Logique d'interaction pour Window_remove.xaml
    /// </summary>
    public partial class Window_remove : Window {

        public RemoveEmoteViewModel ViewModel { get; set; }
        
        public Window_remove(bool ans) {
            InitializeComponent();
            ViewModel = new RemoveEmoteViewModel(ans);
            DataContext = ViewModel;
        }
    }
}
