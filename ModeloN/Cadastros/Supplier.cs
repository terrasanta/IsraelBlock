using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Modelo.Cadastros
{
    public class Supplier
    {
        public long SupplierId { get; set; }
        [Required]
        public String Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
    
}