using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMSNews.Models.Context;
using CMSNews.Models.Models;
using System.Data.Entity;


namespace CMSNews.Repository.Repository
{
    public class NewsRepository : GenericRepository<News>, INewsRepository
    {
        public NewsRepository(DbCMSNewsContext context) : base(context)
        {
        }
        // اضافه شد: دریافت خبر به همراه گروه خبری و کاربر
        public News GetNewsWithDetails(int id)
        {
            return db.News
                .Include(n => n.NewsGroup)
                .Include(n => n.User)
                .FirstOrDefault(n => n.NewsId == id);
        }
    }
}
