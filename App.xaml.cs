using System.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Power_Hand.Data.Repository.Invoices;
using Power_Hand.Data.Repository.Items;
using Power_Hand.Data.Repository.Other;
using Power_Hand.Data.SharedData;
using Power_Hand.DBContext;
using Power_Hand.Interfaces;
using Power_Hand.View;
using Power_Hand.ViewModels;
using Prism.Events;

namespace Power_Hand
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;

        // The App    dependancy injection is here
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

                #region Views
                services.AddTransient<AppShellView>();

                services.AddTransient<CasherView>();
                services.AddTransient<ItemsGridView>();
                services.AddTransient<InvoiceItemsListView>();

                services.AddTransient<InvoicesListingPage>();
                services.AddTransient<InvoicesListingView>();
                services.AddTransient<InvoicePreviewView>();

                services.AddTransient<AddEditClientPageView>();
                services.AddTransient<AddEditClientFormView>();
                services.AddTransient<ClientSearchListView>();

                services.AddTransient<AddEditItemsPageView>();
                services.AddTransient<ItemFormView>();

                services.AddTransient<HomeView>();
                services.AddTransient<ReservationView>();


                services.AddTransient<NavigationBarView>();
                #endregion

                #region ViewModels
                // add view models
                // Main View Model
                services.AddSingleton<AppShellVM>();
                services.AddSingleton<MainVM>();


                // Casher Make Invoices View
                services.AddSingleton<CasherVM>();
                services.AddSingleton<GridItems_SVM>();
                services.AddSingleton<InvoiceItemsList_SVM>();

                // Invoices List View
                services.AddSingleton<InvoicesListingPageVM>();
                services.AddSingleton<InvoicesListViewVM>();
                services.AddSingleton<InvoicePreviewViewVM>();

                // Add Edit Client
                services.AddSingleton<AddEditClientPageVM>();
                services.AddSingleton<ClientFormVM>();
                services.AddSingleton<ClientListingVM>();

                // Add Edit items
                services.AddSingleton<AddEditItemPageVM>();
                services.AddSingleton<ItemFormVM>();
                // GridItems_SVM is used here too

                services.AddSingleton<HomeVM>();
                services.AddSingleton<ReservationVM>();
                services.AddSingleton<NavigationBarVM>();

                #endregion

                // add other services

                
                #region Repositories
                // repositories
                services.AddSingleton<IInvoicesRepo, InvoicesRepoImpl>();
                services.AddSingleton<IItemsRepo, ItemsRepoImpl>();
                services.AddSingleton<IEmploeeRepo, EmploeesRepoImpl>();
                services.AddSingleton<IClientRepo, ClientsRepoImpl>();
                #endregion

                
                #region Other Services
                // database context service goes here 
                services.AddDbContext<DatabaseContext>();

                // data share
                // shared data services acts like a service that has some data
                // which ViewModel subscribe or publish to it 

                services.AddSingleton<IEventAggregator>(provider => new EventAggregator());
                // navigation
                services.AddSingleton<INavigationService, NavigationService>();

                services.AddSingleton<Func<Type, ViewModel>>(provider =>
                    viewModelType => (ViewModel)provider.GetRequiredService(viewModelType));
                #endregion

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
