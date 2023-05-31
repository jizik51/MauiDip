using MauiApp3.MVVM.ViewModel;
using MauiApp3.MVVM.View;
using C1.Maui.Grid;

namespace MauiApp3.MVVM.View;

public partial class UsersListPage : ContentPage
{
    private readonly UsersViewModel _usersViewModel;
    public UsersListPage(UsersViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _usersViewModel = viewModel;
        LoadDataAsync();
    }
    private async void LoadDataAsync()
    {
        var data = await _usersViewModel.GetAllUsers();
        grid.ItemsSource = data;
    }
    private object _originalValue;
    private object _oldName;

    private void OnBeginningEdit(object sender, GridCellEditEventArgs e)
    {
        _originalValue = grid[e.CellRange.Row, e.CellRange.Column];
        _oldName = grid[e.CellRange.Row, 0];
    }
    private void OnCellEditEnded(object sender, GridCellEditEventArgs e)
    {
        var originalValue = _originalValue;
        var currentValue = grid[e.CellRange.Row, e.CellRange.Column];
        if (!e.CancelEdits && (originalValue == null && currentValue != null || !originalValue.Equals(currentValue)))
        {
            DisplayAlert("Confirmation", "Do you want to commit the Edit?", "Apply", "Cancel").ContinueWith(t =>
            {
                if (t.Result)
                {
                    grid[e.CellRange.Row, e.CellRange.Column] = currentValue; 
                    UpdateUser(currentValue);
                }
                else
                {
                    grid[e.CellRange.Row, e.CellRange.Column] = originalValue;
                }
            }, TaskScheduler.FromCurrentSynchronizationContext()); 
        }
    }

    private async void UpdateUser(object newValue)
    {
        await _usersViewModel.SetUppdateUsersData(newValue, _oldName);

    }
}