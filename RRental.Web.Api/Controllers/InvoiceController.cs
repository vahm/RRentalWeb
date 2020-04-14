using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using RRental.Web.Api.Data;
using RRental.Web.Api.Helper;
using RRental.Web.Api.Models;

namespace RRental.Web.Api.Controllers
{
    public class InvoiceController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //GET orders
        public IQueryable<Order> GetOrders()
        {
            return db.Order.GroupBy(x => x.CartNo).Select(x => x.FirstOrDefault());
        }

        [HttpGet]
        [Route("api/invoice/getinvoice/{cartNo}")]
        public IHttpActionResult Create(string cartNo)
        {
            var cartData = db.Order.Where(x => x.CartNo == cartNo);
            var cartDate = cartData.Where(x => x.CartNo == cartNo).Select(x => x.DateCreated)
                .FirstOrDefault();
            var customer = db.Customer.FirstOrDefault(x => x.Id == cartData.Select(y => y.Customer.Id).FirstOrDefault());
            List<InvoiceItems> invoice = new List<InvoiceItems>();

            foreach (var item in cartData)
            {
                var itemName = db.Inventory.Where(x => x.Id == item.ItemId).Select(x => x.Name)
                    .FirstOrDefault();
                var itemType = db.Inventory.Where(x => x.Id == item.ItemId).Select(x => x.EquipmentType)
                    .FirstOrDefault();
                int duration = item.Duration;
                InvoiceItems items = new InvoiceItems();
                {
                    items.ItemName = itemName;
                    items.RentDuration = duration;
                    items.RentPrice = CalculatePrice.Price(itemType, duration);
                    items.BonusPoints = CalculatePoints.Calculate(itemType);
                }

                invoice.Add(items);
            }
            using (var stream = new MemoryStream())
            {
                StreamWriter writer = new StreamWriter(stream);
                writer.WriteLine("Invoice no: " + cartDate.Ticks);
                writer.WriteLine("-------------------");
                writer.WriteLine("Customer name: " + customer?.CustomerName + ", you have " + customer?.BonusPoints + " points");
                writer.WriteLine("Equipment:");
                writer.WriteLine("-------------------");
                int priceSum = 0;
                int pointsSum = 0;
                foreach (var item in invoice)
                {
                    writer.WriteLine(item.ItemName + "; " + item.RentDuration + " days; " + item.RentPrice + " €");
                    priceSum += item.RentPrice;
                    pointsSum += item.BonusPoints;
                }
                writer.WriteLine("-------------------");
                writer.WriteLine("Total: " + priceSum + " €");
                writer.WriteLine("Points earned: " + pointsSum);
                writer.Flush();
                writer.Close();

                var result = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(stream.ToArray())
                };
                result.Content.Headers.ContentDisposition =
                    new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
                    {
                        FileName = "Invoice.txt"
                    };
                result.Content.Headers.ContentType =
                    new MediaTypeHeaderValue("application/octet-stream");

                var response = ResponseMessage(result);

                return response;
            }
        }      
    }
}
