using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CardShop.Models;

namespace CardShop.Service
{
    public interface IBaseballCardService
    {
        /// <summary>
        /// Gets list of all BaseballCards in DBSet
        /// </summary>
        /// <returns>List of CardShop.Models.BaseballCard</returns>
        List<BaseballCard> GetAllCards();

        /// <summary>
        /// Finds BaseballCard in DBSet with matching Id
        /// </summary>
        /// <param name="id">Id of card to be found in DBSet</param>
        /// <returns>CardShop.Models.BaseballCard</returns>
        BaseballCard GetBaseballCard(int id);

        /// <summary>
        /// Adds new BaseballCard to DBSet
        /// </summary>
        /// <param name="baseballCard">CardShop.Models.BaseballCard</param>
        void Create(BaseballCard baseballCard);

        /// <summary>
        /// Updates BaseballCard in DBSet
        /// </summary>
        /// <param name="baseballCard">CardShop.Models.BaseballCard</param>
        void Update(BaseballCard baseballCard);

        /// <summary>
        /// Deletes BaseballCard from DBSet. This method currently throws NotImplmentedException
        /// </summary>
        /// <param name="id">Id of the card to be removed form the DBSet</param>
        void Delete(int id);

        /// <summary>
        /// Calls DBContext.SaveChanges method to save any changes to DBSet
        /// </summary>
        void SaveChanges();
    }
}
