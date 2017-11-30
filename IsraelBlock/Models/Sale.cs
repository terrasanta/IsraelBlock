using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IsraelBlock.Models
{
    public class Sale
    {
        public long? SaleId { get; set; }
        [Required(ErrorMessage = "Número da Nota é obrigatório!")]
        public String NrNota { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime DateSale { get; set; }
        [Required(ErrorMessage = "Nome do Cliente é obrigatório!")]
        public String NameClient { get; set; }
        [Required(ErrorMessage = "CPF do Cliente é obrigatório!")]
        public String CpfClient { get; set; }
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Telefone do Cliente é obrigatório!")]
        public String Phone { get; set; }
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public decimal TotalSale { get; set; }
        public String Closed { get; set; }
        public virtual ICollection<Item> Items { get; set; }

    }
}