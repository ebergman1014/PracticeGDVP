using System.Configuration;
using System.Web.Mvc;
using CardShop.Service;
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
            //container.LoadConfiguration(section);
            container.RegisterType<IBaseballCardService, BaseballCardService>(new TransientLifetimeManager());
        }
    }
}