using PhoneBookApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoneBookApi.Domain.Models;
using PhoneBookApi.DataAccess.Interfaces;
using PhoneBookApi.DataAccess.Concretes;
using PhoneBookApi.DataAccess;

namespace PhoneBookApi.Services.Concretes
{
    public class PhoneBookService : IPhoneBookService
    {
        private PhoneNumberRepository _phoneRepository;
        private ContactRepository _contactRepository;

        public PhoneBookService()
        {

        }
        public PhoneBookDbContext PhoneBookDbContext { get; set; }
        public ContactRepository ContactRepository { get; set; }
        public PhoneNumberRepository PhoneRepository { get; set; }
        public PhoneBookService(IRepositoryContactMarker contactRepository, IRepositoryPhoneMarker phoneRepository)
        {
            PhoneRepository = _phoneRepository = phoneRepository as PhoneNumberRepository;
            ContactRepository = _contactRepository = contactRepository as ContactRepository;
        }
        public bool AddPhoneNumber(Contact contact, string phoneNumber)
        {
            Contact contArg = null;

            if (contact.ContactId > 0)
            {
                contArg = _contactRepository.GetById(contact.ContactId);
                if(contArg!=null)
                contact = contArg;
                else _contactRepository.Add(contact);
            }
            else _contactRepository.Add(contact);
            _phoneRepository.Add(new PhoneNumber { Contact = contact, ContactId = contact.ContactId, Phone = phoneNumber});
            return true;
        }

        public bool DeletePhoneNumber(Contact contact, string phoneNumber)
        {
            var phone = _phoneRepository.GetAll().SingleOrDefault(p => p.Phone.Equals(phoneNumber, StringComparison.OrdinalIgnoreCase));
            _phoneRepository.Delete(phone);
            return true;
        }

        public IEnumerable<PhoneNumber> GetAllContactNumbers(int contactId)
        {
            return _phoneRepository.GetAll().Where(p => p.ContactId == contactId);
        }

        public PhoneNumber GetContactNumber(int contactId)
        {
            return _phoneRepository.GetAll().Where(p => p.ContactId == contactId).FirstOrDefault();
        }
        public bool UpdateContact(Contact contact)
        {
            var con = _contactRepository.GetAll().SingleOrDefault(p => p.ContactId == contact.ContactId);
            con.FirstName = contact.FirstName;
            con.LastName = contact.LastName;
            return true;
        }
        public bool UpdatePhoneNumber(Contact contact, string phoneNumber)
        {
            var mainPhone = _phoneRepository.GetAll().Where(p => p.ContactId == contact.ContactId).FirstOrDefault();
            if(mainPhone != null)
            {
                mainPhone.Phone = phoneNumber;
                return true;
            }
            return false;
        }

        public bool DeleteContact(Contact contact)
        {
            _contactRepository.Delete(contact);
            return true;
        }
    }
}
