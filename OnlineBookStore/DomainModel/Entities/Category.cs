﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainModel.Entities
{
    //One To Many relationship between a Category and Product
    public class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
            CreatedDate = DateTime.Now;
        }

        [Key]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Please Enter Name")]
        [Column(TypeName ="varchar(50)")]
        public string Name { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
