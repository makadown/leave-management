using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Models
{
   /* public class LeaveTypeViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
    }*/

    public class DetailsLeaveTypeViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Nombre")]
        public string Name { get; set; }
        [Display(Name="Fecha Creación")]
        public DateTime DateCreated { get; set; }
    }

    public class CreateLeaveTypeViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}
