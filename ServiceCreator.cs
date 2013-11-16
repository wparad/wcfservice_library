using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Threading;

namespace WcfService
{
	public class ServiceCreator<TService, TContract> where TContract : class where TService : TContract 
	{
		public ServiceCreator()
		{
		}
		
		public void Create()
		{
			try{
				var endpointAddress = typeof(TContract).GetAttributeValue((ZServiceAttribute a) => a.EndpointAddress);
				var @namespace = typeof(TContract).GetAttributeValue((ServiceContractAttribute a) => a.Namespace);
		        using(var host = new ServiceHost(typeof(TService), new Uri(endpointAddress)))
		        {
		            host.AddServiceEndpoint(typeof(TContract), new BasicHttpBinding{Namespace = @namespace}, "");
		            host.Description.Behaviors.Add(new ServiceMetadataBehavior{HttpGetEnabled = true});
	/*              var smb = host.Description.Behaviors.Find<ServiceMetadataBehavior>();
		            if (smb == null)
		            {
		                    smb = new ServiceMetadataBehavior{};
		                    smb.HttpGetEnabled = true;
		                    // smb.MetadataExporter.PolicyVersion = PolicyVersion.Policy15;
		                    host.Description.Behaviors.Add(smb);
		            }
		
		*/   //           host.AddServiceEndpoint(ServiceMetadataBehavior.MexContractName, MetadataExchangeBindings.CreateMexHttpBinding(), "mex");
		
		            host.Open();
					while(true){ Thread.Sleep(10000); }
				}
			}
			catch(Exception exception)
			{
				throw new AggregateException(
					String.Format("Problem starting servce: {0} : {1}", typeof(TService).ToString(), typeof(TContract).ToString()), exception);
			}
		}
	}
}

