using MauiApp3.MVVM.ViewModel;

namespace MauiApp3.MVVM.View;

public partial class AddOrderPage : ContentPage
{
	public AddOrderPage(AddOrderViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}