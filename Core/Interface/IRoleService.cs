using Core.Entities;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface
{
    public interface IRoleService
    {
        Task<Role> GetRoleByIdAsync(int id);
        Task<IReadOnlyList<Role>> GetAllRolesAsync();
        Task<Role> AddRoleAsync(Role role);
        Task<Role> UpdateRoleAsync(Role role);
        Task DeleteRoleAsync(int id);
        Task<bool> RoleExistsAsync(int id);
    }
}
