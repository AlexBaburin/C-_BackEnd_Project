using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectWork.Models
{
    [Table("order")]
    public class Order
    {
        [Key]
        [Column("order_id")]
        public int OrderId { get; set; }

        [Column("amount")]
        public int Amount {  get; set; }

        [Column("time")]
        public DateTime Time { get; set; }

        [Column("status_id")]
        public int StatusId { get; set; }
        [ForeignKey("StatusId")]
        [InverseProperty("Orders")]
        public virtual OrderStatus Status { get; set; } = null!;

        [Column("client_id")]
        public int ClientId { get; set; }
        [ForeignKey("ClientId")]
        [InverseProperty("Orders")]
        public virtual Client Client { get; set; } = null!;
    }
}
