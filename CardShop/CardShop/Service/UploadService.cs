using System;
using System.Collections.Generic;
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

        public void LoadFromFile(string filename)
        {
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
                                BaseballCard card = new BaseballCard();

                                string[] lineArray = Regex.Split(line, ", ");

                                // validate?

                                card.Player = lineArray[0];
                                card.Team = lineArray[1];
                                card.Cost = Convert.ToInt32(lineArray[2]);
                                allCards.Add(card);
                            }
                        }
                    }
                }
                SaveImport(allCards);
            }
        }

        private void SaveImport(List<BaseballCard> allCards)
        {
            foreach (var card in allCards)
            {
                dbContext.BaseballCards().Add(card);
            }

            dbContext.SaveChanges();
        }
    }
}