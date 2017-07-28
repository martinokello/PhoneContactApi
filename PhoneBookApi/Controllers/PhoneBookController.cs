using PhoneBookApi.Domain.Models;
using PhoneBookApi.Services.Concretes;
using PhoneBookApi.Services.Interfaces;
using PhoneBookApi.UnitOfWork.Concretes;
using PhoneBookApi.UnitOfWork.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PhoneBookApi.Controllers
{
    [Authorize]
    public class PhoneBookController : ApiController
    {
        private IUnitOfWork _unitOfWork;
        private IPhoneBookService _phoneBookService;
        public PhoneBookController() { }
        public PhoneBookController(IPhoneBookService service) {
            _phoneBookService = service;
            _unitOfWork = new PhoneBookUnitOfWork(service);
            (service as PhoneBookService).PhoneRepository.PhoneBookDbContext = (_unitOfWork as PhoneBookUnitOfWork).PhoneBookDbContext;
            (service as PhoneBookService).ContactRepository.PhoneBookDbContext = (_unitOfWork as PhoneBookUnitOfWork).PhoneBookDbContext;
        }

        public IUnitOfWork PhoneBookUnitOfWork
        {
            set { _unitOfWork = value; }
        } 

        [HttpPost]
        [Route("api/PhoneBook/AddContact/{phoneNumber}")]
        public IHttpActionResult AddContact([FromBody] Contact contact, string phoneNumber)
        {

            try
            {
                var success =_phoneBookService.AddPhoneNumber(contact, phoneNumber);
                _unitOfWork.SaveChanges();
                return Ok<bool>(success);
            }
            catch(Exception e)
            {
                return BadRequest();
            }
        }
        [HttpPut]
        [Route("api/PhoneBook/UpdateContactNumber/{phoneNumber}")]
        public IHttpActionResult UpdateContactNumber([FromBody] Contact contact, string phoneNumber)
        {
            try
            {
                var success = _phoneBookService.UpdatePhoneNumber(contact, phoneNumber);
                _unitOfWork.SaveChanges();
                return Ok<bool>(success);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
        [HttpGet]
        [Route("api/PhoneBook/GetContactNumber/{contactId}")]
        public IHttpActionResult GetContactNumber(int contactId)
        {
            try
            {
                var success = _phoneBookService.GetContactNumber(contactId);
                return Ok<PhoneNumber>(success);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
        [HttpGet]
        [Route("api/PhoneBook/GetAllContactNumbers/{contactId}")]
        public IHttpActionResult GetAllContactNumbers(int contactId)
        {
            try
            {
                var success = _phoneBookService.GetAllContactNumbers(contactId);
                return Ok<IEnumerable<PhoneNumber>>(success);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
        [HttpPost]
        [Route("api/PhoneBook/DeleteContact/{contact}")]
        public IHttpActionResult DeleteContact([FromBody] Contact contact)
        {
            try
            {
                var success = _phoneBookService.DeleteContact(contact);
                _unitOfWork.SaveChanges();
                return Ok<bool>(success);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
        [HttpPost]
        [Route("api/PhoneBook/DeleteContactPhoneNumber/{phoneNumber}")]
        public IHttpActionResult DeleteContactPhoneNumber([FromBody] Contact contact,string phoneNumber)
        {
            try
            {
                var success = _phoneBookService.DeletePhoneNumber(contact, phoneNumber);
                _unitOfWork.SaveChanges();
                return Ok<bool>(success);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
        
    }
}
