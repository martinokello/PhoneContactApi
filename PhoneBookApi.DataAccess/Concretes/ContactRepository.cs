using PhoneBookApi.DataAccess.Interfaces;
using PhoneBookApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBookApi.DataAccess.Concretes
{
    public class ContactRepository : IRepository<Contact>, IRepositoryContactMarker
    {
        public PhoneBookDbContext PhoneBookDbContext { get; set; }

        public virtual bool Add(Contact item)
        {
            PhoneBookDbContext.Contacts.Add(item);
            return true;
        }

        public virtual bool Delete(Contact item)
        {
            PhoneBookDbContext.Contacts.Remove(item);
            return true;
        }

        public virtual IEnumerable<Contact> GetAll()
        {
            return PhoneBookDbContext.Contacts.ToList();
        }

        public virtual Contact GetById(int id)
        {
            return PhoneBookDbContext.Contacts.SingleOrDefault(p => p.ContactId == id);
        }

        public virtual bool Update(Contact item)
        {
            var contact = PhoneBookDbContext.Contacts.SingleOrDefault(p => p.FirstName == item.FirstName && p.LastName == item.LastName);
            return true;
        }
    }
}
