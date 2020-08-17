using System.Collections.Generic;

namespace server.Environment
{
	public class EnvironmentManager : IManager
	{
		public List<object> _environment = new List<object>();

		public EnvironmentManager()
		{
			_environment.Add(new CarRent());
		}
	}
}
