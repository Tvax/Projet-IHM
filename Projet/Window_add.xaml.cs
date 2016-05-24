using Microsoft.Win32;
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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window {

        private Emote emot;

        public Emote Emote {
            get { return emot; }
            set { emot = value; }

        }
        public Window1() {
            InitializeComponent();
            emot = new Emote();
            emot = null;
        }

        //Button OK
        private void Button_Click(object sender, RoutedEventArgs e) {
            emot.Nom = Add_Nom.Text;
            emot.Description = Add_Desc.Text;
            Close();
        }

        //Button Cancel
        private void Button_Click_1(object sender, RoutedEventArgs e) {
            Close();
        }

        //Button Load
        private void Button_Click_2(object sender, RoutedEventArgs e) {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true) {
                Add_Image.Source = new BitmapImage(new Uri(op.FileName));
                Emote.image.Source = new BitmapImage(new Uri(op.FileName));
            }
        }

        public Window1(Emote e) {
            InitializeComponent();
            Emote = e;
        }
                
        private void View_add_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter) {
                Button_Click(this, new RoutedEventArgs());
            }
            if (e.Key == Key.Escape) {
                Button_Click_1(this, new RoutedEventArgs());
            }
        }
         
    }
}
