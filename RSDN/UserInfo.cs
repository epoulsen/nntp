using System;

namespace Rsdn.RsdnNntp.Public
{
	/// <summary>
	/// Summary description for UserInfo.
	/// </summary>
	public class UserInfo : IUserInfo
	{
		protected Rsdn.RsdnNntp.RsdnService.UserInfo serviceUserInfo;

		public UserInfo(Rsdn.RsdnNntp.RsdnService.UserInfo serviceUserInfo)
		{
			this.serviceUserInfo = serviceUserInfo;
		}

		public int ID
		{
			get { return serviceUserInfo.ID; }
		}
		public string Origin
		{
			get { return serviceUserInfo.Origin; }
		}

		public string Name
		{
			get { return serviceUserInfo.Name; }
		}
		public string Password
		{
			get { return serviceUserInfo.Password; }
			set { serviceUserInfo.Password = value; }
		}
		public FormattingStyle MessageFormat
		{
			get
			{
				switch (serviceUserInfo.MessageFormat)
				{
					case Rsdn.RsdnNntp.RsdnService.MessageFormat.Text :
						return FormattingStyle.PlainText;
					case Rsdn.RsdnNntp.RsdnService.MessageFormat.Html :
					case Rsdn.RsdnNntp.RsdnService.MessageFormat.TextHtml :
					default :
						return FormattingStyle.Html;
				}
			}
		}
	}
}