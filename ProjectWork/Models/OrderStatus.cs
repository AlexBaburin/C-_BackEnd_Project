using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectWork.Models
{
    [Table("order_status")]
    public class OrderStatus
    {
        [Key]
        [Column("status_id")]
        public int StatusId { get; set; }

        [Column("name")]
        public string Name { get; set; } = null!;

        [InverseProperty("Status")]
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
