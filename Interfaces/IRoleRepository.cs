using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Commerce_Api.Models;

namespace E_Commerce_Api.Interfaces
{
    public interface IRoleRepository
    {

        bool Save();

        Role GetOneRole(int roleId);

        bool CheckIfRoleExist(int roleId);

        bool CreateRole(Role role);

        bool UpdateRole(Role role);

        bool DeleteRole(int roleId,int actionPeformerId, string referenceId);

        public ICollection<Role> GetAllRoles();

    }
}
