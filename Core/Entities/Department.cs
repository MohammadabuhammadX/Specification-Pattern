namespace Core.Entities
{
    public class Department : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        //public int ManagerId { get; set; }
        //public int? ParentDepartmentId { get; set; }

        public ICollection<Employee> Employees { get; set; } = new List<Employee>();


        public void UpdateDetilds(string name, string description)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be null or empty.", nameof(name));
            Name = name.Trim();
            Description = description;
        }

    }
}
