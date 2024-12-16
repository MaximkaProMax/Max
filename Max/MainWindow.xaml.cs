using System.Diagnostics.Eventing.Reader;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Max.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Max
{
    /// <summary>
    /// Взаимодействие логики для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent(); // Инициализация компонентов интерфейса окна
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            string login = Username.Text; // Получение текста из поля ввода логина
            string password = Password.Text; // Получение текста из поля ввода пароля

            // Проверка, что поля логина и пароля не пусты
            if (login.IsNullOrEmpty() || password.IsNullOrEmpty())
            {
                // Отображение сообщения об ошибке, если одно из полей пустое
                MessageBox.Show("Значение обоих полей должны быть заполнены!");
                return;
            }

            // Удаление лишних пробелов из логина и пароля
            login = login.Trim();
            password = password.Trim();

            // Использование контекста базы данных для поиска пользователя
            using (var context = new MaxAContext())
            {
                // Выполнение асинхронного запроса к базе данных для поиска пользователя с указанным логином и паролем
                var user = await context.Users
                    .Where(u => u.Name == login && u.Password == password)
                    .FirstOrDefaultAsync();

                // Проверка, найден ли пользователь
                if (user != null)
                {
                    // Если пользователь найден, отображается сообщение об успешной авторизации
                    MessageBox.Show("Вы успешно авторизовались!");
                }
                else
                {
                    // Если пользователь не найден, отображается сообщение об ошибке
                    MessageBox.Show("Вы ввели неправильные логин и пароль. Проверьте введенные данные и попробуйте еще раз.");
                }
            }
        }
    }
}