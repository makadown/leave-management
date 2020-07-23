﻿using Microsoft.AspNetCore.Mvc.Rendering;
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
        [Required]
        public int NumberOfDays { get; set; }
        public DateTime DateCreated { get; set; }
        public EmployeeViewModel Employee { get; set; }
        public string EmployeeId { get; set; } 
        public DetailsLeaveTypeViewModel LeaveType { get; set; }
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
}
