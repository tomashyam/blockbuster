using CC.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CC.DAL
{
    public class UserDataAccess
    {
        private readonly CCContext _ccContext;

        public UserDataAccess(CCContext context)
        {
            _ccContext = context;
        }
    }
}
