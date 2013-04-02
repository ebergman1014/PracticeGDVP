using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CardShop.Models
{
    public class BaseballCardRepository : DbContext, IBaseballCardRepository
    {

        // Specify the name of the connection string in the call to the base class constructor
        public BaseballCardRepository() : base("DefaultConnection") { }

        // List of BaseballCard - automatically populated by the EF
        // as long as the property is public and of type DbSet<T>
        public DbSet<BaseballCard> cards { get; set; }

        /// <summary>
        /// Gets all the baseball cards that currently exist in the DbSet of BaseballCards.
        /// </summary>
        /// <returns></returns>
        public List<BaseballCard> GetAllCards()
        {
            return cards.ToList<BaseballCard>();
        }


        /// <summary>
        /// Retrieves the Baseball Card that has the matching ID.
        /// </summary>
        /// <param name="id">Id of the baseball card wanted.</param>
        /// <returns>The baseball card with the matching ID.
        /// Throws a KeyNotFoundException if the given ID doesn't exist.</returns>
        public BaseballCard GetBaseballCard(int id)
        {
            if (cards.Find(id) != null)
            {
                return cards.Find(id);
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

        public void Create(BaseballCard baseballCard) { throw new NotImplementedException(); }
        public void Delete(int id) { throw new NotImplementedException(); }
        public void SaveChanges() { throw new NotImplementedException(); }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Map the table name to our actual table name ("BaseballCard")
            modelBuilder.Entity<BaseballCard>().ToTable("BaseballCard");
        }
    }
}