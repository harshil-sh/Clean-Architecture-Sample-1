using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Order
    {
        [Key]
        public long Id { get; set; }
        public long CustomerID { get; set; }
        public DateTime OrderDateTime { get; set; }=DateTime.Now;
        public List<OrderItem> Items { get; set; }
        public int TotalNoOfItems { get; set; }
        public float TotalPrice { get; set; }
        public Boolean Status { get; set; }
        public float RefundAmount { get; set; }
        public DateTime OrderDeliveryDate { get; set; }=DateTime.Now.AddDays(7);
       
    }

}
