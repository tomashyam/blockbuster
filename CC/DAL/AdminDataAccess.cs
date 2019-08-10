using CC.Data;
using CC.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CC.DAL
{
    public class AdminDataAccess
    {
        private readonly CCContext _ccContext;

        public AdminDataAccess(CCContext ccContext)
        {
            _ccContext = ccContext;
        }

        public IEnumerable<Comment> GetByUserIdInDateRange(DateTime start, DateTime end, string userId)
        {
            var comments = _ccContext.Comment
                .Include(usr => usr.Publisher)
                .Where(c => c.Date >= start &&
                       c.Date <= end &&
                       c.Publisher.ID == userId);
            return comments;
        }
    }
}
