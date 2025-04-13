using System.ComponentModel.DataAnnotations;

namespace ProjectWork.Models
{
    public class AverageCheck
    {
        [Key]
        public int Hour {  get; set; }
        public int Check { get; set; }
    }
}
