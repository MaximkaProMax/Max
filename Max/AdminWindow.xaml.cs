using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Max.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks; // Добавлено для асинхронных методов

namespace Max
{
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
            LoadUsers();
        }

        private async void LoadUsers()
        {
            try
            {
                using (var context = new HotelManagementContext())
                {
                    var users = await context.Users.ToListAsync();
                    UsersListBox.Items.Clear();
                    foreach (var user in users)
                    {
                        var userItem = new StackPanel
                        {
                            Orientation = Orientation.Horizontal,
                            Margin = new Thickness(5)
                        };

                        var textBlock = new TextBlock
                        {
                            Text = $"{user.Lastname}, {user.Firstname} ({user.Username}) - {user.Email} — ",
                            VerticalAlignment = VerticalAlignment.Center
                        };

                        var checkBox = new CheckBox
                        {
                            Content = "Заблокирован",
                            IsChecked = user.IsLocked,
                            Tag = user.Id,
                            VerticalAlignment = VerticalAlignment.Center
                        };
                        checkBox.Click += LockUserCheckbox_Click;

                        userItem.Children.Add(textBlock);
                        userItem.Children.Add(checkBox);
                        UsersListBox.Items.Add(userItem);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при загрузке пользователей: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OpenAddEmployeeWindow(object sender, RoutedEventArgs e)
        {
            var addEmployeeWindow = new AddEmployeeWindow();
            addEmployeeWindow.ShowDialog();
            LoadUsers();
        }

        private void OpenChangePasswordWindow(object sender, RoutedEventArgs e)
        {
            if (UsersListBox.SelectedItem is StackPanel selectedItem)
            {
                var checkBox = selectedItem.Children.OfType<CheckBox>().FirstOrDefault();
                if (checkBox != null)
                {
                    var userId = (int)checkBox.Tag;
                    var changePassword= new ChangePassword(userId);
                    changePassword.ShowDialog();
                    LoadUsers();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите пользователя для изменения пароля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void DeleteUserButton_Click(object sender, RoutedEventArgs e)
        {
            if (UsersListBox.SelectedItem is StackPanel selectedItem)
            {
                var checkBox = selectedItem.Children.OfType<CheckBox>().FirstOrDefault();
                if (checkBox != null)
                {
                    var userId = (int)checkBox.Tag;

                    var result = MessageBox.Show("Вы уверены, что хотите удалить этого пользователя?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        try
                        {
                            using (var context = new HotelManagementContext())
                            {
                                var user = await context.Users.FirstOrDefaultAsync(u => u.Id == userId);
                                if (user != null)
                                {
                                    context.Users.Remove(user);
                                    await context.SaveChangesAsync();
                                    MessageBox.Show("Пользователь успешно удален.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                                    LoadUsers();
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Произошла ошибка при удалении пользователя: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите пользователя для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void LockUserCheckbox_Click(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox)
            {
                var userId = (int)checkBox.Tag;
                bool isLocked = checkBox.IsChecked ?? false;
                await UpdateUserLockStatus(userId, isLocked);
                LoadUsers();
            }
        }

        private async Task UpdateUserLockStatus(int userId, bool isLocked)
        {
            try
            {
                using (var context = new HotelManagementContext())
                {
                    var user = await context.Users.FirstOrDefaultAsync(u => u.Id == userId);
                    if (user != null)
                    {
                        user.IsLocked = isLocked;
                        await context.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при обновлении статуса блокировки: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}