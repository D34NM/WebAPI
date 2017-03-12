using B64Comparer.Data;
using B64Comparer.Data.Contexts;
using B64Comparer.IoC;
using B64Comparer.Repositories.Implementations;
using B64Comparer.Repository.Repositories;
using B64Comparer.Repository.UnitsOfWork;
using B64Comparer.Repository.UnitsOfWork.Implementations;
using B64Comparer.Services;
using Microsoft.Practices.Unity;
using System.Web.Http;

namespace B64Comparer
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			// Web API configuration and services
			IUnityContainer container = new UnityContainer();

			#region Dependency registrations

			container.RegisterType<IComparerContext, B64ComparerContext>();
			container.RegisterType<ICompareService, CompareService>();
			container.RegisterType<IRepository, B64Repository>();
			container.RegisterType<IUnitOfWork, UnitOfWork>();

			#endregion

			config.DependencyResolver = new UnityDependencyResolver(container);

			// Web API routes
			config.MapHttpAttributeRoutes();
		}
	}
}
