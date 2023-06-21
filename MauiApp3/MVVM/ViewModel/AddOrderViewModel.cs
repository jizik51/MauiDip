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
    public partial class AddOrderViewModel : ObservableObject
    {
        private readonly DatabaseContext _context;
        private FlexGrid _grid;
        [ObservableProperty]
        private ObservableCollection<Orders> _orders = new();

        [ObservableProperty]
        private Orders _operatingOrders = new();
        //private string _selectedDate;
        //public string SelectedDate
        //{
        //    get => _selectedDate;
        //    set
        //    {
        //        if (_selectedStatus != value)
        //        {
        //            _selectedStatus = value;
        //            OnPropertyChanged();
        //        }
        //    }
        //}

        private string _selectedStatus;
        public string SelectedStatus
        {
            get => _selectedStatus;
            set
            {
                if (_selectedStatus != value)
                {
                    _selectedStatus = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _selectedPayMethod;
        public string SelectedPayMethod
        {
            get => _selectedPayMethod;
            set
            {
                if (_selectedPayMethod != value)
                {
                    _selectedPayMethod = value;
                    OnPropertyChanged();
                }
            }
        }

        public AddOrderViewModel(DatabaseContext context)
        {
            _context = context;

        }
        public void SetGrid(FlexGrid grid)
        {
            _grid = grid;
        }
        [RelayCommand]
        private async void SignOut()
        {
            Preferences.Default.Set("id", String.Empty);
            Preferences.Default.Set("UserName", String.Empty);
            Preferences.Default.Set("Password", String.Empty);
            Preferences.Default.Set("AutoLogin", false);
            await Shell.Current.GoToAsync($"///{nameof(RegUserPage)}");
        }

        [RelayCommand]
        public async Task AddNewOrder()
        {
            try
            {
                
                OperatingOrders.PayMethod = _selectedPayMethod.ToString();
                OperatingOrders.OrderStatus = _selectedStatus.ToString();

                await _context.AddItemAsync(OperatingOrders);
                var data = await GetAllOrders();
                _grid.ItemsSource = data;
            }
            catch (Exception ex)
            {
            }
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
    }
}
