using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Models
{
    public class EmployeeViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        [Display(Name = "Teléfono")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Nombre")]
        public string FirstName { get; set; }
        [Display(Name = "Apellido")]
        public string LastName { get; set; }
        public string TaxId { get; set; }
        [Display(Name = "Fecha de Nacimiento")]
        public DateTime DateOfBirth { get; set; }
        public DateTime DateJoined { get; set; }
    }
}
