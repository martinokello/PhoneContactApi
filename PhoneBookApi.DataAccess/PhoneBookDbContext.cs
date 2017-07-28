using PhoneBookApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBookApi.DataAccess
{
    public class PhoneBookDbContext : DbContext
    {
        public DbSet<Contact> Contacts{get;set; }
        public DbSet<PhoneNumber> PhoneNumbers { get; set; }

    }
}
