using System.ComponentModel.DataAnnotations;

namespace leave_management.Data
{
    public class LeaveType: BaseEntity
    {
        public string Name { get; set; }
        public int DefaultDays { get; set; } = 0;
    }
}
