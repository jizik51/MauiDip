using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using MauiApp3.Data;
using MauiApp3.MVVM.Model;

namespace MauiApp3.MVVM.ViewModel
{
    public partial class RegUserViewModel : ObservableObject
    {
        private readonly DatabaseContext _context;

        //[ObservableProperty]
        //private ObservableCollection<Users> _users = new();

        //[ObservableProperty]
        //private Users _operatingUsers = new();

        public RegUserViewModel(DatabaseContext context)
        {
            _context = context;
        }
    }
}
