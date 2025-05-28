using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Employee : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime? TerminationDate { get; set; }
        public string JobTitle { get; set; }
        public decimal Salary { get; set; }
        public bool Status { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
