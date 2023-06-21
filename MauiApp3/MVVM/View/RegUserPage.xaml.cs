using MauiApp3.MVVM.ViewModel;

namespace MauiApp3.MVVM.View;

public partial class RegUserPage : ContentPage
{
	public RegUserPage(RegUserViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;


    }
}