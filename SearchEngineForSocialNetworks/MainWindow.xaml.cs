using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Configuration;
using System.IO;
using System.Net;
using System.Windows;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Media;

namespace SearchEngineForSocialNetworks
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Search_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.Filter = "Text documents (*.json)|*.json";
            var result = dialog.ShowDialog();
            if (result.HasValue && result.Value)
            {
                string filename = dialog.FileName;
                var text = File.ReadAllText(filename);
                try
                {
                    Search.IsEnabled = false;
                    Search.Content = "Please, wait...";
                    Background = new SolidColorBrush(Colors.DarkSalmon);
                    var emailList = JsonConvert.DeserializeObject<EmailList>(text);
                    var instagramSearcher = new InstagramSearcher(emailList.Emails);
                    var instagramUsers = await Task.Run(() => instagramSearcher.Process());
                    if (instagramUsers != null && instagramUsers.Count > 0)
                    {
                        foreach (var user in instagramUsers)
                        {
                            InstagramDataGrid.Items.Add(user);
                        }
                    }
                    var facebookSearcher = new FacebookSearcher(emailList.Emails);
                    var facebookUsers = await Task.Run(() => facebookSearcher.Process());
                    if (facebookUsers != null && facebookUsers.Count > 0)
                    {
                        foreach (var user in facebookUsers)
                        {
                            FacebookDataGrid.Items.Add(user);
                        }
                    }
                    var twitterSearcher = new TwitterSearcher(emailList.Emails);
                    var twitterUsers = await Task.Run(() => twitterSearcher.Process());
                    if (twitterUsers != null && twitterUsers.Count > 0)
                    {
                        foreach (var user in twitterUsers)
                        {
                            TwitterDataGrid.Items.Add(user);
                        }
                    }
                }
                catch (JsonSerializationException)
                {
                    MessageBox.Show("File is not correct.");
                }
            }
            Search.IsEnabled = true;
            Search.Content = "Select json file and search";
            Background = new SolidColorBrush(Colors.White);
        }
    }
}