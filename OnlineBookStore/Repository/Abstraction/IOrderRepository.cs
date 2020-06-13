using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Entities;
using DomainModel.Models;

namespace BAL.Abstraction
{
    public interface IOrderRepository : IRepository<Order>
    {
        int SaveCart(Cart model);
        void PlaceOrder(TransactionModel model);
    }
}
