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
        /// <returns>A List of all the Rulesets available.</returns>
        List<RuleSet> GetAllRulesets();

        /// <summary>
        /// Create a new ruleset.
        /// </summary>
        /// <param name="rulesetWrapper"></param>
        /// <returns>The Ruleset that was just created.</returns>
        RuleSet Create(RuleSet rulesetWrapper);

        /// <summary>
        /// Edit an already existing ruleset.
        /// </summary>
        /// <param name="rulesetWrapper"></param>
        /// <returns></returns>
        RuleSet Edit(RuleSet rulesetWrapper);

        /// <summary>
        /// Retrieve the details of the given ruleset.
        /// </summary>
        /// <param name="id">The Id of the ruleset requested.</param>
        /// <returns>The details of the Ruleset with the given ID.</returns>
        RulesetDetails Details(int id);
                
        /// <summary>
        /// Removes the ruleset with the matching Id if it exists.
        /// </summary>
        /// <param name="id">The ID of the item you want to delete.</param>
        void Delete(int id);

        /// <summary>
        /// Upload a file located on the given file path.
        /// </summary>
        /// <param name="path">The file path to the file to upload.</param>
        /// <returns>The List of Rulesets that were created from the uploaded file.</returns>
        List<RuleSet> Upload(string path);

        List<RuleObject> DeserializeJSONRulesObject(string ruleObj);

        string CompileRuleset(System.Workflow.Activities.Rules.RuleSet ruleset, List<RuleObject> rulesObject);
    }
}