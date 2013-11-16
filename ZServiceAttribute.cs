using System;

namespace WcfService
{
	public class ZServiceAttribute : Attribute
	{
		public ZServiceAttribute()
		{
		}
		
		public String EndpointAddress{ get; set;}
	}
}

