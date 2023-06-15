using MauiApp3.MVVM.ViewModel;
using MauiApp3.MVVM.View;
using C1.Maui.Grid;

namespace MauiApp3.MVVM.View;

public partial class UsersListPage : ContentPage
{
    private readonly UsersViewModel _usersViewModel;
    private readonly FlexGrid _grid;

    private object _originalValue;
    private object _oldName;


    public UsersListPage(UsersViewModel viewModel, AddUserViewModel addUserViewModel) 
    {
        InitializeComponent();
        BindingContext = viewModel;
        _usersViewModel = viewModel;
        _grid = grid;
        addUserViewModel.SetGrid(_grid);
        _usersViewModel.LoadDataAsync(_grid);
        grid.NewRowPlaceholder = "Click";
    }

    private void OnBeginningEdit(object sender, GridCellEditEventArgs e)
    {
        _originalValue = grid[e.CellRange.Row, e.CellRange.Column];
        _oldName = grid[e.CellRange.Row, 0];
    }
    private void OnCellEditEnded(object sender, GridCellEditEventArgs e)
    {
        var originalValue = _originalValue;
        var currentValue = grid[e.CellRange.Row, e.CellRange.Column];
        if (originalValue == null)
        {
            grid[e.CellRange.Row, e.CellRange.Column] = currentValue;
            return;
        }
        if (!e.CancelEdits && (originalValue == null && currentValue != null || !originalValue.Equals(currentValue)))
        {
            DisplayAlert("Confirmation", "Do you want to commit the Edit?", "Apply", "Cancel").ContinueWith(async t =>
            {
                if (t.Result)
                {
                    grid[e.CellRange.Row, e.CellRange.Column] = currentValue; 
                    await _usersViewModel.SetUppdateUsersData(currentValue, _oldName);
                }
                else
                {
                    grid[e.CellRange.Row, e.CellRange.Column] = originalValue;
                }
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
 
}