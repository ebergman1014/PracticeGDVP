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

        public List<BaseballCard> GetAllCards()
        {
            return cards.ToList<BaseballCard>();
        }

        public BaseballCard GetBaseballCard(int id) { throw new NotImplementedException(); }
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