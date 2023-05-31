using MauiApp3.Data;
using MauiApp3.MVVM.ViewModel;
using Microsoft.Extensions.Logging;
using MauiApp3.MVVM.View;
using C1.Maui.Grid;


namespace MauiApp3;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .RegisterFlexGridControls()
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<LoadingPageViewModel>();
        builder.Services.AddSingleton<LoadPage>();
        builder.Services.AddSingleton<LoginPageViewModel>();
        builder.Services.AddSingleton<DatabaseContext>();
        builder.Services.AddSingleton<UsersViewModel>();
        builder.Services.AddSingleton<AppShellViewModel>();
        builder.Services.AddSingleton<UsersListPage>();


        return builder.Build();
	}
}
