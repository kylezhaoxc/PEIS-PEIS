using System;

namespace PEIS.Common
{
	public class SecretConfig
	{
		public static readonly string AllowIPs = BaseConfig.GetConfigValue("AllowIPs");

		public static readonly string LimitIPs = BaseConfig.GetConfigValue("LimitIPs");

		public static readonly string IsDataEncryption = BaseConfig.GetConfigValue("IsDataEncryption");

		private static char _userseparator = '&';

		public static char UserSeparator
		{
			get
			{
				return SecretConfig._userseparator;
			}
		}
	}
}
