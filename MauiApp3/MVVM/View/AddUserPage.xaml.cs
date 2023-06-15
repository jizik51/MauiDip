using MauiApp3.MVVM.ViewModel;

namespace MauiApp3.MVVM.View;

public partial class AddUserPage : ContentPage
{
	public AddUserPage(AddUserViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;

    }
}