using Projet.ViewModels;
using System.Windows;

namespace Projet {
    /// <summary>
    /// Interaction logic for Window_error.xaml
    /// </summary>
    public partial class Window_error : Window {
        public ErrorViewModel ViewModel { get; set; }

        public Window_error(string errorString) {
            ViewModel = new ErrorViewModel(errorString);
            DataContext = ViewModel;
            InitializeComponent();
        }
    }
}
