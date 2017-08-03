using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using CountingKs.Data;
using NewApi.Data;

namespace WebApi
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<ICountingKsRepository, CountingKsRepository>();
            container.RegisterType<CountingKsEntities, CountingKsEntities>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}