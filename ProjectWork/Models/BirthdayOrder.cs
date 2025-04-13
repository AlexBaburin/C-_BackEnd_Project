using System.ComponentModel.DataAnnotations;

namespace ProjectWork.Models
{
    public class BirthdayOrder
    {
        [Key]
        public int Id { get; set; }
        public string Surname { get; set; } = null!;
        public string Name { get; set; } = null!;
        public int Amount {  get; set; }
    }
}
