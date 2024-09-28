using MyDatabase.Models;
using MyDatabase.Repository.Emploee;
using Power_Hand.Data.SharedData;
using Power_Hand.Features.FeatureApp.FeatureEmployee.Channels;
using Power_Hand.Utils.ViewModels;

namespace Power_Hand.Features.Popups.ViewModels
{
    public class DeleteEmployeePopupVM(IEmployeeRepo employeeRepo, IEventAggregator eventAggregator, SharedValuesStore store) 
        : DeletePopupLogicVM<Employee>(eventAggregator)
    {
        private readonly IEmployeeRepo _employeeRepo = employeeRepo;
        private readonly IEventAggregator _eventAggregator = eventAggregator;

        public override string Title => "Action Can't be Undo";

        public override string Message => "You Are about to Delete one Employee";

        public override Employee? ThingToDelete { get => store.EmployeeToDelete; }

        public override void OnCancel() { }

        public override void OnDelete(Employee employee)
        {
            _employeeRepo.DeleteEmployee(employee);
            _eventAggregator.GetEvent<AddEditEmployeeDatabaseChangedChannel>().Publish();
        }
    }
}
