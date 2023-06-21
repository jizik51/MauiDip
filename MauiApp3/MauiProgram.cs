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
        builder.Services.AddSingleton<AddUserPage>();
        builder.Services.AddSingleton<AddUserViewModel>();
        builder.Services.AddSingleton<DeleteUserPage>();
        builder.Services.AddSingleton<DeleteUserViewModel>();
        builder.Services.AddSingleton<RegUserPage>();
        builder.Services.AddSingleton<MobileMainPage>();
        builder.Services.AddSingleton<OrdersListPage>();
        builder.Services.AddSingleton<OrdersViewModel>();
        builder.Services.AddSingleton<AddOrderPage>();
        builder.Services.AddSingleton<AddOrderViewModel>();
        builder.Services.AddSingleton<RegUserViewModel>();










        return builder.Build();
	}
}
