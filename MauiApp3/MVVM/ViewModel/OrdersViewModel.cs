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
    public partial class OrdersViewModel : ObservableObject
    {
        private readonly DatabaseContext _context;
        private FlexGrid _grid;

        [ObservableProperty]
        private ObservableCollection<Orders> _orders = new();

        [ObservableProperty]
        private Orders _operatingOrders = new();
        public OrdersViewModel(DatabaseContext context)
        {
            _context = context;
        }

        public void SetGrid(FlexGrid grid)
        {
            _grid = grid;
        }
        public async void LoadDataAsync(FlexGrid grid)
        {
            var data = await GetAllOrders();
            grid.ItemsSource = data;
        }
        public async Task<ObservableCollection<Orders>> GetAllOrders()
        {
            var orders = await _context.GetAllOrders();
            if (orders is not null)
            {
                Orders ??= new ObservableCollection<Orders>();
                foreach (var order in orders)
                {
                    Orders.Add(order);
                }
            }
            return Orders;
        }
        [RelayCommand]
        private async void LoadAddOrderPage()
        {
            await Shell.Current.GoToAsync($"{nameof(AddOrderPage)}");
        }
    }
}
