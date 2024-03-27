using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Commerce_Api.Models;

namespace E_Commerce_Api.Interfaces
{
    public interface IInvetoryRepository
    {
        bool CheckIfInvetoryExist(int InvetoryId);
        bool CreateInvetory(Inventory inventory);

        bool UpdateInventory(Inventory inventory,int actionPeformerId, string referenceId);
        ICollection<Inventory> GetAllInventories();

        bool DeleteInventory(int inventoryId, int actionPeformerId, string referenceId);
        bool Save();
    }
}
