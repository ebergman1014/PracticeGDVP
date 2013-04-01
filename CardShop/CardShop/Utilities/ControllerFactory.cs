using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CardShop.Controllers;
using CardShop.Models;
using Microsoft.Practices.Unity;

namespace CardShop.Utilities
{
    public class ControllerFactory : DefaultControllerFactory
    {
        public override IController CreateController(
        System.Web.Routing.RequestContext requestContext,
        string controllerName)
        {
            try
            {
                if (!controllerName.Equals("BaseballCard"))
                {
                    return base.CreateController(requestContext, controllerName);
                }
                else
                {
                    // Need to add an entry to the web.config <appSettings> section:
                    // <add key="BaseballCardRepository" value="BaseballCardRepository"/> 
                    string repositoryTypeName =
                        System.Configuration.ConfigurationManager.AppSettings["BaseballCardRepository"];

                    // Get the actual .NET type of repository we need
                    Type repositoryType = Type.GetType("CardShop.Models." + repositoryTypeName);

                    // Create the container
                    var container = new UnityContainer();

                    // Setup a mapping between IFishRepository and the configured concrete repository type
                    container.RegisterType(typeof(IBaseballCardRepository), repositoryType);

                    // Create the FishController and auto-inject the required 
                    // repository dependency into its constructor...
                    return container.Resolve<BaseballCardController>();
                }

            }
            catch
            {
                return base.CreateController(requestContext, controllerName);
            }
        }
    }
}