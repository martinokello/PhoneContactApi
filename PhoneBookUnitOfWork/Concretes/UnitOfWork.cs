using PhoneBookApi.DataAccess;
using PhoneBookApi.Services.Interfaces;
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
        public IPhoneBookService _phoneBookService;

        public PhoneBookUnitOfWork(IPhoneBookService phoneBookService) {
            _phoneBookService = phoneBookService;
            _phoneBookService.PhoneBookDbContext = new PhoneBookDbContext();
            PhoneBookDbContext = _phoneBookService.PhoneBookDbContext;
        }
        public PhoneBookDbContext PhoneBookDbContext{ get;set;}
        public bool SaveChanges()
        {
            PhoneBookDbContext.SaveChanges();
            return true;
        }
    }
}
