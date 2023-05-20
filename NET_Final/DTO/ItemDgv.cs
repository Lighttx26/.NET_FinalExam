using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET_Final.DTO
{
    internal class ItemDgv
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public bool Gender { get; set; }
        public int PositionId { get; set; }
        public string PositionName { get; set; }
        public double Salary { get; set; }
    }
}
