using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace ArchApp.Repository
{
    public class Repository<T> : RepositoryBase where T : class    //Generic Class, T yerine hangi classı, bu classın altındaki methodlardaki işlemler onun için yapılacak.
    {
        //db Field'ini repositarybase den inherit ediyor
        private DbSet<T> _objectSet;

        public Repository()
        {
            new RepositoryBase();
            _objectSet = db.Set<T>();               // Her Methodta çağrıldığında T ye göre Dbset kontrol edilip getirileceğini, Class çağrıldığında bir kere getirilir 
        }
        public DbSet<T> Get()
        {
            return _objectSet;
        }
        public T GetFirst(Expression<Func<T, bool>> whereCondition)
        {
            return _objectSet.FirstOrDefault(whereCondition);

        }
        public T Find(int Val)
        {
            return _objectSet.Find(Val);

        }

        public List<T> List()
        {
            return _objectSet.ToList();
        }
        public List<T> List(Expression<Func<T, bool>> whereCondition)
        {
            return _objectSet.Where(whereCondition).ToList();
        }
        public int Insert(T obj)
        {
            _objectSet.Add(obj);
            return Save();
        }
        public void Update(T updateObj)
        {
            db.Entry(updateObj).State = EntityState.Modified;
             
        }
        public int FindAndUpdate(T obj, int id)
        {
            T updateObj = _objectSet.Find(id);
           
            if (updateObj != null)
            {
                
                db.Entry(updateObj).CurrentValues.SetValues(obj);
                return db.SaveChanges();
            }
            return 0;
            
        }
        public int Delete(T obj)
        {
            _objectSet.Remove(obj);
            return Save();
        }
        public int Save()
        {

            return db.SaveChanges();

        }
    }
}