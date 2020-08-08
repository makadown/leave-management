using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Models
{
    public class LeaveRequestViewModel
    {
        public int Id { get; set; }
        public EmployeeViewModel RequestingEmployee { get; set; }
        [Display(Name ="Nombre del empleado")]
        public string RequestingEmployeeId { get; set; }
        [Display(Name = "Fecha inicio")]
        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [Display(Name = "Fecha fin")]
        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        public LeaveTypeViewModel LeaveType { get; set; }
        public int LeaveTypeId { get; set; }
        [Display(Name = "Fecha Petición")]
        public DateTime DateRequested { get; set; }
        [Display(Name = "Fecha accionada")]
        public DateTime DateActioned { get; set; }
        [Display(Name = "Estatus")]
        public bool? Approved { get; set; }
        public EmployeeViewModel ApprovedBy { get; set; }
        public string ApprovedById { get; set; }
        /*
        /// <summary>
        /// Representa el dropdownList que tendrá el formulario para LeaveTypes
        /// </summary>
        public IEnumerable<SelectListItem> LeaveTypes { get; set; }*/
    }

    public class AdminLeaveRequestViewVM 
    {
        [Display(Name = "Total de peticiones")]
        public int TotalRequests { get; set; }
        [Display(Name = "Peticiones aprobadas")]
        public int ApprovedRequests { get; set; }
        [Display(Name = "Peticiones pendientes")]
        public int PendingRequests { get; set; }
        [Display(Name = "Peticiones rechazadas")]
        public int RejectedRequests { get; set; }
        public List<LeaveRequestViewModel> LeaveRequests { get; set; }
    }

    public class CreateLeaveRequestVM 
    {
        [Display(Name = "Fecha inicio")]
        [Required]
        public DateTime StartDate { get; set; }
        [Display(Name = "Fecha fin")]
        [Required]
        public DateTime EndDate { get; set; }
        public IEnumerable<SelectListItem> LeaveTypes { get; set; }
        [Display(Name ="Tipo de Licencia")]
        public int LeaveTypeId { get; set; }
    }
}
