using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.IO;

namespace Projet {
    /// <summary>
    /// Logique d'interaction pour Window_modify.xaml
    /// </summary>
    public partial class Window_modify : Window {

        private Emote emot;
        public Image default_image;

        public Emote Emote {
            get { return emot; }
            set { emot = value; }

        }

        public Window_modify(Emote e) {
            InitializeComponent();
            emot = new Emote();
            emot = e;

            if (emot != null) {
                Mod_Nom.Text = emot.Nom;
                Mod_Desc.Text = emot.Description;
            }
            if (emot.image.Source != null) {
                Mod_Image.Source = emot.image.Source;
            }
            else{
                default_image.Source = new BitmapImage(new Uri(Path.GetFullPath("Images\black_cross.png")));
                Mod_Image.Source = default_image.Source;
                }
                
            Emote = e;

        }
        
        //Button OK
        private void Button_OK(object sender, RoutedEventArgs e) {

            emot.Nom = Mod_Nom.Text;
            emot.Description = Mod_Desc.Text;

            Close();
        }

        //Button Cancel
        private void Button_Cancel(object sender, RoutedEventArgs e) {
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
                Mod_Image.Source = new BitmapImage(new Uri(op.FileName));
                Emote.image.Source = new BitmapImage(new Uri(op.FileName));
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter) {
                Button_OK(this, new RoutedEventArgs());
            }
            if (e.Key == Key.Escape) {
                Button_Cancel(this, new RoutedEventArgs());
            }
        }
    }
}
