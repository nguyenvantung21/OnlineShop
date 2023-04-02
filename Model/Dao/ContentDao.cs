using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class ContentDao
    {
        OnlineShopDbContext db = null;
        public ContentDao()
        {
            db = new OnlineShopDbContext();
        }
        public Content GetByID(int id)
        {
            return db.Contents.Find(id);
        }

        public long Insert(Content entity)
        {
            db.Contents.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
        public bool Update(Content entity)
        {
            try
            {
                var content = db.Contents.Find(entity.ID);
                content.Name = entity.Name;  
                content.MetaTitle = entity.MetaTitle;
                content.Description = entity.Description;
                content.Image = entity.Image;
                content.Detail = entity.Detail;
                content.Status = entity.Status;
                content.Warranty = entity.Warranty;
                content.TopHot = entity.TopHot;
                content.Tags = entity.Tags;
                content.CategoryID = entity.CategoryID;
                content.MetaDescriptions = entity.MetaDescriptions;
                content.ViewCount = entity.ViewCount;
                db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;

            }
        }

        public bool Delete(long id)
        {
            try
            {
                var content = db.Contents.Find(id);
                db.Contents.Remove(content); 
                db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        public IEnumerable<Content> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Content> model = db.Contents;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString) || x.Name.Contains(searchString));
            }

            return model.OrderBy(x => x.CreatedDate).ToPagedList(page, pageSize);
        }


        public bool ChangeStatus(long id)
        {
            var content = db.Contents.Find(id);
            content.Status = !content.Status;
            db.SaveChanges();
            return content.Status;
        }
    }
}
