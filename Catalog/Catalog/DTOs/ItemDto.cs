using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.DTOs
{
    public record ItemDto //(Guid Id, string Name, decimal Price, DateTimeOffset CreatedDate);
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        //public string Description { get; init; }
        public decimal Price { get; init; }
        public DateTimeOffset CreatedDate { get; set; }
    }
        
    public record CreateItemDto([Required] string Name, string Description, [Range(1, 1000)] decimal Price);
    public record UpdateItemDto([Required] string Name, string Description, [Range(1, 1000)] decimal Price);
}
