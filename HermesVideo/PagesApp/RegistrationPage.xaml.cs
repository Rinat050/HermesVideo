using HermesVideo.ADOApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }

        private void BtnRegistrate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(tbLogin.Text)
                   || string.IsNullOrEmpty(tbPassword.Password))
                {
                    MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var user = App.Connection.User
                    .FirstOrDefault(x => x.Login == tbLogin.Text);

                if (user != null)
                {
                    MessageBox.Show("Пользователь с таким логином уже существует!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!IsPasswordCorrect(tbPassword.Password))
                {
                    MessageBox.Show("Пароль должен отвечать следующим требованиям: \n" +
                                    "- Минимум 6 символов \n" +
                                    "- Минимум 1 прописная буква \n" +
                                    "- Минмум 1 цифра \n" +
                                    "- Минимум один символ из набора: ! @ # $ % ^", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var newUser = new User()
                {
                    Login = tbLogin.Text,
                    Password = tbPassword.Password,
                };

                App.Connection.User.Add(newUser);
                App.Connection.SaveChanges();

                MessageBox.Show("Успешная регистрация!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch
            {
                MessageBox.Show("Произошла ошибка!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool IsPasswordCorrect(string password)
        {
            var regexDigit = new Regex(@"\d+");
            var regexSymbol = new Regex(@"[!@#$%^]+");
            var regexUpper = new Regex(@"[A-Z]+");

            return password.Length >= 6 &&
                regexDigit.IsMatch(password) &&
                regexSymbol.IsMatch(password) &&
                regexUpper.IsMatch(password);
        }
    }
}
