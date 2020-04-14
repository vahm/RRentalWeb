using System.Collections.Generic;

namespace RRental.Web.Api.Models
{
    public class Invoice
    {
        public Invoice(List<InvoiceItems> invoiceItems)
        {
            InvoiceItems = invoiceItems;
        }
        public List<InvoiceItems> InvoiceItems { get; set; }
    }

    public class InvoiceItems
    {
        public string ItemName { get; set; }
        public int RentDuration { get; set; }
        public int RentPrice { get; set; }
    }
}