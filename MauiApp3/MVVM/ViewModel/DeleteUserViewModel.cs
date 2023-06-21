using C1.Maui.Grid;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp3.Data;
using MauiApp3.MVVM.Model;
using MauiApp3.MVVM.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp3.MVVM.ViewModel
{
    public partial class DeleteUserViewModel : ObservableObject
    {
        private readonly DatabaseContext _context;
        private FlexGrid _grid;


        [ObservableProperty]
        private ObservableCollection<Users> _users = new();

        [ObservableProperty]
        private Users _operatingUsers = new();
        public DeleteUserViewModel(DatabaseContext context)
        {
            _context = context;

        }
        public void SetGrid(FlexGrid grid)
        {
            _grid = grid;
        }

        [RelayCommand]
        public async Task DeleteUser()
        {
            try
            {
                if (OperatingUsers == null || OperatingUsers.UserName.Length > 45 || string.IsNullOrWhiteSpace(OperatingUsers.UserName))
                {
                    throw new Exception("Invalid name");
                }
                var user = await _context.GetUsersIdAsync(OperatingUsers);
                await _context.DeleteUsers(user);
                LoadDataAsync();

            }
            catch (Exception ex)
            {
                string errorMessage = "Invalid user, enter user name field";
                await Application.Current.MainPage.DisplayAlert("Error", errorMessage, "OK");
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        public async void LoadDataAsync()
        {
            var data = await GetAllUsers();
            _grid.ItemsSource = data;
        }

        public async Task<ObservableCollection<Users>> GetAllUsers()
        {
            var users = await _context.GetAllUsers();
            if (users is not null)
            {
                Users ??= new ObservableCollection<Users>();
                if (Users.Count > 0) Users.Clear();
                foreach (var user in users)
                {
                    Users.Add(user);
                }
            }
            return Users;
        }


    }
}
