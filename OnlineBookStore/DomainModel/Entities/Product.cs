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
    public class Product
    {
        public Product()
        {
            CreatedDate = DateTime.Now;
        }

        [Key]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Please Enter Name")]
        [Column(TypeName = "varchar(50)")]
        public string Name { get; set; }

        public decimal UnitPrice { get; set; }

        [Required(ErrorMessage = "Please Enter Description")]
        [Column(TypeName = "varchar(500)")]
        public string Description { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string ImageName { get; set; }

        [Column(TypeName = "varchar(250)")]
        public string ImagePath { get; set; }
               
        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        
        public virtual Category Category { get; set; }
    }
}
