using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace CardShop.Daos
{
    public class DbSetWrapper<TEntity> : IDbSet<TEntity> where TEntity : class
    {
        private DbSet<TEntity> db {get;set;}

        public DbSetWrapper(DbSet<TEntity> db)
        {
            this.db = db;
        }

        public TEntity Add(TEntity entity)
        {
            return db.Add(entity);
        }

        public TEntity Attach(TEntity entity)
        {
            return db.Attach(entity);
        }

        public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, TEntity
        {
            return db.Create<TDerivedEntity>();
        }

        public TEntity Create()
        {
            return db.Create();
        }

        public TEntity Find(params object[] keyValues)
        {
            return db.Find(keyValues);
        }

        public TEntity Remove(TEntity entity)
        {
            return db.Remove(entity);
        }

        public List<TEntity> ToList()
        {
            return db.ToList();
        }

        public DbSqlQuery<TEntity> SqlQuery(string sql, params object[] parameters)
        {
            return db.SqlQuery(sql, parameters);
        }
    }
}