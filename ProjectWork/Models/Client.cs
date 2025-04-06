using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectWork.Models
{
    [Table("client")]
    public partial class Client
    {
        [Key]
        [Column("client_id")]
        public int ClientId { get; set; }
        [Column("name")]
        public string Name { get; set; } = null!;

        [Column("surname")]
        public string Surname { get; set; } = null!;

        [Column("birth_date")]
        public DateOnly BirthDate { get; set; }

        [InverseProperty("Client")]
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
