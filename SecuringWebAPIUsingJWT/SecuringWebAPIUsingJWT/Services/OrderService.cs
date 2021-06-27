using Microsoft.EntityFrameworkCore;
using SecuringWebAPIUsingJWT.Entities;
using SecuringWebAPIUsingJWT.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecuringWebAPIUsingJWT.Services
{
    public class OrderService : IOrderService
    {
        private readonly CustomersDbContext customersDbContext;

        public OrderService(CustomersDbContext customersDbContext)
        {
            this.customersDbContext = customersDbContext;
        }
        public async Task<List<Order>> GetOrdersByCustomerId(int id)
        {
            var orders = await customersDbContext.Orders.Where(order => order.CustomerId == id).ToListAsync();

            return orders;
        }

    }
}
