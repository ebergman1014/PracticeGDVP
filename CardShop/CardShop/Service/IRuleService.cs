using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CardShop.Models;
using CardShop.ViewModels;

namespace CardShop.Service
{
    public interface IRuleService
    {
        /// <summary>
        /// Retrieve all the current rulesets.
        /// </summary>
        /// <returns></returns>
        List<Models.RuleSet> GetAllRulesets();

        /// <summary>
        /// Create a new ruleset.
        /// </summary>
        /// <param name="rulesetWrapper"></param>
        /// <returns></returns>
        Models.RuleSet Create(Models.RuleSet rulesetWrapper);

        /// <summary>
        /// Edit an already existing ruleset.
        /// </summary>
        /// <param name="rulesetWrapper"></param>
        /// <returns></returns>
        Models.RuleSet Edit(Models.RuleSet rulesetWrapper);

        /// <summary>
        /// Retrieve the details of the given ruleset.
        /// </summary>
        /// <param name="id">The Id of the ruleset requested.</param>
        /// <returns></returns>
        RulesetDetails Details(int id);

        /// <summary>
        /// Removes the ruleset with the matching Id if it exists.
        /// </summary>
        /// <param name="id"></param>
        void Delete(int id);
    }
}