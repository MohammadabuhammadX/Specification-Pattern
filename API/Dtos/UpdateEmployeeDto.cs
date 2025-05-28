namespace API.Dtos
{
    public class UpdateEmployeeDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime? TerminationDate { get; set; }
        public string JobTitle { get; set; } = string.Empty;
        public decimal Salary { get; set; }
        public bool Status { get; set; }
        public int DepartmentId { get; set; }
        public int RoleId { get; set; }
    }
}
