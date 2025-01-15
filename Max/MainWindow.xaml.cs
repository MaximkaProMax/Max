﻿using System.Diagnostics.Eventing.Reader;
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
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            string login = Username.Text.Trim();
            string password = Password.Password.Trim();

            // Проверка поля заполнены
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Пожалуйста, введите логин и пароль.");
                return;
            }

            using (var context = new HotelManagementContext())
            {
                var user = await context.Users
                    .Where(u => u.Username == login)
                    .FirstOrDefaultAsync();

                // Проверка существования пользователя
                if (user == null)
                {
                    MessageBox.Show("Неправильный логин или пароль.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Проверка на блокировку пользователя
                if (user.IsLocked ?? false)
                {
                    MessageBox.Show("Вы заблокированы. Обратитесь к администратору.", "Доступ запрещен", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Проверка на длительное отсутствие входа
                if (user.LastLoginDate.HasValue && (DateTime.Now - user.LastLoginDate.Value).TotalDays > 30 && user.Role != "Admin")
                {
                    user.IsLocked = true;
                    await context.SaveChangesAsync();
                    MessageBox.Show("Ваша учетная запись заблокирована из-за длительного отсутствия входа. Обратитесь к администратору.", "Доступ запрещен", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Проверка пароля
                if (user.Password == password)
                {
                    user.LastLoginDate = DateTime.Now;

                    // Сброс неудачных попыток
                    user.FailedLoginAttempts = 0;
                    await context.SaveChangesAsync();

                    MessageBox.Show("Вы успешно авторизовались!", "Добро пожаловать", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Скрываем основное окно перед показом окна смены пароля
                    var changePasswordWindow = new ChangePassword(user.Id);
                    this.Hide();
                    changePasswordWindow.ShowDialog();

                    if (changePasswordWindow.DialogResult == true)
                    {
                        // Проверка первого входа
                        if (user.IsFirstLogin == true)
                        {
                            user.IsFirstLogin = false;
                            await context.SaveChangesAsync();
                        }

                        // Открываем соответствующее окно в зависимости от роли пользователя
                        if (user.Role == "Admin")
                        {
                            var adminWindow = new AdminWindow();
                            adminWindow.Show();
                        }
                        else
                        {
                            var mainWindow = new MainWindow();
                            mainWindow.Show();
                        }
                    }

                    this.Close(); // Закрываем MainWindow после смены пароля или открытия другого окна
                }
                else
                {
                    // Увеличение счетчика неудачных попыток
                    user.FailedLoginAttempts++;

                    if (user.FailedLoginAttempts >= 3)
                    {
                        user.IsLocked = true;
                        MessageBox.Show("Вы заблокированы после 3 неудачных попыток. Обратитесь к администратору.", "Доступ запрещен", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        int attemptsLeft = 3 - (user.FailedLoginAttempts ?? 0);
                        MessageBox.Show($"Неправильный логин или пароль. Осталось попыток: {attemptsLeft}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    await context.SaveChangesAsync();
                }
            }
        }
    }
}