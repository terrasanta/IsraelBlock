using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IsraelBlock.Models
{
    public class Item
    {
        public long? ItemId { get; set; }
        [Required(ErrorMessage = "Quantidade é obrigatório!")]
        public int Amount { get; set; }
        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Valor unitário é obrigatório!")]
        public decimal UnitaryValue { get; set; }
        public Sale Sale { get; set; }
        public Product Product { get; set; }

        public long? ProductId { get; set; }
        public long? SaleId { get; set; }
    }
}