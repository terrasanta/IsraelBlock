using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IsraelBlock.Models
{
    public class Product
    {
        public long? ProductId { get; set; }
        [Required]
        public String Name { get; set; }
        public String Active { get; set; }

        public long? CategoryId { get; set; }
        public long? SupplierId { get; set; }

        public Category Category { get; set; }
        public Supplier Supplier { get; set; }
        public virtual ICollection<Item> Items { get; set; }
    }
}