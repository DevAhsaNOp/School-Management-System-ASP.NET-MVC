using SchoolManagementSystem.Repository;
using System.Web.Mvc;
using Unity;
using Unity.AspNet.Mvc;

namespace SchoolManagementSystem.App_Start
{
    public class Bootstrapper
    {
        public static IUnityContainer Initialize()
        {
            var container = BuildUnityContainer();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            return container;
        }

        private static UnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();
            container.RegisterType<IResumeRepository, ResumeRepository>();

            RegisterTypes(container);
            return container;
        }

        public static void RegisterTypes(IUnityContainer container)
        {

        }
    }
}