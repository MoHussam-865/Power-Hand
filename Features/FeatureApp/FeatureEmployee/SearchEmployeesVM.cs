using System.Windows.Input;
using Power_Hand.Data.Other;
using Power_Hand.Data.Repository.Other;
using Power_Hand.Features.FeatureApp.FeatureEmployee.Channels;
using Power_Hand.Interfaces;
using Power_Hand.Models;
using Power_Hand.Utils.ViewModels;
using Prism.Events;

namespace Power_Hand.Features.FeatureApp.FeatureEmployee
{
    public class SearchEmployeesVM : SearchLogicVM<Employee>
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IEmployeeRepo _employeeRepo;

        public SearchEmployeesVM(
            IEventAggregator eventAggregator,
            IEmployeeRepo employeeRepo)
        {
            _eventAggregator = eventAggregator;
            _employeeRepo = employeeRepo;

            // this is an inherited method from SearchLogicVM
            GetItems();
            _eventAggregator.GetEvent<AddEditEmployeeDatabaseChangedChannel>().Subscribe(OnDatabaseChanged);
        }


        private void OnDatabaseChanged() => GetItems();

        public override async Task<List<Employee>> OnSearchChanged(string? search)
        {
            List<Employee>? employees = await _employeeRepo.GetEmployees(search);
            return employees ?? [];
        }

        public override void OnItemClicked(Employee? employee)
        {
            _eventAggregator.GetEvent<EditSelectedEmployeeChannel>().Publish(employee);
        }
    }
}
