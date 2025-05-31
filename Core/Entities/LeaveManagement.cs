namespace Core.Entities
{
    public class LeaveManagement : BaseEntity
    {
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public int? ApprovedBy { get; set; }
        public string Comments { get; set; }

        public string LeaveType { get; set; }

        public LeaveManagement(int employeeId, DateTime startDate, DateTime endDate, string leaveType, string reason)
        {
            EmployeeId = employeeId;
            StartDate = startDate;
            EndDate = endDate;
            LeaveType = leaveType;
            Reason = reason;
            Status = LeaveStatus.Pending.ToString();
        }

        public void ApproveLeave(int approverId, DateTime approvedDate, string comments = null)
        {
            Status = LeaveStatus.Approved.ToString();
            ApprovedBy = approverId;
            ApprovedDate = approvedDate;
            Comments = comments;
        }

        public void RejectLeave(string comments = null)
        {
            Status = LeaveStatus.Rejected.ToString();
            Comments = comments;
        }
    }
}