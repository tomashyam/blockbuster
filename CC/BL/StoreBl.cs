using CC.DAL;
using CC.Data;
using CC.Models;
using System.Collections.Generic;
using System.Linq;

namespace CC.BL
{
    public class StoreBl
    {
        private readonly StoreDataAccess _storeDataAccess;

        public StoreBl(CCContext ccContext)
        {
            _storeDataAccess = new StoreDataAccess(ccContext);
        }

        public List<Cinema> GetAllStores()
        {
            return _storeDataAccess.GetAllStores();
        }

        public IEnumerable<Cinema> GetStoreByName(string name)
        {
            return _storeDataAccess.GetStoreByName(name);
        }

        public void AddStore(Cinema store)
        {
            _storeDataAccess.AddStore(store);
        }

        public Cinema GetStoreById(int id)
        {
            return _storeDataAccess.GetStoreById(id);
        }

        public void UpdateStore(Cinema storeToUpdate)
        {
            _storeDataAccess.UpdateStore(storeToUpdate);
        }

        public void DeleteStore(Cinema store)
        {
            _storeDataAccess.DeleteStore(store);
        }
    }
}
