using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Http.Results;
using Moq;
using PhoneBookApi.Controllers;
using PhoneBookApi.DataAccess.Concretes;
using PhoneBookApi.Domain.Models;
using PhoneBookApi.Services.Concretes;
using PhoneBookApi.Services.Interfaces;
using PhoneBookApi.UnitOfWork.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace PhoneBookApi.Tests
{
    public class FakeUnitOfWork:IUnitOfWork
    {
        public bool SaveChanges()
        {
            return true;
        }
        
    }
    [TestClass]
    public class PhoneBookControllerTest
    {
        private IUnitOfWork _unitOfWork;
        private PhoneBookService _phoneBookService;
        private PhoneBookController controller; 

        private Contact contacts1 = null;
        private Contact contacts2 = null;
        private List<PhoneNumber> phoneNumbers1 = null;
        private List<PhoneNumber> phoneNumbers2 = null;
        public PhoneBookControllerTest() { }

        [TestInitialize]
        public void SetUp()
        {
            contacts1 = new Contact { ContactId = 1, FirstName = "Martin", LastName = "Okello"};
            phoneNumbers1 = new List<PhoneNumber> { new PhoneNumber { PhoneId = 1,  Phone = "03945785856"} };
            contacts2 = new Contact { ContactId = 2, FirstName = "Joe", LastName = "Bloggs" };
            phoneNumbers2 = new List<PhoneNumber> { new PhoneNumber { PhoneId = 2,  Phone = "056956856785" } };

            var contacts = new List<Contact> { contacts1, contacts2 };
            var phoneNumbers = phoneNumbers1.Union(phoneNumbers2);

            _unitOfWork = new FakeUnitOfWork();

            var phoneBookRepository = new Mock<PhoneNumberRepository>();
            var contactRepository = new Mock<ContactRepository>();

            phoneBookRepository.Setup(p => p.Add(It.IsAny<PhoneNumber>())).Returns(true);
            phoneBookRepository.Setup(p => p.Update(It.IsAny<PhoneNumber>())).Returns(true);
            phoneBookRepository.Setup(p => p.Delete(phoneNumbers.FirstOrDefault(q=> q.Phone== "03945785856"))).Returns(true);
            phoneBookRepository.Setup(p => p.GetAll()).Returns(phoneNumbers);
            phoneBookRepository.Setup(p => p.GetById(1)).Returns(phoneNumbers.SingleOrDefault(q=> q.PhoneId ==1));

            contactRepository.Setup(p => p.Add(It.IsAny<Contact>())).Returns(true);
            contactRepository.Setup(p => p.Update(It.IsAny<Contact>())).Returns(true);
            contactRepository.Setup(p => p.Delete(contacts.FirstOrDefault(q => q.ContactId == 1))).Returns(true);
            contactRepository.Setup(p => p.GetAll()).Returns(contacts);
            contactRepository.Setup(p => p.GetById(1)).Returns(contacts.SingleOrDefault(q => q.ContactId == 1));

            _phoneBookService = new PhoneBookService(contactRepository.Object, phoneBookRepository.Object, new FakeUnitOfWork());

            controller = new PhoneBookController(_phoneBookService);
            controller.PhoneBookUnitOfWork = _unitOfWork;
        }
        [TestMethod]
        public void Test_Add_Contact_AddsContact()
        {
            try
            {
                IHttpActionResult success = controller.AddContact(contacts1, "0987464644");
                Assert.IsInstanceOfType(success, typeof(OkNegotiatedContentResult<bool>));

                var result = success as OkNegotiatedContentResult<bool>;
                Assert.IsTrue(result.Content.ToString().ToLower().Contains("true"));
            }
            catch
            {
                throw new Exception();
            }
        }

        [TestMethod]
        public void Test_UpdateContactNumber_Updates_Contacts_PhoneNumber()
        {
            try
            {
                string phoneNumber = "0459595955956";
                var success = controller.UpdateContactNumber(contacts1, phoneNumber);
                Assert.IsInstanceOfType(success, typeof(OkNegotiatedContentResult<bool>));

                var result = success as OkNegotiatedContentResult<bool>;
                Assert.IsTrue(result.Content.ToString().ToLower().Contains("true"));
            }
            catch
            {
                throw new Exception();
            }
        }

        [TestMethod]
        public void Test_GetContactNumber_Gets_Contacts_Number()
        {
            try
            {
                var success = controller.GetContactNumber(1);

                Assert.IsInstanceOfType(success, typeof(OkNegotiatedContentResult<PhoneNumber>));
            }
            catch
            {
                throw new Exception();
            }
        }

        [TestMethod]
        public void Test_GetAllContactNumbers_Gets_All_Contacts_Numbers()
        {
            try
            {
                var contactId = 1;
                var success = controller.GetAllContactNumbers(contactId);
                Assert.IsInstanceOfType(success, typeof(OkNegotiatedContentResult<IEnumerable<PhoneNumber>>));
            }
            catch
            {
                throw new Exception();
            }
        }

        [TestMethod]
        public void Test_DeleteContact_Deletes_Contact()
        {
            try
            {
                var success = controller.DeleteContact(contacts1);
                Assert.IsInstanceOfType(success, typeof(OkNegotiatedContentResult<bool>));

                var result = success as OkNegotiatedContentResult<bool>;
                Assert.IsTrue(result.Content.ToString().ToLower().Contains("true"));
            }
            catch
            {
                throw new Exception();
            }
        }

        [TestMethod]
        public void Test_DeleteContactPhoneNumber_Deletes_Contacts_Number()
        {
            try
            {
                var phoneNumber1 = "056956856785";

                var success = controller.DeleteContactPhoneNumber(contacts2, phoneNumber1);
                Assert.IsInstanceOfType(success, typeof(OkNegotiatedContentResult<bool>));

                var result = success as OkNegotiatedContentResult<bool>;
                Assert.IsTrue(result.Content.ToString().ToLower().Contains("true"));
            }
            catch
            {
                throw new Exception();
            }
        }
    }
}
