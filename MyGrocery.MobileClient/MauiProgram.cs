using Microsoft.Extensions.Logging;
using MyGrocery.MobileClient.DataServices;
using MyGrocery.MobileClient.Pages;

namespace MyGrocery.MobileClient;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

        builder.Services.AddSingleton<IRestDataSevice, RestDataService>();
		builder.Services.AddSingleton<MainPage>();
		builder.Services.AddTransient<ManageGroceryPage>();

		return builder.Build();
	}
}

