using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SimpleShop.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public Cart Cart { get; set; }
        public List<Order> Orders { get; set; }
    }
}
