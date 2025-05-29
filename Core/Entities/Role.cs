namespace Core.Entities
{
    public class Role : BaseEntity
    {
        public string Name { get; set; }
        public string Permissions { get; set; }
        //public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }

}
