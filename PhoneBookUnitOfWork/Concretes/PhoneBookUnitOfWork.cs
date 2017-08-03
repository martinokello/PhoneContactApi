using PhoneBookApi.DataAccess;
using PhoneBookApi.UnitOfWork.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBookApi.UnitOfWork.Concretes
{
    public class PhoneBookUnitOfWork : IUnitOfWork
    {

        public PhoneBookUnitOfWork() {
            PhoneBookDbContext = new PhoneBookDbContext();
        }
        public PhoneBookDbContext PhoneBookDbContext{ get;set;}
        public bool SaveChanges()
        {
            PhoneBookDbContext.SaveChanges();
            return true;
        }
    }
}
