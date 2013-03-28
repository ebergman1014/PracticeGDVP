using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using CardShop.Daos;
using CardShop.Models;

namespace CardShop.Service
{
    public class UploadService : IUploadService
    {
        public IPracticeGDVPDao dbContext { get; set; }
        public UploadService()
        {
            this.dbContext = PracticeGDVPDao.GetInstance();
        }

        public List<BaseballCard> LoadFromFile(string filename)
        {
            BaseballCard card;
            List<BaseballCard> allCards = new List<BaseballCard>();

            if (File.Exists(filename))
            {
                using (FileStream file = File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (var reader = new StreamReader(file))
                    {
                        while (!reader.EndOfStream)
                        {
                            string line = reader.ReadLine();
                            if (!String.IsNullOrEmpty(line))
                            {
                                card = new BaseballCard();

                                string[] lineArray = Regex.Split(line, ",");

                                try
                                {
                                    card.Player = lineArray[0];
                                    card.Team = lineArray[1];
                                    card.Cost = Convert.ToDecimal(lineArray[2]);
                                    allCards.Add(card);
                                }
                                catch (FormatException ex)
                                {
                                    Debug.WriteLine(ex.Message);
                                }
                            }
                        }
                    }
                }
                return SaveImport(allCards);
            }
            return null;
        }

        private List<BaseballCard> SaveImport(List<BaseballCard> allCards)
        {
            List<BaseballCard> savedCards = new List<BaseballCard>();
            foreach (BaseballCard card in allCards)
            {
                dbContext.BaseballCards().Add(card);
                savedCards.Add(card);
            }

            dbContext.SaveChanges();
            return savedCards;
        }
    }
}