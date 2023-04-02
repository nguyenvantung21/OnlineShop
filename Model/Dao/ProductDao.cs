using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class ProductDao
    {
        OnlineShopDbContext db = null;
        public ProductDao()
        {
            db = new OnlineShopDbContext();
        }

        //Client

        public List<Product> ListNewProduct(int top)
        {
            return db.Products.OrderByDescending(x=>x.CreatedDate).Take(top).ToList();
        }

        public List<Product> ListFreatureProduct(int top)
        {
            return db.Products.Where(x => x.TopHot != null & x.TopHot > DateTime.Now).OrderByDescending(x => x.CreatedDate).ToList();
        }

        public Product ViewDetail(long id)
        {
            return db.Products.Find(id);
        }




        //Admin
        public IEnumerable<Product> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Product> model = db.Products;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString));
            }
            return model.OrderBy(x => x.Status == true).ToPagedList(page, pageSize);
        }

        public Product GetByID(int id)
        {
            return db.Products.Find(id);
        }

        public long Insert(Product entity)
        {
            db.Products.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
        public bool Update(Product entity)
        {
            try
            {
                var product = db.Products.Find(entity.ID);
                product.Name = entity.Name;
                product.Description = entity.Description;
                product.Image = entity.Image;
                product.Detail = entity.Detail;
                product.Status = entity.Status;
                product.Quantity = entity.Quantity;
                product.TopHot = entity.TopHot;
                product.CategoryID = entity.CategoryID;
                product.ViewCount = entity.ViewCount;
                product.Price = entity.Price;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;

            }
        }

        public bool Delete(long id)
        {
            try
            {
                var product = db.Products.Find(id);
                db.Products.Remove(product);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool ChangeStatus(long id)
        {
            var product = db.Products.Find(id);
            product.Status = !product.Status;
            db.SaveChanges();
            return product.Status;
        }

    }
}
