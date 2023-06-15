using MauiApp3.MVVM.View;
using MauiApp3.MVVM.ViewModel;
using MauiApp3.Data;
using System.Diagnostics;
using Microsoft.Maui.Controls;

namespace MauiApp3;

public partial class AppShell : Shell
{
    public AppShell()
	{
		InitializeComponent();
        this.BindingContext = new AppShellViewModel();
        Routing.RegisterRoute(nameof(MainPage), typeof(MauiApp3.MVVM.View.MainPage));
        Routing.RegisterRoute(nameof(LoginPageViewModel), typeof(LoginPageViewModel));
		Routing.RegisterRoute(nameof(LoadPage), typeof(LoadPage));
        Routing.RegisterRoute(nameof(UsersListPage), typeof(UsersListPage));
        Routing.RegisterRoute(nameof(AddUserPage), typeof(AddUserPage));
        Routing.RegisterRoute(nameof(RegUserPage), typeof(RegUserPage));
        Routing.RegisterRoute(nameof(MobileMainPage), typeof(MobileMainPage));




        //Routing.RegisterRoute(nameof(NewPage1), typeof(NewPage1));

    }
}
