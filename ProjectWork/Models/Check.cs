using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectWork.Models
{
    public class Check
    {
        [Key]
        public int Hour {  get; set; }
        public double AverageCheck { get; set; }
    }
}
