using System.ComponentModel.DataAnnotations.Schema;

namespace leave_management.Data
{
    public class LeaveAllocation: BaseEntity
    {
        public int NumberOfDays { get; set; } = 0;

        [ForeignKey("LeaveTypeId")]
        public LeaveType LeaveType { get; set; }
        public int LeaveTypeId { get; set; }
                
        public string EmployeeId { get; set; }
     
    }
}
