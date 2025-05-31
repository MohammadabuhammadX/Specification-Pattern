using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface
{
    public interface IDepartmentService
    {
        Task<IReadOnlyList<Department>> GetAllDepartmentAsync();
        Task<Department> GetDepartmentByIdAsync(int id);
        Task<Department> CreateDepartmentAsync(Department department);
        Task<Department> UpdateDepartmentAsync(Department department);
        Task DeleteDepartmentAsync(int id);
        Task RestoreDepartmentAsync(int id);
        Task<Department> GetDepartmentByIdIncludingDeletedAsync(int id);

    }
}
