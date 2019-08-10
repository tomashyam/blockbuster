using CC.Data;
using CC.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CC.DAL
{
    public class ProductDataAccess
    {
        private readonly CCContext _ccContext;

        public ProductDataAccess(CCContext ccContext)
        {
            _ccContext = ccContext;
        }

        public List<Movie> GetAllProducts()
        {
            return _ccContext
                .Movie
                .Include(p => p.Comments)
                .ThenInclude(c => c.Publisher)
                .Include(p => p.Likes)
                .ThenInclude(l => l.User)
                .ToList();
        }

        public Movie GetRecomandedMovie(string userId)
        {
            var allUserFavorits = _ccContext.Like.Where(l => l.User.ID == userId).Select(l=> l.MovieID).ToList();
            var userTopPopularMovie = _ccContext.Like.Include(l=> l.User).ToList().Where(l => allUserFavorits.Contains(l.MovieID)).ToList()
                .GroupBy(l => l.MovieID).OrderByDescending(g=> g.Count()).FirstOrDefault()?.Key;
            var allUsersWhoLikeThatMoive = _ccContext.Like.Where(l => l.MovieID == userTopPopularMovie && l.User.ID != userId).Select(l=> l.User);
            var topRecomanded = _ccContext.Like.Where(l=> allUsersWhoLikeThatMoive.Contains(l.User) && !allUserFavorits.Contains(l.MovieID)).GroupBy(l=> l.MovieID)
                .OrderByDescending(g=> g.Count()).FirstOrDefault()?.Key;
            return _ccContext.Movie.Where(m => m.ID == topRecomanded).FirstOrDefault();
        }

        public Movie GetProductById(int productId)
        {
            return _ccContext.Movie
                                  .Include(p => p.Comments)
                                  .ThenInclude(c => c.Publisher)
                                  .Include(p => p.Likes)
                                  .ThenInclude(l => l.User)
                                .FirstOrDefault(x => x.ID == productId);
        }

        public IEnumerable<Movie> GetProductsByCategory(Category category)
        {
            return _ccContext.Movie
                .Include(p => p.Comments)
                .ThenInclude(c => c.Publisher)
                .Include(p => p.Likes)
                .ThenInclude(l => l.User)
                    .Where(p => p.Category == category)
                    .AsEnumerable();
        }

        public void SaveProducts(IEnumerable<Movie> products)
        {
            _ccContext.Movie.AddRange(products);
            _ccContext.SaveChanges();
        }

        public void AddComment(int productId, Comment comment)
        {
            this.GetProductById(productId).Comments.Add(comment);
            _ccContext.SaveChanges();
        }

        public void AddLike(int productId, Like like)
        {
            this.GetProductById(productId).Likes.Add(like);
            _ccContext.SaveChanges();
        }

        public void UpdateProduct(Movie product)
        {
            _ccContext.Movie.Update(product);
            _ccContext.SaveChanges();
        }

        public void DeleteProduct(Movie product)
        {
            var productToDelete = _ccContext
                .Movie
                .Include(p => p.Comments)
                .Include(p => p.Likes)
                .SingleOrDefault(p => p.ID == product.ID);

            if (productToDelete == null)
            {
                throw new
                    Exception(string.Format("could not find product with id {0}", product.ID));
            }

            _ccContext.Like.RemoveRange(productToDelete.Likes);
            _ccContext.Comment.RemoveRange(productToDelete.Comments);
            _ccContext.Movie.Remove(productToDelete);
            _ccContext.SaveChanges();
        }
    }
}
