using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IsraelBlock.Models
{
    public class Category
    {
        public long CategoryId { get; set; }
        [Required]
        public string Name { get; set; }

        //public virtual ICollection<Product> Products { get; set; }

    }
}