using System;
using System.ServiceModel;

namespace WcfService
{
	public class ServiceClient<T> : ClientBase<T> where T : class
	{
		public static T Service { get; private set; }
		static ServiceClient()
		{ 
			new ServiceClient<T>();
		}
		private ServiceClient() : base(new BasicHttpBinding{Namespace = Namespace()},
				new EndpointAddress(EndpointAddress()))
		{
			 Service = Channel;
		}
		
		private static String Namespace()
		{
			return typeof(T).GetAttributeValue((ServiceContractAttribute a) => a.Namespace);
		}
		
		private static String EndpointAddress()
		{
			return typeof(T).GetAttributeValue((ZServiceAttribute a) => a.EndpointAddress);
		}
	}
}


