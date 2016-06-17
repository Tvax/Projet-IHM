﻿using System.Windows;
using Projet.ViewModels;

namespace Projet {
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private ListEmoteViewModel _viewModel;

        public MainWindow() {
            InitializeComponent();
            _viewModel = new ListEmoteViewModel();
            DataContext = _viewModel;
        }
    }
}