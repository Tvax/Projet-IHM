using Projet.ViewModels;
using System.Windows;
using Projet.Modeles;


namespace Projet {
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window_add : Window {

        public AddEmoteViewModel ViewModel { get; set; }

        public Window_add(Emote emot) {   
            ViewModel = new AddEmoteViewModel(emot);
            DataContext = ViewModel;
            InitializeComponent();
        }
    }
}