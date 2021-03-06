﻿using BarboraTimeCheck.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BarboraTimeCheck
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SettingsService settingsService;
        BarboraService barboraService;
        public MainWindow()
        {
            settingsService = new SettingsService();
            barboraService = new BarboraService();
            InitializeComponent();
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow settingsWindow = new SettingsWindow();
            settingsWindow.Show();
            this.Close();
        }

        private void Refresh_click(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            var settings = settingsService.GetSettings();
            settingsService.CleatAuthInformation();

            var authCookie = barboraService.Login(settings.Username, settings.Password);
            settingsService.UpdateAuthInformation(settings.Username, settings.Password, authCookie);

            Mouse.OverrideCursor = Cursors.Arrow;
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            settingsService.CleatAuthInformation();
            var loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }
    }
}
