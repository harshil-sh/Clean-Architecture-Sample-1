using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Book
    {
        [Key]
        public long ID { get; set; }
        public string Title { get; set; }
        public string Authors { get; set; }
        public int QuantityAvailable { get; set; }
        public float Price { get; set; }
    }
}
