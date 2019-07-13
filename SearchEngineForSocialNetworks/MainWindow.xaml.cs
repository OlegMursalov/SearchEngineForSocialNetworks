using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SearchEngineForSocialNetworks.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            var driver = new ChromeDriver();
            this.Driver = driver;
            this.JScript = new JScript(driver);
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(EmailAddress.Text))
            {
                Driver.Navigate().GoToUrl("https://www.facebook.com/");
                JScript.AuthorizationFacebookCommand("sadfasdf", "sadgdfg");
            }
            else
            {
                MessageBox.Show("Please, enter email address");
            }
        }
    }
}