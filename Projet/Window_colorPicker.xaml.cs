using Projet.Modeles;
using Projet.ViewModels;
using System.Windows;

namespace Projet {
    /// <summary>
    /// Interaction logic for Window_colorPicker.xaml
    /// </summary>
    public partial class Window_colorPicker : Window {
        public ColorPickerViewModel ViewModel { get; set; }

        public Window_colorPicker(User user) {
            ViewModel = new ColorPickerViewModel(user);
            DataContext = ViewModel;
            InitializeComponent();
        }
    }
}
