﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Power_Hand.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public double Price { get; set; }
        public int ParentId { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsFolder {  get; set; }

        public double? Expence { get; set; }
        public string? Note { get; set; }
        public double? Discount { get; set; }

        // constructor
        public Item(int id,
            string name, 
            double price, int parent,
            string? description = null,
            double? expence = null, string? note = null, 
            double? discount = null)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            ParentId = parent;
            Expence = expence;
            Note = note;
            Discount = discount;
        }


        // convert to InvoiceItem
        public InvoiceItem ToInvoiceItem(double qty = 1)
        {
            // set The Quantity directly
            return new InvoiceItem(itemId: Id, name: Name,
            price: Price, parentId: ParentId, discount: Discount,
            quantity:qty, isFolder:IsFolder);
            // notes, total & InvoiceId to be set later
        }
    }
}