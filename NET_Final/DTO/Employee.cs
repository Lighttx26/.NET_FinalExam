using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET_Final.DTO
{
    public class Employee
    {
        [Key]
        [Required]
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public bool Gender { get; set; }
        public double Rate { get; set; }
        //public double Salary { get; set; }
        public int PositionId { get; set; }
        [ForeignKey("PositionId")]
        public virtual Position Position { get; set; }

    }
}
