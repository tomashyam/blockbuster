using CC.DAL;
using CC.Data;
using CC.Models;
using System.Collections.Generic;
using System;
using System.Linq;

namespace CC.BL
{
    public class ProductBl
    {
        private readonly ProductDataAccess _productDataAccess;

        public ProductBl(CCContext ccContext)
        {
            _productDataAccess = new ProductDataAccess(ccContext);
        }

        public List<Movie> GetAllProducts()
        {
            Movie recommanded = null;
            try
            {
                recommanded = _productDataAccess.GetRecomandedMovie("10216907566494866");
            }
            catch (Exception ex)
            {
                // write error that cannot get recoomandation;
            }

            var movies =  _productDataAccess.GetAllProducts();
            if (recommanded != null)
            {
                movies.Remove(recommanded);
                movies.Insert(0, recommanded);
            }


            return movies;
        }

        public Movie GetProductById(int productId)
        {
            return _productDataAccess.GetProductById(productId);
        }

        public IEnumerable<Movie> GetProductsByCategory(Category category)
        {
            return _productDataAccess.GetProductsByCategory(category);
        }

        public void SaveProducts(IEnumerable<Movie> products)
        {
            _productDataAccess.SaveProducts(products);
        }

        public void SaveProdcut(Movie product)
        {
            _productDataAccess.SaveProducts(new List<Movie> { product });
        }

        public void UpdateProduct(Movie product)
        {
            _productDataAccess.UpdateProduct(product);
        }

        public void DeleteProduct(Movie product)
        {
            _productDataAccess.DeleteProduct(product);
        }

    }
}
