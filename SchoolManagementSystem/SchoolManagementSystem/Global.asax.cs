using AutoMapper;
using DatabaseAccess;
using SchoolManagementSystem.App_Start;
using SchoolManagementSystem.Request;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SchoolManagementSystem
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            Mapper.Initialize(cfg => cfg.CreateMap<StudentTable, StudentRequest>()
            .ForMember(dest => dest.ClassID, opt => opt
                .MapFrom(src => src.ClassSectionTable.ClassTable.ClassID))
            .ForMember(dest => dest.SectionID, opt => opt
                .MapFrom(src => src.ClassSectionTable.SectionID))
            .ForMember(dest => dest.DateofBirth, opt => opt
                .MapFrom(src => src.DateofBirth.ToString("yyyy-MM-dd")))
            .ForMember(dest => dest.AddmissionDate, opt => opt
                .MapFrom(src => src.AddmissionDate.ToString("yyyy-MM-dd")))
            );
            Bootstrapper.Initialize();
        }
    }
}
