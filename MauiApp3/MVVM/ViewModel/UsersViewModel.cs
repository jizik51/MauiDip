using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp3.Data;
using MauiApp3.MVVM.Model;
using MauiApp3.MVVM.View;
using System.Collections.ObjectModel;
using C1.Maui.Grid;

namespace MauiApp3.MVVM.ViewModel
{
    public partial class UsersViewModel : ObservableObject
    {
        private readonly DatabaseContext _context;
        private readonly AddUserViewModel addUserViewModel;
        private FlexGrid _grid;


        [ObservableProperty]
        private ObservableCollection<Users> _users = new();

        [ObservableProperty]
        private Users _operatingUsers = new();

        private ObservableCollection<Users> _usersList = new();

        private bool _isChecked;
        public bool IsChecked
        {
            get { return _isChecked; }
            set { SetProperty(ref _isChecked, value); }
        }
        public UsersViewModel(DatabaseContext context)
        {
            _context = context;
        }

        //private void SetOperatingUsers(Users? users) => OperatingUsers = users ?? new();

        public void SetGrid(FlexGrid grid)
        {
            _grid = grid;
        }
        private bool IsValidData(Users users)
        {
            if (users.UserName == string.Empty || users.UserName == null || users.UserName.Contains(" "))
            {
                App.Current.MainPage.DisplayAlert("Unsuccess", "Login uncessful, incorrect name ", "OK");
                return false;
            }

            if (users.Password == string.Empty || users.Password == null || users.Password.Contains(" "))
            {

                App.Current.MainPage.DisplayAlert("Unsuccess", "Login uncessful, incorrect pass ", "OK");
                return false;
            }
            return true;
        }
        [RelayCommand]
        private void ClearData()
        {
            OperatingUsers.UserName = string.Empty;
            OperatingUsers.Password = string.Empty;
        }

        [RelayCommand]
        private async Task CreateUser()
        {
            if (IsValidData(OperatingUsers) == false) return;
            await _context.AddItemAsync(OperatingUsers);
            Users.Add(OperatingUsers);
            await App.Current.MainPage.DisplayAlert("Success", "Registration cessful", "Ok");
        }

        public async void LoadDataAsync(FlexGrid grid) 
        {
            var data = await GetAllUsers();
            grid.ItemsSource = data;
           
        }

        public async Task SetUppdateUsersData(object newValue, object oldValue)
        {
            //OperatingUsers.UserName
            //OperatingUsers.Password

            await _context.UpdateUser(newValue.ToString(), oldValue.ToString());
        }

        [RelayCommand]
        private async void LoadAddUserPage()
        {
            await Shell.Current.GoToAsync($"{nameof(AddUserPage)}");
        }

        [RelayCommand]
        private async Task LogginUser()
        {
            if (IsValidData(OperatingUsers) == false) return;
            var res = await _context.GetUsersIdAsync(OperatingUsers);
            if (_isChecked)
            {
                Preferences.Set("id", res.Id);
                Preferences.Set("UserName", res.UserName);
                Preferences.Set("Password", res.Password);
                Preferences.Set("AutoLogin", true);
                await Shell.Current.GoToAsync($"{nameof(MainPage)}");
                //await Navigate();
                //await App.Current.MainPage.DisplayAlert("Log", $"{res.UserName}", "Ok");
            }
            else
            {
                Preferences.Default.Set("id", String.Empty);
                Preferences.Default.Set("UserName", String.Empty);
                Preferences.Default.Set("Password", String.Empty);
                Preferences.Default.Set("AutoLogin", false);
                await Shell.Current.GoToAsync($"{nameof(MainPage)}");
                //await Navigate();
                //await App.Current.MainPage.DisplayAlert("Success", $"{res.UserName}", "Ok");
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


        [RelayCommand]
        private Task Navigate()
        {
            MainPage mainPage = Application.Current.MainPage as MainPage;
            if (mainPage != null)
            {
                return Task.CompletedTask;
            }
            else
            {
                mainPage = new MainPage(this);
                Application.Current.MainPage = new NavigationPage(mainPage);
                return Task.CompletedTask;
            }
        }


        [RelayCommand]
        private Task Back()
        {
            Preferences.Default.Set("id", String.Empty);
            Preferences.Default.Set("UserName", String.Empty);
            Preferences.Default.Set("Password", String.Empty);
            Preferences.Default.Set("AutoLogin", false);

            MainPage mainPage = Application.Current.MainPage as MainPage;
            if (mainPage != null)
            {
                mainPage.Navigation.PushAsync(new LoginPageViewModel(this));
                Application.Current.MainPage = new LoginPageViewModel(this);
                return Task.CompletedTask;
            }
            else
            {
                var usersViewModel = new UsersViewModel(_context);
                var loginPage = new LoginPageViewModel(this);
                Application.Current.MainPage = loginPage;
                return Task.CompletedTask;
            }
            //return Shell.Current.GoToAsync(nameof(LoginPageViewModel));
        }




    }
}
