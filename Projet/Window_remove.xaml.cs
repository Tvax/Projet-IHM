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
    /// Logique d'interaction pour Window_remove.xaml
    /// </summary>
    public partial class Window_remove : Window {

        private bool res1;

        public bool Res1 {
            get { return res1; }
            set { res1 = value; }
        }

        public Window_remove() {
            InitializeComponent();
            res1 = new bool();
        }

        //Button NO
        private void No_Click(object sender, RoutedEventArgs e) {
            res1 = false;
            Close();
        }

        //Button Yes
        private void Yes_Click(object sender, RoutedEventArgs e) {
            res1 = true;
            Close();
        }

        public Window_remove(bool res) {
            InitializeComponent();
            res1 = res;
        }

        private void View_remove_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter) {
                Yes_Click(this, new RoutedEventArgs());
            }
            if (e.Key == Key.Escape) {
                No_Click(this, new RoutedEventArgs());
            }
        }


    }
}
