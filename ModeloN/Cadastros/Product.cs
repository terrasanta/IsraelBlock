using System;
using System.ComponentModel.DataAnnotations;
using Modelo.Tabelas;

namespace Modelo.Cadastros
{
    public class Product
    {
        public long? ProductId { get; set; }
        [Required]
        public String Name { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        public long? CategoryId { get; set; }
        public long? SupplierId { get; set; }

        public Category Category { get; set; }
        public Supplier Supplier { get; set; }
    }
}