using System.Configuration;
using System.Web.Mvc;
using CardShop.Auth;
using CardShop.Controllers;
using CardShop.Service;
using CardShop.Service.Admin;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Unity.Mvc4;

namespace CardShop.Utilities
{
    public class UnityConfiguration
    {
        public static void ConfigureIoCContainer()
        {
            IUnityContainer container = new UnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            RegisterTypes(container);
        }

        private static void RegisterTypes(IUnityContainer container)
        {
            UnityConfigurationSection section = (UnityConfigurationSection)ConfigurationManager.GetSection("unity");
            container.LoadConfiguration(section);

            //  Register web security
            container.RegisterInstance((IWebSecurity)new WebSecurityWrapper());
            // create bean programatically instead of using xml
            container.RegisterInstance((IDiscountService)new DiscountService());
            container.RegisterInstance((IManageUserService)new ManageUserService());
            container.RegisterInstance((IManageStoreService)new ManageStoreService());
        }


    }
}