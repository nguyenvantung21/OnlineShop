using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class CategoryDao
    {
        OnlineShopDbContext db = null;
        public CategoryDao()
        {
            db = new OnlineShopDbContext();
        }

        //Client

        public ProductCategory ViewDetail (long id)
        {
            return db.ProductCategories.Find(id);
        }





        //Admin
        public List<Category> ListAll()
        {
            return db.Categories.Where(x=>x.Status==true).ToList();
        }
        public IEnumerable<Category> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Category> model = db.Categories;
            if(!string.IsNullOrEmpty(searchString) )
            {
                model = model.Where(x=>x.Name.Contains(searchString));
            }
            return model.OrderBy(x => x.Status == true).ToPagedList(page, pageSize);
        }


        public long Insert(Category category)
        {
            db.Categories.Add(category);
            db.SaveChanges();
            return category.ID;
        }

        public Category GetByID(long id)
        {
            return db.Categories.Find(id);
        }
        public bool Update(Category entity)
        {
            try
            {
                var category = db.Categories.Find(entity.ID);
                category.Name= entity.Name;
                category.MetaTitle = entity.MetaTitle;
                category.Status = entity.Status;
                category.DisplayOrder = entity.DisplayOrder;
                db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var category = db.Categories.Find(id);
                db.Categories.Remove(category);
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
            var category = db.Categories.Find(id);
            category.Status = !category.Status;
            db.SaveChanges();
            return category.Status;
        }

    }
}
