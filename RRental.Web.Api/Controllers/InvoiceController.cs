using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using RRental.Web.Api.Data;
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
            var cartData = db.Order;
            var cartDate = cartData.Where(x => x.CartNo == cartNo).Select(x => x.DateCreated)
                .FirstOrDefault();

            List<InvoiceItems> invoice = new List<InvoiceItems>();

            foreach (var item in cartData.Where(x => x.CartNo == cartNo))
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
                }

                invoice.Add(items);
            }
            using (var stream = new MemoryStream())
            {
                StreamWriter writer = new StreamWriter(stream);
                writer.WriteLine("Invoice no: " + cartDate.Ticks);
                writer.WriteLine("-------------------");
                writer.WriteLine("Equipment:");
                writer.WriteLine("-------------------");
                foreach (var item in invoice)
                {
                    writer.WriteLine(item.ItemName + "; " + item.RentDuration + " days; " + item.RentPrice + " €");
                }
                writer.WriteLine("-------------------");
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

        public static class CalculatePrice
        {
            public static int Price(EquipmentType type, int duration)
            {
                int oneTimeFee = 100;
                int dailyFeePremium = 60;
                int dailyFeeRegular = 40;
                int price = 0;
                switch (type)
                {
                    case EquipmentType.HeavyEquipment:
                        price = dailyFeePremium * duration + oneTimeFee;
                        break;
                    case EquipmentType.RegularEquipment:
                        if (duration > 2)
                        {
                            price = dailyFeePremium * 2 + dailyFeeRegular * (duration - 2) + oneTimeFee;
                        }
                        else
                        {
                            price = dailyFeePremium * duration + oneTimeFee;
                        }
                        break;
                    case EquipmentType.SpecializedEquipment:
                        if (duration > 3)
                        {
                            price = dailyFeePremium * 3 + dailyFeeRegular * (duration - 3);
                        }
                        else
                        {
                            price = dailyFeePremium * duration;
                        }
                        break;
                }
                return price;
            }
        }
    }
}
