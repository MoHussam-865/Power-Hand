using Power_Hand.Models;

namespace Power_Hand.Data.Repository.Other
{
    public interface IPeopleRepo
    {
        public Emploee? GetEmploee(string username, string password);

        public Client? GetClient(string search);
    }
}
