using C1.Maui.Grid;
using MauiApp3.MVVM.ViewModel;

namespace MauiApp3.MVVM.View;

public partial class OrdersListPage : ContentPage
{
    private OrdersViewModel _ordersViewModel;
    
    public readonly FlexGrid _grid;
    public OrdersListPage(OrdersViewModel viewModel, OrdersViewModel ordersViewModel, AddOrderViewModel addOrderViewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
        _ordersViewModel = viewModel;
        _grid = grid;
        ordersViewModel.SetGrid(_grid);
        addOrderViewModel.SetGrid(_grid);
        _ordersViewModel.LoadDataAsync(_grid);

    }
    private object _originalValue;
    private void OnBeginningEdit(object sender, GridCellEditEventArgs e)
    {
        _originalValue = grid[e.CellRange.Row, e.CellRange.Column];
    }
    private void OnCellEditEnded(object sender, GridCellEditEventArgs e)
    {
        var originalValue = _originalValue?.ToString().Replace(" ", "");
        var currentValue = grid[e.CellRange.Row, e.CellRange.Column].ToString().Replace(" ", "");
        var id = grid[e.CellRange.Row, 0].ToString().Replace(" ", "");
        var service = grid[e.CellRange.Row, 1].ToString().Replace(" ", "");
        var userData = grid[e.CellRange.Row, 2].ToString().Replace(" ", "");
        var date = grid[e.CellRange.Row, 3].ToString().Replace(" ", "");
        var payMethod = grid[e.CellRange.Row, 4].ToString().Replace(" ", "");
        var orderStatus = grid[e.CellRange.Row, 5].ToString().Replace(" ", "");

        grid[e.CellRange.Row, 0] = id;
        grid[e.CellRange.Row, 1] = service;
        grid[e.CellRange.Row, 2] = userData;
        grid[e.CellRange.Row, 3] = date;
        grid[e.CellRange.Row, 4] = payMethod;
        grid[e.CellRange.Row, 5] = orderStatus;


        if (!e.CancelEdits && (originalValue == null && currentValue != null || !originalValue.Equals(currentValue)))
        {
            DisplayAlert("Confirmation", "Do you want to commit the Edit?", "Apply", "Cancel").ContinueWith(async t =>
            {
                if (t.Result)
                {

                    var gr = grid[e.CellRange.Row, e.CellRange.Column];
                    grid[e.CellRange.Row, e.CellRange.Column] = currentValue;
                    //await viewModel.SetUppdateUsersData(name, pass, mail, phone, role, _oldUserName, this, e.CellRange.Row, e.CellRange.Column);
                }
                else
                {
                    grid[e.CellRange.Row, e.CellRange.Column] = originalValue;
                }
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}