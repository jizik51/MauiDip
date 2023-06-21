using CommunityToolkit.Mvvm.ComponentModel;
using MauiApp3.Data;
using MauiApp3.MVVM.Model;
using System.Collections.ObjectModel;
using C1.Maui.Grid;
using MauiApp3.MVVM.View;
using CommunityToolkit.Mvvm.Input;

namespace MauiApp3.MVVM.ViewModel
{
    public partial class AddUserViewModel : ObservableObject
    {
        private readonly DatabaseContext _context;
        private FlexGrid _grid;

        [ObservableProperty]
        private ObservableCollection<Users> _users = new();

        [ObservableProperty]
        private Users _operatingUsers = new();


        private string _selectedRole;

        public string SelectedRole
        {
            get => _selectedRole;
            set
            {
                if (_selectedRole != value)
                {
                    _selectedRole = value;
                    OnPropertyChanged();
                }
            }
        }
        public AddUserViewModel(DatabaseContext context)
        {
            _context = context;

        }
        public void SetGrid(FlexGrid grid)
        {
            _grid = grid;
        }

        [RelayCommand]
        public async Task AddNewUser()
        {
            try
            {
                if (string.IsNullOrEmpty(OperatingUsers.UserName) || string.IsNullOrEmpty(OperatingUsers.Password) ||
                    string.IsNullOrEmpty(_selectedRole) || string.IsNullOrEmpty(OperatingUsers.Mail) ||
                    string.IsNullOrEmpty(OperatingUsers.Phone) ||
                    string.IsNullOrWhiteSpace(OperatingUsers.UserName) || string.IsNullOrWhiteSpace(OperatingUsers.Password) || 
                    string.IsNullOrWhiteSpace(OperatingUsers.Mail) ||
                    string.IsNullOrWhiteSpace(OperatingUsers.Phone))
                {
                    throw new Exception("Invalid user data. Please fill in all required fields.");
                }
                OperatingUsers.UserName = OperatingUsers.UserName?.Replace(" ", "");
                OperatingUsers.Password = OperatingUsers.Password?.Replace(" ", "");
                OperatingUsers.Role = _selectedRole.ToString();
                OperatingUsers.Mail = OperatingUsers.Mail?.Replace(" ", "");
                OperatingUsers.Phone = OperatingUsers.Phone?.Replace(" ", "");

                var isAdded = await _context.CheckUser(OperatingUsers);
                if (isAdded)
                {
                    await App.Current.MainPage.DisplayAlert("Error", "This user name is taken", "Ok");
                    return;
                }
                else
                {
                    await _context.AddItemAsync(OperatingUsers);
                    UsersViewModel viewModel = new UsersViewModel(_context);
                    var data = await GetAllUsers();
                    _grid.ItemsSource = data;
                    viewModel.LoadDataAsync(_grid);
                    await App.Current.MainPage.DisplayAlert("User added", "Added", "Ok");
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", "User or password incorrect", "Ok");
                Console.WriteLine("Error: " + ex.Message);
            }
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
