﻿using System.Configuration;
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
using Power_Hand.ViewModels;
using Power_Hand.View;
using Power_Hand.Interfaces;

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
				// add Views
				services.AddSingleton<MainWindow>(provider => new MainWindow
				{
					DataContext = provider.GetRequiredService<MainVM>()
				});

				services.AddTransient<CasherView>();
				services.AddTransient<HomeView>();

				// database context service goes here 
				services.AddDbContext<DatabaseContext>(options =>
					options.UseSqlServer("connection string"));

				// add view models
				services.AddSingleton<CasherVM>();

                services.AddSingleton<HomeVM>();

                services.AddSingleton<MainVM>();


                // add other services

                services.AddSingleton<INavigationService,NavigationService>();

                services.AddSingleton<Func<Type, ViewModel>>(provider =>
                    viewModelType => (ViewModel) provider.GetRequiredService(viewModelType));


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
