using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBookApi.Domain.Models
{
    public class Contact
    {
        [Key]
        public int ContactId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [ForeignKey("PhoneNumber")]
        public int PhoneId { get; set; }
        public PhoneNumber PhoneNumber { get; set; }
        public ICollection<PhoneNumber> PhoneNumbers { get; set; }
        
    }
}
