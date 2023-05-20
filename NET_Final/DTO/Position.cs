using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET_Final.DTO
{
    public class Position
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PositionId {  get; set; }
        public string PositionName { get; set; }
        public double PositionRate { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public Position()
        {
            Employees = new HashSet<Employee>();
        }
    }
}
