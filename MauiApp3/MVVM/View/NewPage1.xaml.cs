using MauiApp3.MVVM.ViewModel;

namespace MauiApp3.MVVM.View;

public partial class NewPage1 : ContentPage
{
    private readonly UsersViewModel _usersViewModel;

    public NewPage1(UsersViewModel viewModel)
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