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

        MainPage = new AppShell();

    }

    protected override Window CreateWindow(IActivationState activationState)
    {
        var window = base.CreateWindow(activationState);
        window.Created += Window_Created;
        return window;
    }
    private async void Window_Created(object sender, EventArgs e)
    {
        var window = (Window)sender;

        const int defaultWidth = 1300;
        const int defaultHeight = 750;

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

