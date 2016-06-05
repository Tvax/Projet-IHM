using Projet.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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

        private void Button_Click(object sender, RoutedEventArgs e) {
            Close();
        }
    }
}
