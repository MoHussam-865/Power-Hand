using Power_Hand.Interfaces;

namespace Power_Hand.Features.FeatureApp.FeatureEmployee
{
    public class AddEditEmployeePageVM(AddEditEmployeeFormVM formVM, SearchEmployeesVM searchVM) : ViewModel
    {
        private AddEditEmployeeFormVM _employeeFormVM = formVM;
        public AddEditEmployeeFormVM EmployeeFormVM
        {
            get => _employeeFormVM; set {  _employeeFormVM = value; OnPropertyChanged(); } 
        }

        private SearchEmployeesVM _searchEmployeesVM = searchVM;
        public SearchEmployeesVM SearchEmployeesVM 
        { 
            get => _searchEmployeesVM; set { _searchEmployeesVM = value; OnPropertyChanged(); } 
        }
    }
}
