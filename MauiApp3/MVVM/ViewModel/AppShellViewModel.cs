using CommunityToolkit.Mvvm.Input;
using MauiApp3.MVVM.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp3.MVVM.ViewModel
{
    public partial class AppShellViewModel 
    {
        [RelayCommand]
        private async void SignOut()
        {
            Preferences.Default.Set("id", String.Empty);
            Preferences.Default.Set("UserName", String.Empty);
            Preferences.Default.Set("Password", String.Empty);
            Preferences.Default.Set("AutoLogin", false);
            await Shell.Current.GoToAsync($"///{nameof(LoginPageViewModel)}");
        }
    }
}
