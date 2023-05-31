using MauiApp3;
using MauiApp3.MVVM.View;
using MauiApp3.Data;
using MauiApp3.MVVM.ViewModel;
using System.Diagnostics;

namespace MauiApp3;

public partial class App : Application
{

    public App()
    {
        //var databaseContext = new DatabaseContext();
        //var usersViewModel = new UsersViewModel(databaseContext);
        //var mainPage = new MainPage(usersViewModel);
        //var loginPage = new LoginPageViewModel(usersViewModel);

        MainPage = new AppShell();
        //var isAutoLogin = Preferences.Default.Get("AutoLogin", false);
        //if (isAutoLogin) MainPage = new NavigationPage(mainPage);
        //else Application.Current.MainPage = loginPage;
    }


    protected override Window CreateWindow(IActivationState activationState)
    {
        var window = base.CreateWindow(activationState);
        window.Created += Window_Created;
        return window;
    }

    private async void Window_Created(object sender, EventArgs e)
    {
        const int defaultWidth = 1200;
        const int defaultHeight = 650;

        var window = (Window)sender;
        window.Width = defaultWidth;
        window.Height = defaultHeight;
        window.X = -defaultWidth;
        window.Y = -defaultHeight;

        await window.Dispatcher.DispatchAsync(() => { });

        var displayInfo = DeviceDisplay.Current.MainDisplayInfo;
        window.X = (displayInfo.Width / displayInfo.Density - window.Width) / 2;
        window.Y = (displayInfo.Height / displayInfo.Density - window.Height) / 2;
    }


}

