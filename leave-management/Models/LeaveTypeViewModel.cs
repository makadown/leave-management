using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Models
{
    public class LeaveTypeViewModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Nombre")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Dias predeterminados")]
        [Range(1,25, ErrorMessage ="El rango debe ser entre 1 y 25")]
        public int DefaultDays { get; set; }
        [Display(Name="Fecha Creación")]
        public DateTime? DateCreated { get; set; }
    }
}
