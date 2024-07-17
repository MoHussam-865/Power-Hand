using Power_Hand.Data.Repository.Other;
using Power_Hand.Data.SharedData;
using Power_Hand.Models;
using Power_Hand.Utils.ViewModels;
using Prism.Events;

namespace Power_Hand.Features.Popups.ViewModels
{
    public class DeleteEmployeePopupVM(IEmploeeRepo employeeRepo, IEventAggregator eventAggregator, SharedValuesStore store) 
        : DeletePopupLogicVM<Employee>(eventAggregator)
    {
        private readonly IEmploeeRepo _employeeRepo = employeeRepo;

        public override string Title => "Action Can't be Undo";

        public override string Message => "You Are about to Delete one Employee";

        public override Employee? ThingToDelete { get => store.EmployeeToDelete; }

        public override void OnCancel() { }

        public override void OnDelete(Employee employee)
        {
            _employeeRepo.DeleteEmploee(employee);
        }
    }
}
