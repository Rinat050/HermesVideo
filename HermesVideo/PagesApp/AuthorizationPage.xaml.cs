using HermesVideo.Properties;
using System;
using System.Collections.Generic;
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

namespace HermesVideo.PagesApp
{
    /// <summary>
    /// Interaction logic for AuthorizationPage.xaml
    /// </summary>
    public partial class AuthorizationPage : Page
    {
        private int _loginCount = 0;

        public AuthorizationPage()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(tbLogin.Text)
                    || string.IsNullOrEmpty(tbPassword.Password))
                {
                    MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (_loginCount > 3)
                {
                    MessageBox.Show("Вы заблокированы на 1 минуту за неправильный ввод данных!", "Блокировка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var user = App.Connection.User
                    .FirstOrDefault(x => x.Login == tbLogin.Text && x.Password == tbPassword.Password);

                if (user == null)
                {
                    MessageBox.Show("Пользователь не найден!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    _loginCount++;

                    if (_loginCount > 3)
                    {
                        Settings.Default.BlockDateTime = DateTime.Now;
                        Settings.Default.Save();
                    }

                    return;
                }

                if (cbRemember.IsChecked == true)
                {
                    Settings.Default.RememberUserLogin = tbLogin.Text;
                }
                else
                {
                    Settings.Default.RememberUserLogin = null;
                }

                Settings.Default.Save();
                NavigationService.Navigate(new MainPage());
            }
            catch
            {
                MessageBox.Show("Произошла ошибка!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            tbLogin.Text = Settings.Default.RememberUserLogin;

            var block = Settings.Default.BlockDateTime;

            if (block != null)
            {
                if (block.AddMinutes(1) < DateTime.Now)
                {
                    _loginCount = 0;
                }
                else
                {
                    _loginCount = 4;
                }
            }
        }
    }
}
