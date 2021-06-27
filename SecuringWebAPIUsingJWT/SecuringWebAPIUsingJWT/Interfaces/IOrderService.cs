using SecuringWebAPIUsingJWT.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecuringWebAPIUsingJWT.Interfaces
{
    public interface IOrderService
    {
        Task<List<Order>> GetOrdersByCustomerId(int id);
    }
}
