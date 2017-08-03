using PhoneBookApi.DataAccess.Interfaces;
using PhoneBookApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBookApi.DataAccess.Concretes
{
    public class PhoneNumberRepository : IRepository<PhoneNumber>, IRepositoryPhoneMarker
    {
        public PhoneBookDbContext PhoneBookDbContext { get; set; }
        public virtual bool Add(PhoneNumber item)
        {
            PhoneBookDbContext.PhoneNumbers.Add(item);
            return true;
        }

        public virtual bool Delete(PhoneNumber item)
        {
             var phone = PhoneBookDbContext.PhoneNumbers.SingleOrDefault(p => p.PhoneId == item.PhoneId);
            if (phone != null)
            {
                PhoneBookDbContext.PhoneNumbers.Remove(phone);
                return true;
            }
            return false;
        }

        public virtual IEnumerable<PhoneNumber> GetAll()
        {
            return PhoneBookDbContext.PhoneNumbers.ToList();
        }

        public virtual PhoneNumber GetById(int id)
        {
            return PhoneBookDbContext.PhoneNumbers.SingleOrDefault(p => p.PhoneId == id);
        }

        public virtual bool Update(PhoneNumber item)
        {
            var phone = PhoneBookDbContext.PhoneNumbers.SingleOrDefault(p => p.PhoneId == item.PhoneId);
            if(phone != null)
            {
                phone.Phone = item.Phone;
                return true;
            }
            return false;
        }
    }
}
