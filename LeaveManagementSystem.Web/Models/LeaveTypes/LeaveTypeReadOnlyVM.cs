namespace LeaveManagementSystem.Web.Models.LeaveTypes
{
    public class LeaveTypeReadOnlyVM
    {
        // this is going to have similar structure as the leave type model
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Days { get; set; }
    }
}