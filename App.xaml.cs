using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Power_Hand.Data.Models;
using Power_Hand.Data.Other;
using Power_Hand.Data.Repository.Invoices;
using Power_Hand.Data.Repository.Items;
using Power_Hand.Data.Repository.Other;
using Power_Hand.Data.SharedData;
using Power_Hand.Features.FeatureApp;
using Power_Hand.Features.FeatureApp.FeatureCasher;
using Power_Hand.Features.FeatureApp.FeatureEditClient;
using Power_Hand.Features.FeatureApp.FeatureEditItem;
using Power_Hand.Features.FeatureApp.FeatureEmployee;
using Power_Hand.Features.FeatureApp.FeatureInvoicesPreview;
using Power_Hand.Features.FeatureApp.FeatureReservation;
using Power_Hand.Features.FeatureHome;
using Power_Hand.Features.FeatureMain;
using Power_Hand.Features.Popups.ViewModels;
using Power_Hand.Features.Popups.Views;
using Power_Hand.Interfaces;
using Power_Hand.Utils.Component;
using Power_Hand.Utils.ViewModels;
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

                // takeaway
                services.AddTransient<CasherView>();
                services.AddTransient<ItemsGridView>();
                services.AddTransient<InvoiceItemsListView>();

                // Invoices
                services.AddTransient<InvoicesListingPage>();
                services.AddTransient<InvoicesListingView>();
                services.AddTransient<InvoicePreviewView>();

                // add edit  clients
                services.AddTransient<AddEditClientPageView>();
                services.AddTransient<AddEditClientFormView>();
                services.AddTransient<ClientSearchListView>();

                // Add edit items
                services.AddTransient<AddEditItemsPageView>();
                services.AddTransient<ItemFormView>();

                // add edit employees
                services.AddTransient<SearchView>();
                services.AddTransient<AddEditEmploeePageView>();
                services.AddTransient<AddEditEmploeeForm>();
                services.AddTransient<SearchEmploeesListView>();

                services.AddTransient<HomeView>();
                services.AddTransient<ReservationView>();


                services.AddTransient<NavigationBarView>();
                services.AddTransient<TaskBarView>();
                #endregion

                #region ViewModels
                // add view models
                // Main View Model
                services.AddSingleton<AppShellVM>();
                services.AddSingleton<MainVM>();


                // Casher Make Invoices View
                services.AddSingleton<CasherVM>();
                services.AddSingleton<CasherItemsNavigationVM>();
                services.AddSingleton<InvoiceItemsListVM>();

                // Invoices List View
                services.AddSingleton<InvoicesListingPageVM>();
                services.AddSingleton<InvoicesListViewVM>();
                services.AddSingleton<InvoicePreviewViewVM>();

                // Add Edit Client
                services.AddSingleton<AddEditClientPageVM>();
                services.AddSingleton<ClientFormVM>();
                services.AddSingleton<ClientListingVM>();

                // Add Edit emploees
                services.AddSingleton<AddEditEmployeePageVM>();
                services.AddSingleton<AddEditEmployeeFormVM>();
                services.AddSingleton<SearchEmployeesVM>();

                // Add Edit items
                services.AddSingleton<AddEditItemPageVM>();
                services.AddSingleton<ItemFormVM>();
                services.AddSingleton<EditItemsGridVM>();
                // GridItems_SVM is used here too

                services.AddSingleton<HomeVM>();
                services.AddSingleton<ReservationVM>();
                services.AddSingleton<NavigationBarVM>();
                services.AddSingleton<TaskBarVM>();

                // shared store gets instantiated in the app start and save shared data
                services.AddSingleton<SharedValuesStore>();
                services.AddSingleton<CalculatorVM>();

                #endregion


                #region Popups
                services.AddTransient<EditInvoiceItemPopupView>();
                services.AddSingleton<EditInvoiceItemPopupVM>();

                services.AddTransient<DeletePopupView>();
                services.AddSingleton<DeleteInvoiceItemPopupVM>();
                services.AddSingleton<DeleteEmployeePopupVM>();
                services.AddSingleton<DeleteClientPopupVM>();
                services.AddSingleton<DeleteItemPopupVM>();

                #endregion

                // add other services

                #region Repositories
                // repositories
                services.AddSingleton<IInvoicesRepo, InvoicesRepoImpl>();
                services.AddSingleton<IItemsRepo, ItemsRepoImpl>();
                services.AddSingleton<IEmployeeRepo, EmployeesRepoImpl>();
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
                // 
                services.AddSingleton<Func<Type, ViewModel>>(provider =>
                    viewModelType => (ViewModel)provider.GetRequiredService(viewModelType));
                #endregion

                // add the Refit Api for Http

            }).Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await _host.StartAsync();

            // start main window
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
