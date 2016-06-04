using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.IO;
using Projet.Modeles;
using Projet.ViewModels;

namespace Projet {
    /// <summary>
    /// Logique d'interaction pour Window_modify.xaml
    /// </summary>
    public partial class Window_modify : Window {

        public AddEmoteViewModel _viewModel;


        public Window_modify(Emote emot) {
            InitializeComponent();
            _viewModel = new AddEmoteViewModel(emot);
            DataContext = _viewModel;
        }
    }
}

