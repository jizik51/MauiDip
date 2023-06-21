using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using MauiApp3.Data;
using MauiApp3.MVVM.Model;
using C1.Maui.Grid;
using CommunityToolkit.Mvvm.Input;
using MauiApp3.MVVM.View;

namespace MauiApp3.MVVM.ViewModel
{
    public partial class RegUserViewModel : ObservableObject
    {

        private readonly DatabaseContext _context;
        private FlexGrid _grid;

        [ObservableProperty]
        private ObservableCollection<Users> _users = new();

        [ObservableProperty]
        private Users _operatingUsers = new();

        public RegUserViewModel(DatabaseContext context)
        {
            _context = context;
        }

        [RelayCommand]
        public async Task RegNewUser()
        {
            try
            {
                OperatingUsers.Role = "users";
                await _context.AddItemAsync(OperatingUsers);
                UsersViewModel viewModel = new UsersViewModel(_context);
                var data = await GetAllUsers();
                await Shell.Current.GoToAsync($"{nameof(LoginPageViewModel)}");
            }
            catch (Exception ex)
            {
            }
        }

        [RelayCommand]
        public async void OpenLoginPage()
        {
            await Shell.Current.GoToAsync($"{nameof(LoginPageViewModel)}");
        }
        public async Task<ObservableCollection<Users>> GetAllUsers()
        {
            var users = await _context.GetAllUsers();
            if (users is not null)
            {
                Users ??= new ObservableCollection<Users>();
                foreach (var user in users)
                {
                    Users.Add(user);

                }
            }
            return Users;
        }
    }
}
