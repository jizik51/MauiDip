using MauiApp3.MVVM.ViewModel;

namespace MauiApp3.MVVM.View;

public partial class LoadPage : ContentPage
{
    private readonly LoadingPageViewModel _viewModel;

    public LoadPage(LoadingPageViewModel viewModel)
	{
		InitializeComponent();
        this.BindingContext = viewModel;
        _viewModel = viewModel;
    }
    protected async override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        if (_viewModel.CheckUserLoginData()) 
            await Shell.Current.GoToAsync($"{nameof(MainPage)}");
        else 
            await Shell.Current.GoToAsync($"{nameof(RegUserPage)}");

    }
}