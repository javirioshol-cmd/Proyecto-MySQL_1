using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_MySQL_1.Models
{
    public class Datos
    {
        public int StaffId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int StoreId { get; set; }
        public bool Active { get; set; }
        public string Username { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}
