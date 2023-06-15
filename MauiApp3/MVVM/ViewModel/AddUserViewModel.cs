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
        private readonly UsersListPage _usersListPage;
        [ObservableProperty]
        private ObservableCollection<Users> _users = new();

        [ObservableProperty]
        private Users _operatingUsers = new();

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
                //Добавить переход на главную
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
