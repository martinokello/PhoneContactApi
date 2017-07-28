using PhoneBookApi.DataAccess;
using PhoneBookApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBookApi.Services.Interfaces
{
    public interface IPhoneBookService
    {
        PhoneNumber GetContactNumber(int contactId);
        IEnumerable<PhoneNumber> GetAllContactNumbers(int contactId);
        bool AddPhoneNumber(Contact contact, string phoneNumber);
        bool UpdatePhoneNumber(Contact contact, string phoneNumber);
        bool UpdateContact(Contact contact);
        bool DeletePhoneNumber(Contact contact, string phoneNumber);
        bool DeleteContact(Contact contact);
        PhoneBookDbContext PhoneBookDbContext { get; set; }
    }
}
