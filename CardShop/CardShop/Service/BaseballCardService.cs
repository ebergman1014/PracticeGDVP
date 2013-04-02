using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using CardShop.Models;

namespace CardShop.Service
{
    public class BaseballCardService : DbContext, IBaseballCardService
    {
        
        // Specify the name of the connection string in the call to the base class constructor
        public BaseballCardService() : base("DefaultConnection") {}
        
        // List of BaseballCard - automatically populated by the EF
        // as long as the property is public and of type DbSet<T>
        public DbSet<BaseballCard> cards { get; set; }

        /// <summary>
        /// Calls the DBSet<BaseballCard>.ToList() Method and returns all cards in list format.
        /// </summary>
        /// <returns>List<CardShop.Models.BaseballCard></returns>
        public List<BaseballCard> GetAllCards()
        {
            return cards.ToList<BaseballCard>();
        }

        /// <summary>
        /// Finds a single BaseballCard by the Id passed into the method. calls DBSet<BaseballCard>.Find(id)
        /// </summary>
        /// <param name="id">Id of the baseballCard to be found in DBSet</param>
        /// <returns>CardShop.Models.BaseballCard</returns>
        public BaseballCard GetBaseballCard(int id) 
        {
            return cards.Find(id);
        }

        /// <summary>
        /// Adds a new BaseballCard to DBSet<BaseballCard>
        /// </summary>
        /// <param name="baseballCard">BaseballCard to be added to the DBSet</param>
        public void Create(BaseballCard baseballCard) 
        {
            cards.Add(baseballCard);
        }

        /// <summary>
        /// Updates BaseballCard in DBSet with information
        /// </summary>
        /// <param name="baseballCard">BaseballCard to update</param>
        public void Update(BaseballCard baseballCard)
        {
            base.Entry(baseballCard);
            SaveChanges();
        }

        /// <summary>
        /// This method is not implemented yet
        /// </summary>
        /// <param name="id">Id of the card to be deleted from the DBSet</param>
        public void Delete(int id) { throw new NotImplementedException(); }

        /// <summary>
        /// Calls DBContext.SaveChanges to save anything that may have changed in the DBContext
        /// </summary>
        public void SaveChanges() 
        {
            base.SaveChanges();
        }

        /// <summary>
        /// Override to the DBContext Method to map entity to right table.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Map the table name to our actual table name ("BaseballCard")
            modelBuilder.Entity<BaseballCard>().ToTable("BaseballCard");
        }
    }
}