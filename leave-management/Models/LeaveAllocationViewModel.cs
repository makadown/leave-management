using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Models
{
    public class LeaveAllocationViewModel
    {
        public int Id { get; set; }
        public int NumberOfDays { get; set; }
        public DateTime DateCreated { get; set; }
        public int Period { get; set; }
        public EmployeeViewModel Employee { get; set; }
        public string EmployeeId { get; set; } 
        public LeaveTypeViewModel LeaveType { get; set; }
        public int LeaveTypeId { get; set; }
        /// <summary>
        /// Representa el dropdownList que tendrá el formulario para 
        /// empleados
        /// </summary>
        public IEnumerable<SelectListItem> Employees { get; set; }
        /// <summary>
        /// Representa el dropdownList que tendrá el formulario para LeaveTypes
        /// </summary>
        public IEnumerable<SelectListItem> LeaveTypes { get; set; }
    }

    /// <summary>
    /// modelo para usar cuando se crea una nueva asignacion
    /// </summary>
    public class CreateLeaveAllocationVM {
        public int NumberUpdated { get; set; }
        public List<LeaveTypeViewModel> LeaveTypes { get; set; }
    }
}
