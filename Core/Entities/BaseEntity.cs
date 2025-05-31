using Core.Interface;

namespace Core.Entities
{
    public class BaseEntity : ISoftDelete
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public virtual BaseEntity MarkAsDeleted(bool isDeleted = true)
        {
            IsDeleted = isDeleted;
            UpdatedAt = DateTime.Now;
            return this;
        }
    }
}
