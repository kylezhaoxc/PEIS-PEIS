using log4net;
using System;
using System.Reflection;

namespace PEIS.Common
{
	public class Log4J
	{
		private static readonly ILog _instance = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		public static ILog Instance
		{
			get
			{
				return Log4J._instance;
			}
		}
	}
}
