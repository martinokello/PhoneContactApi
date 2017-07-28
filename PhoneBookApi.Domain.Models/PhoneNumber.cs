using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBookApi.Domain.Models
{
    public class PhoneNumber
    {
        [Key]
        public int phoneId { get; set; }
        public string Phone { get; set; }
        [ForeignKey("Contact")]
        public int ContactId { get; set; }
        public Contact Contact { get; set; }
    }
}
