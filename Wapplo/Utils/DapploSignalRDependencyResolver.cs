using System;
using System.Collections.Generic;
using System.Linq;
using Dapplo.Addons;
using Microsoft.AspNet.SignalR;

namespace Wapplo.Utils
{
	/// <summary>
	/// This DependencyResolver makes sure that we can inject exported services into SignalR hubs
	/// </summary>
	public class DapploSignalRDependencyResolver : DefaultDependencyResolver
	{
		private readonly IServiceLocator _serviceLocator;

		/// <summary>
		/// Constructor with IBootstrapper
		/// </summary>
		/// <param name="serviceLocator">IBootstrapper</param>
		public DapploSignalRDependencyResolver(IServiceLocator serviceLocator)
		{
			_serviceLocator = serviceLocator;
		}

		/// <inheritdoc />
		public override object GetService(Type serviceType)
		{
			var obj = base.GetService(serviceType);
			if (obj != null)
			{
				_serviceLocator.FillImports(obj);
				return obj;
			}
			return _serviceLocator.GetExport(serviceType);
		}
	}
}
