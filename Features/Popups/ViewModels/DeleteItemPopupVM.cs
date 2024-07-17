using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Power_Hand.Data.Repository.Items;
using Power_Hand.Data.Repository.Other;
using Power_Hand.Data.SharedData;
using Power_Hand.Models;
using Power_Hand.Utils.ViewModels;
using Prism.Events;

namespace Power_Hand.Features.Popups.ViewModels
{
    public class DeleteItemPopupVM(IItemsRepo itemsRepo, IEventAggregator eventAggregator, SharedValuesStore store)
        : DeletePopupLogicVM<Item>(eventAggregator)
    {

        private readonly IItemsRepo _itemsRepo = itemsRepo;

        public override string Title => "Action Can't be Undo";

        public override string Message => "You Are about to Delete one Item";

        public override Item? ThingToDelete { get => store.ItemToDelete; }

        public override void OnCancel() { }

        public override void OnDelete(Item item)
        {
            _itemsRepo.DeleteItem(item);
        }
    }
}
