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
        public Content GetByID(long id)
        {
            return db.Contents.Find(id);
        }

        //public IEnumerable<Content> ListAllPaging(string searchString, int page, int pageSize)
        //{
        //    IQueryable<Content> model = db.Contents;
        //    if (!string.IsNullOrEmpty(searchString))
        //    {
        //        model = model.Where(x => x.Name.Contains(searchString) || x.Name.Contains(searchString));
        //    }

        //    return model.OrderBy(x => x.CreatedDate).ToPagedList(page, pageSize);
        //}

    }
}
