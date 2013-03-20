using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Collections;
using System.Linq.Expressions;

namespace CardShop.Daos
{
    /// <summary>
    /// A wrapper for DbSet.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class DbSetWrapper<TEntity> : IDbSet<TEntity> where TEntity : class
    {
        private DbSet<TEntity> db {get;set;}

        /// <summary>
        /// Constructor, takes in Entity. Sets entity for wrapper class
        /// </summary>
        /// <param name="db"> Entity found in PracticeGDVPEntities.
        ///  If confused, look in file. Use field names found there.</param>
        public DbSetWrapper(DbSet<TEntity> db)
        {
            this.db = db;
            this.ElementType = ((IQueryable)db).ElementType;
            this.Expression = ((IQueryable)db).Expression;
            this.Provider = ((IQueryable)db).Provider;
        }
        /// <summary>
        /// Wrapper for Add in DbSet
        /// </summary>
        /// <param name="entity">Entity found in PracticeGDVPEntities.
        ///  If confused, look in file. Use field names found there.</param>
        /// <returns></returns>
        public TEntity Add(TEntity entity)
        {
            return db.Add(entity);
        }
        /// <summary>
        /// Attaches an Entity to the context.
        /// </summary>
        /// <param name="entity">Entity found in PracticeGDVPEntities.
        ///  If confused, look in file. Use field names found there.</param>
        /// <returns></returns>
        public TEntity Attach(TEntity entity)
        {
            return db.Attach(entity);
        }
        /// <summary>
        /// Creates an instance of the derived entity. Does not Add!!
        /// </summary>
        /// <typeparam name="TDerivedEntity">Entity found in PracticeGDVPEntities.
        ///  If confused, look in file. Use field names found there.</typeparam>
        /// <returns></returns>
        public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, TEntity
        {
            return db.Create<TDerivedEntity>();
        }
        /// <summary>
        /// Creates an instance of the derived entity. Does not Add!!
        /// </summary>
        /// <returns></returns>
        public TEntity Create()
        {
            return db.Create();
        }
        /// <summary>
        /// Find an entity with the given value at the given location.
        /// Example: id = 17. set a new User,  User.Id = 17.  Find(User.Id) will find
        /// where User.Id = 17
        /// </summary>
        /// <param name="keyValues">The values of the primary key for the entity to be found.
        /// </param>
        /// <returns></returns>
        public TEntity Find(params object[] keyValues)
        {
            return db.Find(keyValues);
        }
        /// <summary>
        /// Marks entity as ready to delete. must call context.SaveChanges
        /// to delete.
        /// </summary>
        /// <param name="entity">Entity found in PracticeGDVPEntities.
        ///  If confused, look in file. Use field names found there.</param>
        /// <returns></returns>
        public TEntity Remove(TEntity entity)
        {
            return db.Remove(entity);
        }
        /// <summary>
        /// Changes DbSet to List.
        /// </summary>
        /// <returns></returns>
        public List<TEntity> ToList()
        {
            return db.ToList();
        }
        /// <summary>
        /// Executes a query.
        /// </summary>
        /// <param name="sql">SQL String </param>
        /// <param name="parameters"> parameters for SQL string</param>
        /// <returns></returns>
        public DbSqlQuery<TEntity> SqlQuery(string sql, params object[] parameters)
        {
            return db.SqlQuery(sql, parameters);
        }
        /// <summary>
        /// Returns enumerator that enumarates through collection
        /// </summary>
        /// <returns></returns>
        public IEnumerator<TEntity> GetEnumerator()
        {
            return ((IEnumerable<TEntity>)db).GetEnumerator();
        }
        /// <summary>
        /// Gets the element Type from the execution of the tree
        /// </summary>
        public Type ElementType { get; set; }
        /// <summary>
        /// Get expression
        /// </summary>
        public Expression Expression { get; set; }
        /// <summary>
        /// get provider
        /// </summary>
        public IQueryProvider Provider{get;set;}

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)db).GetEnumerator();
        }
    }
}