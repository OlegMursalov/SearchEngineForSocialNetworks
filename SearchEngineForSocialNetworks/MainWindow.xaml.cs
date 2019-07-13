using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Configuration;
using System.IO;
using System.Net;
using System.Windows;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;

namespace SearchEngineForSocialNetworks
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal IWebDriver Driver { get; }
        internal JScript JScript { get; }

        public MainWindow()
        {
            InitializeComponent();
            /*var driver = new ChromeDriver();
            this.Driver = driver;
            this.JScript = new JScript(driver);*/
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.Filter = "Text documents (*.json)|*.json";
            var result = dialog.ShowDialog();
            if (result.HasValue && result.Value)
            {
                string filename = dialog.FileName;
                /*var text = Common.GetTextFromEmbededResource("emails.json");*/
                var text = File.ReadAllText(filename);
                var emailList = JsonConvert.DeserializeObject<EmailList>(text);
                var searcher = new InstagramSearcher(emailList.Emails);
                var instagramUsers = searcher.Process();
                if (instagramUsers.Count > 0)
                {
                    foreach (var user in instagramUsers)
                    {
                        MainDataGrid.Items.Add(user);
                    }
                }
            }
        }

        /*private void Search_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(EmailAddress.Text))
            {
                var email = EmailAddress.Text;
                var loginOfFacebook = ConfigurationManager.AppSettings["LoginOfFacebook"];
                var passOfFacebook = ConfigurationManager.AppSettings["PassOfFacebook"];
                if (!string.IsNullOrEmpty(loginOfFacebook) && !string.IsNullOrEmpty(passOfFacebook))
                {
                    Driver.Navigate().GoToUrl("https://www.facebook.com/");
                    JScript.AuthorizationFacebookCommand(loginOfFacebook, passOfFacebook);
                    JScript.SearchUsersInFacebookByEmail(email);
                    var facebookUsers = JScript.ParseInformationAboutUser();
                }
                else
                {
                    MessageBox.Show("Please, fill App.config (LoginOfFacebook, PassOfFacebook)");
                }
            }
            else
            {
                MessageBox.Show("Please, enter email address");
            }
        }*/
    }
}