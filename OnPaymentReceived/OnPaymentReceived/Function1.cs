using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

//Add Nuget --> microsoft.azure.webjobs.storage for Queue
namespace OnPaymentReceived
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            [Queue("orders")] IAsyncCollector<Order> orderQueue, //It creates the Queue even if it does not exists
            [Table("orders")] IAsyncCollector<Order> orderTable,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var order = JsonConvert.DeserializeObject<Order>(requestBody);
            //Pass the order to the queue
            await orderQueue.AddAsync(order);

            log.LogInformation($"Order {order.OrderId} received from {order.Email} for product {order.ProductId} " );

            //Add a row in table storage
            // Every save we make will result in a new row in Azue Table Storage as well as sending message to a queue for license file generation
            order.PartitionKey = "orders"; // just one partition (for demo purposes)
            order.RowKey = order.OrderId;
            await orderTable.AddAsync(order);

            log.LogInformation($"Order {order.OrderId} received from {order.Email} for product {order.ProductId}");
            return new OkObjectResult("Thank you for placing order!!");
        }
    }

    public class Order
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }


        public string OrderId { get; set; }

        public string ProductId { get; set; }

        public string Email { get; set; }

        public decimal Price { get; set; }
    }
}
