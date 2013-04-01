using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardShop.Models
{
    public interface IBaseballCardRepository
    {
        List<BaseballCard> GetAllCards();
        BaseballCard GetBaseballCard(int id);
        void Create(BaseballCard baseballCard);
        void Delete(int id);
        void SaveChanges();
    }
}
