using MauiApp3.MVVM.ViewModel;

namespace MauiApp3.MVVM.View;

public partial class MobileMainPage : ContentPage
{
	public MobileMainPage(AddOrderViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}