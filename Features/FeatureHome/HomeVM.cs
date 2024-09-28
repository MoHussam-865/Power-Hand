using System.Diagnostics;
using System.Windows.Input;
using MyDatabase.Models;
using MyDatabase.Repository.Emploee;
using Power_Hand.Features.FeatureApp;
using Power_Hand.Features.FeatureApp.FeatureCasher;
using Power_Hand.Other.Other;
using Power_Hand.Other.SharedData;
using Prism.Events;

namespace Power_Hand.Features.FeatureHome
{
    class HomeVM : ViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IEmployeeRepo _employeeRepo;
        private INavigationService _navigationService;
        public INavigationService NavigationService
        {
            get => _navigationService;
            set { _navigationService = value; OnPropertyChanged(nameof(_navigationService)); }
        }
        private string? _name;
        public string? UserName
        {
            get => _name;
            set { _name = value; OnPropertyChanged(); }
        }

        private string? _password;
        public string? Password
        {
            get => _password;
            set { _password = value; OnPropertyChanged(); }
        }

        private string? _error;

        public string? Error
        {
            get { return _error; }
            set { _error = value; OnPropertyChanged(); }
        }



        public ICommand OnLoginCommand { get; set; }

        public HomeVM(INavigationService navigationService, IEmployeeRepo employeeRepo, IEventAggregator eventAggregator)
        {
            _navigationService = navigationService;
            _eventAggregator = eventAggregator;
            _employeeRepo = employeeRepo;
            OnLoginCommand = new FunCommand(OnLoginClicked);
        }


        private void OnLoginClicked()
        {
            string? userName = _name;
            string? password = _password;
            Debug.WriteLine(userName + "  " + password);

            // hash the input text
            // check if there is a user with the same hash and userName
            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password))
            {
                //password = password.GetHashCode().ToString();
                Employee? employee = _employeeRepo.GetEmployee(userName, password);

                Debug.WriteLine("employee" + employee?.Name?.ToString());


                if (employee != null)
                {
                    Error = null;
                    _eventAggregator.GetEvent<EmploeeShare>().Publish(employee);
                    NavigationService.SetParentView<AppShellVM>();
                    NavigationService.NavigateTo<CasherVM>();
                    Clear();
                }
                else
                {
                    Error = "Wrong Username or Password";
                }
            }

        }


        private void Clear()
        {
            UserName = null;
            Password = null;
        }

    }
}
