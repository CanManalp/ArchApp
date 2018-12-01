using ArchApp.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace ArchApp.Models
{
    public class Search<T> where T : class
    {
        public List<T> MSearch(Expression<Func<T, bool>> whereCondition)
        {
            DbContextApp db = new DbContextApp();           
                return db.Set<T>().Where(whereCondition).ToList();
            }


        
    }
}