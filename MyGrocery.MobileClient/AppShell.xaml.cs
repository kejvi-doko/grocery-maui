using MyGrocery.MobileClient.Pages;

namespace MyGrocery.MobileClient;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        Routing.RegisterRoute(nameof(ManageGroceryPage), typeof(ManageGroceryPage));
    }
}

