using System;
using System.Windows;
using Max.Models;
using Microsoft.EntityFrameworkCore;

namespace Max
{
    public partial class AddEmployeeWindow : Window
    {
        public AddEmployeeWindow()
        {
            InitializeComponent();
        }

        private async void AddEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            string lastName = LastNameTextBox.Text.Trim();
            string firstName = FirstNameTextBox.Text.Trim();
            string username = UsernameTextBox.Text.Trim();
            string role = RoleComboBox.Text.Trim();
            string email = EmailTextBox.Text.Trim();
            string phone = PhoneTextBox.Text.Trim();
            string password = PasswordBox.Password.Trim();
            int failedLoginAttempts = 0;
            bool isLocked = IsLockedCheckBox.IsChecked ?? false;
            bool isFirstLogin = true;

            // Проверка заполненности полей
            if (string.IsNullOrWhiteSpace(lastName) ||
                string.IsNullOrWhiteSpace(firstName) ||
                string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(role) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(phone) ||
                string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                using (var context = new HotelManagementContext())
                {
                    var user = new User
                    {
                        Lastname = lastName,
                        Firstname = firstName,
                        Username = username,
                        Role = role,
                        Email = email,
                        Phone = phone,
                        Password = password,
                        FailedLoginAttempts = failedLoginAttempts,
                        IsLocked = isLocked,
                        IsFirstLogin = isFirstLogin,
                        LastLoginDate = null
                    };

                    // Добавление пользователя в контекст и сохранение изменений
                    context.Users.Add(user);
                    await context.SaveChangesAsync();

                    MessageBox.Show("Сотрудник успешно добавлен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    Close();
                }
            }
            catch (Exception ex)
            {
                var innerExceptionMessage = ex.InnerException != null ? ex.InnerException.Message : "Нет дополнительной информации";
                MessageBox.Show($"Произошла ошибка при добавлении сотрудника: {ex.Message}\n\nДополнительная информация: {innerExceptionMessage}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BackToAdminPanelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}