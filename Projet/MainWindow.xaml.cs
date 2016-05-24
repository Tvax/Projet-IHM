using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;


//Changer truc Kappa and co
//Rajouter croix rouge sur window error
//Ajouter image par defaut!

namespace Projet {
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        private Emote emot;
        private Emote emot1;
        //private Images default_image;

        public ObservableCollection<Emote> ListeEmotes;

        public Emote Emote {
            get { return emot; }
            set { emot = value; }
        }

        public MainWindow() {
            InitializeComponent();

            emot = new Emote();
            emot.Nom = "Emote";
            emot.Description = "Emote Description";
            emot.image.Source = new BitmapImage(new Uri(Path.GetFullPath("Kappa.png")));

            emot1 = new Emote();
            emot1.Nom = "Emote 2";
            emot1.Description = "Emote 2 Description";

            ListeEmotes = new ObservableCollection<Emote>();

            ListeEmotes.Add(emot);
            ListeEmotes.Add(emot1);
            List.ItemsSource = ListeEmotes;

            //default_image.default_image.Source = new BitmapImage(new Uri(@"C:\Users\Teiva\Pictures\Twitch\Bannieres + PP\OFFLINE.png"));
        }

        //Button Add
        private void Add_Click(object sender, RoutedEventArgs e) {
            Emote em = new Projet.Emote();
            Window1 Add = new Window1(em);
            Add.ShowDialog();

            if (em.Nom != null) {
                ListeEmotes.Add(em);
            }
        }

        //Button Modify
        private void Modify_Click(object sender, RoutedEventArgs e) {
            Emote em = List.SelectedItem as Emote;

            if (em != null) {
                Window_modify Mod = new Window_modify(em);
                Mod.ShowDialog();
            }
            else {
                Window_not_selected w = new Window_not_selected();
                w.ShowDialog();
                        }
        }

        //Item selected in the list
        private void List_SelectionChanged(object sender, SelectionChangedEventArgs e) {

            Emote e1 = List.SelectedItem as Emote;
            if (e1 != null) {
                Nom.Content = e1.Nom;
                Description.Content = e1.Description;
                img.Source = e1.image.Source;

                if (e1.image.Source == null) {
                    img.Source = new BitmapImage(new Uri(Path.GetFullPath("favicon.ico")));
                }
            }

        }

        //Button Remove
        private void Remove_Click(object sender, RoutedEventArgs e) {
            Emote e1 = List.SelectedItem as Emote;
            if (e1 != null) {

                bool res = true;
                Window_remove r = new Window_remove(res);
                r.ShowDialog();

                if (res == true) {
                    Nom.Content = " ";
                    Description.Content = " ";
                    img.Source = new BitmapImage(new Uri(Path.GetFullPath("favicon.ico")));
                    ListeEmotes.Remove(e1);
                }
            }
            else {
                Window_not_selected w = new Window_not_selected();
                w.ShowDialog();
            }
            //List.SelectedIndex = 0;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.A) {
                Add_Click(this, new RoutedEventArgs());
            }
            if (e.Key == Key.M) {
                Modify_Click(this, new RoutedEventArgs());
            }
            if (e.Key == Key.D || e.Key == Key.R) {
                Remove_Click(this, new RoutedEventArgs());
            }
        }
    }

}