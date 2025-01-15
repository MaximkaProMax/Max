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
            string lastName = LastNameTextBox.Text;
            string firstName = FirstNameTextBox.Text;
            string username = UsernameTextBox.Text;
            string role = RoleComboBox.Text;
            string email = EmailTextBox.Text;
            string phone = PhoneTextBox.Text;
            string password = PasswordBox.Password;
            int failedLoginAttempts = 0;
            bool isLocked = IsLockedCheckBox.IsChecked ?? false;
            bool isFirstLogin = true;

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
                MessageBox.Show($"Произошла ошибка при добавлении сотрудника: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BackToAdminPanelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}