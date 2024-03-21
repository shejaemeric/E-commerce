using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Commerce_Api.Data;
using E_Commerce_Api.Interfaces;
using E_Commerce_Api.Models;

namespace E_Commerce_Api.Repository
{
    public class InvetoryRepository:IInvetoryRepository
    {
        private readonly DataContext _context;
        public InvetoryRepository(DataContext context)
        {
            _context = context;
        }

        public bool CheckIfInvetoryExist(int InvetoryId)
        {
            return _context.Inventories.Any(i => i.Id ==InvetoryId);
        }

        public bool CreateInvetory(Inventory inventory)
        {
            _context.Add(inventory);
            return Save();
        }

        public ICollection<Inventory> GetAllInventories()
        {
            return _context.Inventories.OrderBy(c => c.Id).ToList();
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }

        public bool UpdateInventory( Inventory inventory)
        {
            _context.Update(inventory);
            return Save();
        }
    }
}
