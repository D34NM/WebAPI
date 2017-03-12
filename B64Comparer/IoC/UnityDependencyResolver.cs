using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;

namespace B64Comparer.IoC
{
	public class UnityDependencyResolver : IDependencyResolver
	{
		protected IUnityContainer _container;

		public UnityDependencyResolver(IUnityContainer container)
		{
			_container = container;
		}

		public object GetService(Type serviceType)
		{
			try
			{
				return _container.Resolve(serviceType);
			}
			catch (ResolutionFailedException)
			{
				return null;
			}
		}

		public IEnumerable<object> GetServices(Type serviceType)
		{
			try
			{
				return _container.ResolveAll(serviceType);
			}
			catch (ResolutionFailedException)
			{
				return new List<object>();
			}
		}

		public IDependencyScope BeginScope()
		{
			var child = _container.CreateChildContainer();
			return new UnityDependencyResolver(child);
		}

		public void Dispose()
		{
			_container.Dispose();
		}
	}
}