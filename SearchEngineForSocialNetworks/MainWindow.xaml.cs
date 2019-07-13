using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SearchEngineForSocialNetworks.Common;
using System.Configuration;
using System.Windows;

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

        private async void Search_Click(object sender, RoutedEventArgs e)
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
        }
    }
}