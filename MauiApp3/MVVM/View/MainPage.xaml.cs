using MauiApp3.MVVM.ViewModel;
namespace MauiApp3.MVVM.View;

public partial class MainPage : ContentPage
{
    private readonly UsersViewModel _usersViewModel;

    public MainPage(UsersViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
        _usersViewModel = viewModel;
    }
    //protected override void OnNavigatedTo(NavigatedToEventArgs args)
    //{
    //    base.OnNavigatedTo(args);
    //}
}