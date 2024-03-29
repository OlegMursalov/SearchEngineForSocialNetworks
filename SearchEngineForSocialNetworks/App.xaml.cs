﻿using System.Windows;

namespace SearchEngineForSocialNetworks
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private MainWindow mainWindow;

        protected override void OnStartup(StartupEventArgs e)
        {
            mainWindow = new MainWindow();
            mainWindow.Show();
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            /*if (mainWindow != null)
            {
                mainWindow.Driver.Dispose();
            }*/
        }
    }
}
