using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using CardShop.Models;

namespace CardShop.Daos
{
    public class DefaultContext : DbContext
    {
        public DefaultContext()
            : base("PracticeGDVPEntities")
        {
        }

        public DbSet<BaseballCard> BaseballCards { get; set; }
        public DbSet<RuleSet> RuleSets { get; set; }
    }
}