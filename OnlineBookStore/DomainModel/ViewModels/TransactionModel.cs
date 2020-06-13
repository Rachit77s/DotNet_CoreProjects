using DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainModel.Models
{
    [NotMapped]
    public class TransactionModel : Transaction
    {
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
