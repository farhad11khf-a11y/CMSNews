using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMSNews.Models.Context;
using CMSNews.Models.Models;



namespace CMSNews.Repository.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T:BaseEntity
    {
        DbCMSNewsContext db;
        DbSet<T> dbSet;
        public GenericRepository( DbCMSNewsContext context)
        {
            db = context;
            dbSet = context.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            return dbSet.ToList();
        }
        public T GetEntity(int id)
        {
            return dbSet.Find(id);
        }

        public bool Add(T Entity)
        {
            try
            {
                dbSet.Add(Entity);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
           
        }
        public bool Update(T Entity)
        {

            try
            {
                db.Entry(Entity).State = EntityState.Modified; 
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool Delete(T Entity)
        {
            try
            {
                db.Entry(Entity).State = EntityState.Deleted;
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool Delete(int id)
        {
            var Entity = GetEntity(id); 
            try
            {
                db.Entry(Entity).State = EntityState.Deleted;
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public void Save()
        {
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();   
        }





    }
}
