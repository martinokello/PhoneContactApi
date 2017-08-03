using System.Web.Http;
using System.Reflection;
using PhoneBookApi.DataAccess.Interfaces;
using PhoneBookApi.Services.Concretes;
using PhoneBookApi.Services.Interfaces;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using PhoneBookApi.UnitOfWork.Concretes;
using PhoneBookApi.UnitOfWork.Interfaces;

namespace PhoneBookApi.AppStart
{
    public static class UnityConfig
    {
        public static void Register(HttpConfiguration config)
        {

            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IRepositoryPhoneMarker, PhoneBookApi.DataAccess.Concretes.PhoneNumberRepository>();
            container.RegisterType<IRepositoryContactMarker, PhoneBookApi.DataAccess.Concretes.ContactRepository>();
            container.RegisterType<IPhoneBookService, PhoneBookService>();
            container.RegisterType<IUnitOfWork, PhoneBookUnitOfWork>();

            var types = Assembly.GetExecutingAssembly().GetTypes();

            foreach (var t in types)
            {
                if (t.IsAssignableFrom(typeof(System.Web.Http.Controllers.IHttpController)))
                {
                    container.RegisterType(t);
                }
            }
            config.DependencyResolver = new WebApiContrib.IoC.Unity.UnityResolver(container);

        }
    }
}