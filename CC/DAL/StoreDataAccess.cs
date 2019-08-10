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

    }
}
