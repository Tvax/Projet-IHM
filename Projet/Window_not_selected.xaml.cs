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
    /// Logique d'interaction pour Window_not_selected.xaml
    /// </summary>
    public partial class Window_not_selected : Window {
        public Window_not_selected() {
            InitializeComponent();
        }

        //Button OK
        private void Button_Click(object sender, RoutedEventArgs e) {
            Close();
        }

        private void View_remove_KeyDown(object sender, KeyEventArgs e) {

        }

        private void Window_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter) {
                Button_Click(this, new RoutedEventArgs());
            }
            if (e.Key == Key.Escape) {
                Button_Click(this, new RoutedEventArgs());
            }
        }
    }
}
