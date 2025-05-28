namespace API.Dtos
{
    public class EmployeeToReturnDto
    {
        public int Id { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public string Email { get; set; }
        public string Phone { get; set; }
        public string JobTitle { get; set; }
        public string DepartmentName { get; set; }
        public string RoleName { get; set; }
        public bool Status { get; set; }

    }
}
