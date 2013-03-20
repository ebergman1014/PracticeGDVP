using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using CardShop.Models;

namespace CardShop.DAL
{
    public class RulesContext : DbContext
    {
        public RulesContext()
            : base("PracticeGDVPEntities")
        {
        }

        public DbSet<RuleSet> RuleSets { get; set; }
    }
}