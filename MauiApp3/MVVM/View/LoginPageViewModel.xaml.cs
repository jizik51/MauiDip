using MauiApp3.MVVM.ViewModel;

namespace MauiApp3.MVVM.View;

public partial class LoginPageViewModel : ContentPage
{
    private readonly UsersViewModel _usersViewModel;
    public LoginPageViewModel(UsersViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
        _usersViewModel = viewModel;
    }
    //protected async override void OnAppearing()
    //{
    //    base.OnAppearing();
    //    await _usersViewModel.GetAllUsers();
    //}

}