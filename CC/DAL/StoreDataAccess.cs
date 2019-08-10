using CC.Data;
using CC.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CC.DAL
{
    public class StoreDataAccess
    {
        private readonly CCContext _ccContext;

        public StoreDataAccess(CCContext ccContext)
        {
            _ccContext = ccContext;
        }

        public void UpdateStore(Cinema storeToUpdate)
        {
            _ccContext.Cinema.Update(storeToUpdate);
            _ccContext.SaveChanges();
        }

        public Cinema GetStoreById(int id)
        {
            return _ccContext.Cinema
                .SingleOrDefault(x => x.ID == id);
        }

        public List<Cinema> GetAllStores()
        {
            return _ccContext.Cinema
                .ToList();
        }

        public IEnumerable<Cinema> GetStoreByName(string name)
        {
            return _ccContext.Cinema
                                .Where(s => s.Name.Contains(name))
                                .ToList();
        }

        public Cinema GetStroeById(int storeId)
        {
            return _ccContext.Cinema
                .SingleOrDefault(store => store.ID == storeId);
        }

        public void AddStore(Cinema store)
        {
            _ccContext.Cinema.Add(store);
            _ccContext.SaveChanges();
        }

        public void DeleteStore(Cinema store)
        {
            _ccContext.Cinema.Remove(store);
            _ccContext.SaveChanges();
        }
    }
}
