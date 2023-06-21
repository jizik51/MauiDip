using MauiApp3.MVVM.ViewModel;
using MauiApp3.MVVM.View;
using C1.Maui.Grid;
using MauiApp3.Data;
namespace MauiApp3.MVVM.View;

public partial class UsersListPage : ContentPage
{
    private readonly UsersViewModel _usersViewModel;
    private DatabaseContext _databaseContext = new DatabaseContext();
    public readonly FlexGrid _grid;

    private object _originalValue;
    private object _oldUserName;


    public UsersListPage(UsersViewModel viewModel, AddUserViewModel addUserViewModel, DeleteUserViewModel deleteUserViewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _usersViewModel = viewModel;
        _grid = grid;
        deleteUserViewModel.SetGrid(_grid);
        addUserViewModel.SetGrid(_grid);
        _usersViewModel.LoadDataAsync(_grid);
    }

    private void OnBeginningEdit(object sender, GridCellEditEventArgs e)
    {
        _originalValue = grid[e.CellRange.Row, e.CellRange.Column];
        _oldUserName = grid[e.CellRange.Row, 0];
    }
    private void OnCellEditEnded(object sender, GridCellEditEventArgs e)
    {
        var originalValue = _originalValue?.ToString().Replace(" ", "");
        var currentValue = grid[e.CellRange.Row, e.CellRange.Column].ToString().Replace(" ", "");
        var name = grid[e.CellRange.Row, 0].ToString().Replace(" ", "");
        var pass = grid[e.CellRange.Row, 1].ToString().Replace(" ", "");
        var mail = grid[e.CellRange.Row, 2].ToString().Replace(" ", "");
        var phone = grid[e.CellRange.Row, 3].ToString().Replace(" ", "");
        var role = grid[e.CellRange.Row, 4].ToString().Replace(" ", "");

        grid[e.CellRange.Row, 0] = name;
        grid[e.CellRange.Row, 1] = pass;
        grid[e.CellRange.Row, 2] = mail;
        grid[e.CellRange.Row, 3] = phone;
        grid[e.CellRange.Row, 4] = role;
        //if (originalValue == null)
        //{
        //    grid[e.CellRange.Row, e.CellRange.Column] = currentValue;
        //    return;
        //}
        if (!e.CancelEdits && (originalValue == null && currentValue != null || !originalValue.Equals(currentValue)))
        {
            DisplayAlert("Confirmation", "Do you want to commit the Edit?", "Apply", "Cancel").ContinueWith(async t =>
            {
                if (t.Result)
                {

                    var gr = grid[e.CellRange.Row, e.CellRange.Column];
                    grid[e.CellRange.Row, e.CellRange.Column] = currentValue;
                    await _usersViewModel.SetUppdateUsersData(name, pass, mail, phone, role, _oldUserName, this, e.CellRange.Row, e.CellRange.Column);
                }
                else
                {
                    grid[e.CellRange.Row, e.CellRange.Column] = originalValue;
                }
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }
    }

}