namespace Power_Hand.Data.enums
{
    public enum EmployeeRules
    {
        // Can do Anything and Add Admins
        SuperAdmin,  

        // Can Add, Edit and Delete Employees
        // Make Add Edit Delete Invoices
        // Add Edit Delete Clients
        // Add Edit Delete Items
        Admin,

        // Can Add Invoices, Reservations, Delivery and Restaurant 
        // Add View Clients
        Casher,

        // Can Add Invoices, Reservations, Delivery and Restaurant 
        // Add Edit View Clients
        Reservation
    }
}
