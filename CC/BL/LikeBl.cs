using System;
using CC.DAL;
using CC.Data;

namespace CC.BL
{
    public class LikeBl
    {
        private readonly ProductDataAccess _productDal;
        private readonly UserDataAccess _userDal;

        public LikeBl(CCContext context)
        {
            _productDal = new ProductDataAccess(context);
            _userDal = new UserDataAccess(context);
        }

        public void Add(string productId, string userId)
        {
            var like = new Models.Like
            {
                User = _userDal.GetUser(userId)
            };

            _productDal.AddLike(int.Parse(productId), like);
        }
    }
}
