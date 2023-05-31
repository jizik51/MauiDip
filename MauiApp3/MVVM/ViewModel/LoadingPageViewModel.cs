using MauiApp3.MVVM.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp3.MVVM.ViewModel
{
    public partial class LoadingPageViewModel : ContentPage
    {
        
        public LoadingPageViewModel()
        {
        }

        public bool CheckUserLoginData()
        {
            var isAutoLog = Preferences.Default.Get("AutoLogin", false);
            return isAutoLog;

        }


    }
}
