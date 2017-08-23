using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IsraelBlock.Models
{
    public class Supplier
    {
        public long SupplierId { get; set; }
        [Required]
        public String Name { get; set; }
    }
}