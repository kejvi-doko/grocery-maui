using MyGrocery.MobileClient.DataServices;
using MyGrocery.MobileClient.Models;

namespace MyGrocery.MobileClient.Pages;

[QueryProperty(nameof(Grocery),"Grocery")]
public partial class ManageGroceryPage : ContentPage
{
	private readonly IRestDataSevice _dataService;
	private Grocery _grocery;
	private bool _isNew;

	public Grocery Grocery
	{
		get => _grocery;
		set
		{
			_isNew = IsNew(value);
			_grocery = value;
			OnPropertyChanged();
		}
	}
	public ManageGroceryPage(IRestDataSevice dataService)
	{
		InitializeComponent();
		_dataService = dataService;
		BindingContext = this;
	}

	bool IsNew(Grocery grocery)
	{
		return grocery.Id == 0;
	}

	async void OnSaveButtonClicked(object sender, EventArgs e)
	{
		if (_isNew)
		{
			await _dataService.AddGroceryAsync(_grocery);
		}
		else
		{
			await _dataService.UpdateGroceryAsync(_grocery);
		}
		
		await Shell.Current.GoToAsync("..");
	}
	
	async void OnDeleteButtonClicked(object sender, EventArgs e)
	{
		await _dataService.DeleteGroceryAsync(Grocery.Id);
		await Shell.Current.GoToAsync("..");
	}
	
	async void OnCancelButtonClicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("..");
	}
}
