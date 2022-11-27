using System.Diagnostics;
using MyGrocery.MobileClient.DataServices;
using MyGrocery.MobileClient.Models;
using MyGrocery.MobileClient.Pages;

namespace MyGrocery.MobileClient;

public partial class MainPage : ContentPage
{
    private readonly IRestDataSevice _dataService;

    public MainPage(IRestDataSevice dataSevice)
	{
		InitializeComponent();
		_dataService = dataSevice;
	}

	protected async override void OnAppearing()
	{
		base.OnAppearing();
        collectionView.ItemsSource = await _dataService.GetGroceriesAsync();
	}

	async void ToolbarItem_Clicked(System.Object sender, System.EventArgs e)
    {
        Debug.WriteLine("---> Add Button Clicked");
        var navigationParameter = new Dictionary<string, object>()
        {
	        { nameof(Grocery), new Grocery() }
        };

        await Shell.Current.GoToAsync(nameof(ManageGroceryPage), navigationParameter);
    }

    async void collectionView_SelectionChanged(System.Object sender, Microsoft.Maui.Controls.SelectionChangedEventArgs e)
    {
        Debug.WriteLine("---> Collection Item Clicked");
        
        var navigationParameter = new Dictionary<string, object>()
        {
	        { nameof(Grocery), e.CurrentSelection.FirstOrDefault() as Grocery }
        };

        await Shell.Current.GoToAsync(nameof(ManageGroceryPage), navigationParameter);
    }
}


