using System.Windows.Input;
using Power_Hand.Data.Other;
using Power_Hand.Data.Repository.Other;
using Power_Hand.Data.SharedData;
using Power_Hand.Features.FeatureApp.FeatureEmployee.Channels;
using Power_Hand.Features.Popups;
using Power_Hand.Features.Popups.ViewModels;
using Power_Hand.Interfaces;
using Power_Hand.Models;
using Prism.Events;

namespace Power_Hand.Features.FeatureApp.FeatureEmployee
{
    public class AddEditEmployeeFormVM: ViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IEmployeeRepo _employeeRepo;
        private readonly SharedValuesStore _sharedValuesStore;
        private readonly INavigationService _navigationService;

        private Employee? _employee;
        public Employee? Employee
        {
            get => _employee;
            set
            {
                _employee = value;
                Name = _employee?.Name;
                ID = _employee?.Id.ToString();
                Password = null;
                OnPropertyChanged();
            }
        }

        private string? _name;
        public string? Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(); }
        }

        private string? _id;
        public string? ID
        {
            get { return _id; }
            set { _id = value; OnPropertyChanged(); }
        }

        private string? _password;
        public string? Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(); }
        }




        public ICommand OnSaveCommand { get; set; }
        public ICommand OnDeleteCommand { get; set; }
        public ICommand OnDiscardCommand { get; set; }

        public AddEditEmployeeFormVM(IEventAggregator eventAggregator, 
            IEmployeeRepo employeeRepo, 
            SharedValuesStore store,
            INavigationService navigationService) 
        {
            _eventAggregator = eventAggregator;
            _employeeRepo = employeeRepo;
            _sharedValuesStore = store;
            _navigationService = navigationService;

            OnSaveCommand = new FunCommand(OnSaveClicked);
            OnDeleteCommand = new FunCommand(OnDeleteClicked);
            OnDiscardCommand = new FunCommand(OnDiscardClicked);

            _eventAggregator.GetEvent<EditSelectedEmployeeChannel>().Subscribe(OnEmployeeSelected);
        }

        private void OnEmployeeSelected(Employee? employee)
        {
            Employee = employee;
        }

        private void OnDiscardClicked()
        {
            Employee = null;
            Clear();
        }

        private void Clear()
        {
            Password = null;
            Name = null;
            ID = null;
        }

        private void OnDeleteClicked()
        {
            if (Employee != null)
            {
                _sharedValuesStore.EmployeeToDelete = Employee;
                _navigationService.OpenPopup<DeleteEmployeePopupVM>();
                _eventAggregator.GetEvent<PopupCloseChannel>().Publish(false);
                Clear();
            }
        }

        private void OnSaveClicked()
        {
            if (Employee == null)
            {
                if (!(string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(Name)))
                {
                    string password = Password.GetHashCode().ToString();
                    // new Employee
                    Employee employee = new(name: Name, password: password);
                    _employeeRepo.AddEmployee(employee);
                }
            }
            else
            {
                // edit Employee
                string? name = string.IsNullOrEmpty(Name)? Employee.Name : Name;
                string? password = string.IsNullOrEmpty(Password)? Employee.Password : Password.GetHashCode().ToString();

                if (!(string.IsNullOrEmpty(name) || string.IsNullOrEmpty(password)))
                {
                    Employee.Name = name;
                    Employee.Password = password;
                    _employeeRepo.UpdateEmployee(Employee);
                }
            }

            OnDiscardClicked();
            DatabaseChanged();
        }

        private void DatabaseChanged()
        {
            _eventAggregator.GetEvent<AddEditEmployeeDatabaseChangedChannel>().Publish();
        }
    }
}
