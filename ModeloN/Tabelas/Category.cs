using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Modelo.Cadastros;

namespace Modelo.Tabelas
{
    public class Category
    {
        public long CategoryId { get; set; }
        [Required]
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }

    }
}