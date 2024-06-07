using System.Configuration;
using System.Data;
using System.Windows;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Refit;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Power_Hand.DBContext;
using Power_Hand.Api;

namespace Power_Hand
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
		private readonly IHost _host;

		public App()
		{
			_host = Host.CreateDefaultBuilder().ConfigureServices((context, services) =>
			{
				// rigester to the dependance injection
				// add window
				services.AddSingleton<MainWindow>();

				// database context service goes here 
				services.AddDbContext<DatabaseContext>(options =>
					options.UseSqlServer("connection string"));

				// add view models


				// add other services

				// add the Refit Api for Http
				


			}).Build();
		}

		protected override async void OnStartup(StartupEventArgs e)
		{
			await _host.StartAsync();

			var mainWindow = _host.Services.GetRequiredService<MainWindow>();
			mainWindow.Show();

			base.OnStartup(e);
		}

		protected override async void OnExit(ExitEventArgs e)
		{
			await _host.StopAsync(TimeSpan.FromSeconds(5));
			_host.Dispose();

			base.OnExit(e);
		}
	}

}
