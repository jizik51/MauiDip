using MauiApp3.Helpers;
using MauiApp3.Data;
using MauiApp3.MVVM.ViewModel;
using MauiApp3.MVVM.View;

namespace MauiApp3.MVVM.View;

public partial class DeleteUserPage : ContentPage
{

    public DeleteUserPage(DeleteUserViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        Appearing += DeleteUserPage_Appearing;
        Disappearing += DeleteUserPage_Disappearing;
    }
    private void DeleteUserPage_Appearing(object sender, System.EventArgs e)
    {
        var width = 400;
        var height = 450;
        WindowSizeHelper.SetWindowSize(this, width, height);

    }
    private void DeleteUserPage_Disappearing(object sender, System.EventArgs e)
    {
        var context = new DatabaseContext();
        var usersViewModel = new UsersViewModel(context);
        var addUserViewModel = new AddUserViewModel(context);
        var deleteUserViewModel = new DeleteUserViewModel(context);
        var usersListPage = new UsersListPage(usersViewModel, addUserViewModel, deleteUserViewModel);
        WindowSizeHelper.ResetWindowSize(usersListPage);
    }


}
