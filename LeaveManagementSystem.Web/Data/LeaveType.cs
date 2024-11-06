using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeaveManagementSystem.Web.Data;

public class LeaveType
{
    public int Id { get; set; }
    [Column(TypeName = "nvarchar(150)")]
    [Display(Name = "Nombre")]
    public string Name { get; set; } = string.Empty;
    [Display(Name = "Número de dias")]
    public int NumberOfDays { get; set; }
}
