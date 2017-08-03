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
using PhoneBookApi.UnitOfWork.Concretes;
using PhoneBookApi.UnitOfWork.Interfaces;

namespace PhoneBookApi.Services.Concretes
{
    public class PhoneBookService : IPhoneBookService
    {
        private PhoneNumberRepository _phoneRepository;
        private ContactRepository _contactRepository;
        private IUnitOfWork _unitOfWork;

        public PhoneBookDbContext PhoneBookDbContext { get; set; }
        public ContactRepository ContactRepository { get; set; }
        public PhoneNumberRepository PhoneRepository { get; set; }

        public PhoneBookService()
        {

        }
        public PhoneBookService(IRepositoryContactMarker contactRepository, IRepositoryPhoneMarker phoneRepository, IUnitOfWork UnitOfWork)
        {
            PhoneRepository = _phoneRepository = phoneRepository as PhoneNumberRepository;
            ContactRepository = _contactRepository = contactRepository as ContactRepository;
            _unitOfWork = UnitOfWork;
            PhoneRepository.PhoneBookDbContext = (_unitOfWork as PhoneBookUnitOfWork).PhoneBookDbContext;
            ContactRepository.PhoneBookDbContext = (_unitOfWork as PhoneBookUnitOfWork).PhoneBookDbContext;
        }
        public bool AddPhoneNumber(int contactId, PhoneNumber phoneNumber)
        {
            Contact contArg = null;

            if (contactId > 0)
            {
                AddPhone(phoneNumber);
                _unitOfWork.SaveChanges();
                contArg = _contactRepository.GetById(contactId);
                if (contArg != null)
                {
                    contArg.PhoneNumber = phoneNumber;
                    contArg.PhoneId = phoneNumber.PhoneId;
                    _unitOfWork.SaveChanges();
                }
                else
                {
                    AddContact(contArg);
                    _unitOfWork.SaveChanges();
                    contArg.PhoneNumber = phoneNumber;
                    contArg.PhoneId = phoneNumber.PhoneId;
                    _unitOfWork.SaveChanges();
                }
                return true;
            }
            else return false;
        }

        public bool DeletePhoneNumber(Contact contact, string phoneNumber)
        {
            var phone = _phoneRepository.GetAll().SingleOrDefault(p => p.Phone.Equals(phoneNumber, StringComparison.OrdinalIgnoreCase));
            _phoneRepository.Delete(phone);
            _unitOfWork.SaveChanges();
            return true;
        }

        public IEnumerable<PhoneNumber> GetAllContactNumbers(int contactId)
        {
            var contact = _contactRepository.GetById(contactId);
            return _phoneRepository.GetAll().Where(p => p.PhoneId == contact.PhoneId);
        }

        public PhoneNumber GetContactNumber(int contactId)
        {
            var contact = _contactRepository.GetById(contactId);
            return _phoneRepository.GetAll().Where(p => p.PhoneId == contact.PhoneId).FirstOrDefault();
        }
        public bool UpdateContact(Contact contact)
        {
            var con = _contactRepository.GetAll().SingleOrDefault(p => p.ContactId == contact.ContactId);
            con.FirstName = contact.FirstName;
            con.LastName = contact.LastName;
            _unitOfWork.SaveChanges();
            return true;
        }
        public bool UpdatePhoneNumber(Contact contact, string phoneNumber)
        {
            var mainContact = _contactRepository.GetById(contact.ContactId);
            var mainPhone=_phoneRepository.GetAll().Where(p=>p.PhoneId == mainContact.PhoneId).FirstOrDefault();

            if(mainPhone != null)
            {
                mainPhone.Phone = phoneNumber;

                _unitOfWork.SaveChanges();
                return true;
            }
            return false;
        }

        public bool DeleteContact(Contact contact)
        {
            _contactRepository.Delete(contact);
            _unitOfWork.SaveChanges();
            return true;
        }

        public bool AddContact(Contact contact)
        {
            var result = _contactRepository.Add(contact);

            _unitOfWork.SaveChanges();
            return result;
        }
        public bool AddPhone(PhoneNumber phoneNumber)
        {
            var result = _phoneRepository.Add(phoneNumber);
            _unitOfWork.SaveChanges();
            return result;
        }
    }
}
