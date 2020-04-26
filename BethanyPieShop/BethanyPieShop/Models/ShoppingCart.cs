using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanyPieShop.Models
{
    //Add this class in Startup.cs file
    public class ShoppingCart
    {
        private readonly AppDbContext _appDbContext;

        public string ShoppingCartId { get; set; }

        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        //constructor
        private ShoppingCart(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public static ShoppingCart GetCart(IServiceProvider services)
        {
            //Rachit  

            //IServiceProvider contains collection of services

            //IHttpContextAccessor -> helps in the session management in .NET Core.
            //In .NET Core, We have access to sessions directly in the controller, but we do not have access in a regular class therefore we are using IHttpContextAccessor 
            //And through IHttpContextAccessor we have access to HttpContext.Session and all the information about the request
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?
                .HttpContext.Session;

            //Ask the services collection for the AppDbContext
            var context = services.GetService<AppDbContext>();

            //When the user comes to shopping site, check if there is already an active session containing a cart Id. If not, create a new one. Then create an instance of a shopping cart, passing that cart Id. And that's what exactly we are doing in this function GetCart().  
            //Check the session to see if it already has a string with the ID called cartId. If not, create a new GUID and that will then be the cartId it has set in the session.
            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            //Set the cartId in the session.
            session.SetString("CartId", cartId);

            //Create a new shopping cart passing the AppDbContext with ShoppingCartId as the cartId.
            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }

        public void AddToCart(Pie pie, int amount)
        {
            //Check if PieId is found for that shopping cart 
            var shoppingCartItem =
                    _appDbContext.ShoppingCartItems.SingleOrDefault(
                        s => s.Pie.PieId == pie.PieId && s.ShoppingCartId == ShoppingCartId);

            //If Pie was not in the ShoppingCart then create new ShoppingCartItem and set the Pie as passed-in Pie
            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    Pie = pie,
                    Amount = 1
                };

                //Add the ShoppingCartItem to the list currently managed by AppDbContext in its DBSet.
                _appDbContext.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }

            //Add the ShoppingCartItem to the database
            _appDbContext.SaveChanges();
        }

        public int RemoveFromCart(Pie pie)
        {
            var shoppingCartItem =
                    _appDbContext.ShoppingCartItems.SingleOrDefault(
                        s => s.Pie.PieId == pie.PieId && s.ShoppingCartId == ShoppingCartId);

            var localAmount = 0;

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                    localAmount = shoppingCartItem.Amount;
                }
                else
                {
                    _appDbContext.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }

            _appDbContext.SaveChanges();

            return localAmount;
        }

        //GetShoppingCartItems will look if the ShoppingCartItems were already previously retrieved in this current ShoppingCart instance
        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            //If ShoppingCartItems list is null then go to AppDbContext and retrieve the ShoppingCartItems where the ShoppingCartId is the given ShoppingCartId that is currently associated with the Session of the user.
            return ShoppingCartItems ??
                   (ShoppingCartItems =
                       _appDbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                           .Include(s => s.Pie)
                           .ToList());
        }

        //Removes all the shopping cart items that are associated with the ShoppingCartId
        public void ClearCart()
        {
            var cartItems = _appDbContext
                .ShoppingCartItems
                .Where(cart => cart.ShoppingCartId == ShoppingCartId);

            _appDbContext.ShoppingCartItems.RemoveRange(cartItems);

            _appDbContext.SaveChanges();
        }

        //Get the sum of all the ShoppingCartItems in the ShoppingCart and returns the total. 
        public decimal GetShoppingCartTotal()
        {
            var total = _appDbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                .Select(c => c.Pie.Price * c.Amount).Sum();
            return total;
        }
    }
}
