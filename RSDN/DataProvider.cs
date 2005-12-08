using System;
using System.IO;
using System.Net;
using System.Reflection;

using Microsoft.Web.Services2.Security;
using Microsoft.Web.Services2.Security.Tokens;
using Plumbwork.Orange.Compression;
using Rsdn.Framework.Common;
using Rsdn.Nntp;
using Rsdn.RsdnNntp.Common;
using Rsdn.RsdnNntp.Public.RsdnService;

namespace Rsdn.RsdnNntp.Public
{
  /// <summary>
  /// RSDN Data Provider
  /// </summary>
  public class RsdnDataPublicProvider : RsdnDataCommonProvider
  {
		/// <summary>
		/// Construct RSDN Data Provider
		/// </summary>
    public RsdnDataPublicProvider() : base()
    {
    	webService = new Service2();
			// set compression filters
			webService.Pipeline.InputFilters.Add(new CompressionInputFilter());
    }

    /// <summary>
    /// RSDN forums' web-service proxy
    /// </summary>
    protected Service2 webService;

		protected void SetUsernameToken(UsernameToken userToken)
		{
			if (!webService.RequestSoapContext.Security.Tokens.Contains(userToken))
			{
				webService.RequestSoapContext.Security.Tokens.Add(userToken);
				MessageSignature sig = new MessageSignature(userToken);
				webService.RequestSoapContext.Security.Elements.Add(sig);
				webService.RequestSoapContext.Security.Timestamp.TtlInSeconds = 60;
			}
 		}

  	/// <summary>
  	/// Set data provider's parameters from IUserInfo object.
  	/// </summary>
  	/// <param name="userInfo"></param>
  	protected override void SetUserInfo(IUserInfo userInfo)
  	{
  		base.SetUserInfo(userInfo);
			SetUsernameToken(	
				new UsernameToken(userInfo.Name, userInfo.Password, PasswordOption.SendHashed));
  	}

  	public override IGroup InternalGetGroup(string groupName)
  	{
			try
			{
				return new Group(webService.GroupInfo(groupName));
			}		
			catch (Exception exception)
			{
				try
				{
					ProcessException(exception);
					return null;
				}
				catch (DataProviderException providerEx)
				{
					if (providerEx.Error == DataProviderErrors.NoSuchGroup)
					{
						throw new DataProviderException(DataProviderErrors.NoSuchGroup, groupName, providerEx);
					}
					else
						throw;
				}
			}
  	}

  	protected override IGroup[] GetGroupList(DateTime startTime)
  	{
			try
			{
				group_list groupList = webService.GetGroupList(startTime);
				Group[] groups = new Group[groupList.groups.Length];
				for (int i = 0; i < groups.Length; i++)
					groups[i] = new Group(groupList.groups[i]);
				return groups;
			}				
			catch (Exception exception)
			{
				ProcessException(exception);
				return null;
			}	
  	}

  	protected override IArticle GetArticle(int articleNumber, string groupName)
  	{
			try
			{
				return new Article(webService.GetArticle(groupName, articleNumber));
			}
			catch (Exception exception)
			{
				ProcessException(exception);
				return null;
			}	
  	}

  	protected override IArticle GetArticle(int messageID)
  	{
			try
			{
				return new Article(webService.GetArticleByID(messageID));
			}	
			catch (Exception exception)
			{
				ProcessException(exception);
				return null;
			}	
		}

  	/// <summary>
  	/// Autentificate user.
  	/// </summary>
  	/// <param name="user">Username.</param>
  	/// <param name="pass">Password.</param>
  	/// <param name="ip">User's IP.</param>
  	/// <returns>User's info. Nulll if user is not authentificated.</returns>
  	public override IUserInfo InternalAuthentificate(string user, string pass, IPAddress ip)
  	{
			try
			{
				
				SetUsernameToken(new UsernameToken(user, Utils.GetPasswordHash(user, pass),
					PasswordOption.SendHashed));
				webService.Authentication();
				return new UserInfo(webService.GetUserInfo());
			}	
			catch (Exception exception)
			{

				ProcessException(exception);
				return null;
			}	
  	}

  	protected override IUserInfo GetUserInfoByID(int id)
  	{
			try
			{
				return new UserInfo(webService.GetUserInfoByID(id));
			}			
			catch (Exception exception)
			{
				ProcessException(exception);
				return null;
			}
  	}

  	protected override IArticle[] GetArticleList(string groupName, int startNumber, int endNumber)
  	{
			try
			{
		    article_list articleList = webService.ArticleList(groupName, startNumber, endNumber);
				// sometimes web-service return null....
				if (articleList != null)
				{
					IArticle[] iArticles = new IArticle[articleList.articles.Length];
					for (int i = 0; i < iArticles.Length; i++)
						iArticles[i] = new Article(articleList.articles[i]);
					return iArticles;
				}
				else
					return new Article[0];
    	}
			catch (Exception exception)
			{
			  ProcessException(exception);
				return null;
			}	
  	}

  	protected override IArticle[] GetArticleList(string[] groups, DateTime startTime)
  	{
			try
			{
				article_list articleList = webService.ArticleListFromDate(groups, startTime);
				Article[] iArticles = new Article[articleList.articles.Length];
				for (int i = 0; i < iArticles.Length; i++)
					iArticles[i] = new Article(articleList.articles[i]);
				return iArticles;
			}
			catch (Exception exception)
			{
				ProcessException(exception);
				return null;
			}	
  	}

  	protected override string PostFile(string filename, string mimeType, byte[] content)
  	{
			try
			{
				return webService.PostFile(filename, mimeType, content);
			}
			catch (Exception exception)
			{
				ProcessException(exception);
				return null;
			}	
  	}

  	protected override void PostMessage(int mid, string group, string subject, string message)
  	{
			try
			{
				webService.PostMessage(mid, group,
					Utils.ProcessInvalidXmlCharacters(subject),
					Utils.ProcessInvalidXmlCharacters(message));
			}
			catch (Exception exception)
			{
				ProcessException(exception);
			}	
  	}

    /// <summary>
    /// Process exception raised during request to data provider
    /// </summary>
    /// <param name="exception"></param>
    protected void ProcessException(Exception exception)
    {
    	if (typeof(InvalidOperationException).IsAssignableFrom(exception.GetType()))
    		// check for System.InvalidOperationException (html instead xml in answer),
				// System.Net.WebException (connection problems)
    		throw new DataProviderException(DataProviderErrors.ServiceUnaviable, exception);

			if (exception.Message.IndexOf("1 Incorrect group name.") >= 0)
				throw new DataProviderException(DataProviderErrors.NoSuchGroup, exception);
			else if (exception.Message.IndexOf("2 Incorrect login name or password") >= 0)
				throw new DataProviderException(DataProviderErrors.NoPermission, exception);
			else if (exception.Message.IndexOf("WSE563") >= 0)
				throw new DataProviderException(DataProviderErrors.NoPermission, exception);
			else if (exception.Message.IndexOf("3 Article not found.") >= 0)
				throw new DataProviderException(DataProviderErrors.NoSuchArticle, exception);
			else
				throw exception;
		}

		/// <summary>
		/// Identity of assembly for information purposes
		/// </summary>
		protected static readonly string identity = Assembly.GetExecutingAssembly().GetName().Name + " " +
			Assembly.GetExecutingAssembly().GetName().Version;

		/// <summary>
		/// Identity of assembly (name version)
		/// </summary>
		public override string Identity
		{
			get
			{
				return identity;
			}
		}

  	/// <summary>
  	/// Get necessary configuration object type
  	/// </summary>
  	/// <returns></returns>
  	public override Type GetConfigType()
  	{
  		return typeof(DataProviderSettings);
  	}

  	protected override IWebProxy Proxy
  	{
  		get { return webService.Proxy; }
  	}

  	/// <summary>
    /// Configures data provider
    /// </summary>
    /// <param name="settings"></param>
    public override void Config(object settings)
    {
    	DataProviderSettings rsdnSettings = settings as DataProviderSettings;
    	if (rsdnSettings != null)
    	{
    		serverSchemeAndName = rsdnSettings.ServiceUri.GetLeftPart(UriPartial.Authority);
				serverName = rsdnSettings.ServiceUri.Host;
    		webService.Url = rsdnSettings.Service;
				// set proxy if necessary
				switch (rsdnSettings.ProxyType)
				{
					case ProxyType.Default : 
						break;
					case ProxyType.Explicit :
						webService.Proxy = rsdnSettings.Proxy;
						break;
                    case ProxyType.None:
                        webService.Proxy = null;
                        break;
                    default:
						break;
				}

				base.Config(settings);

				formatter.CanonicalRsdnHostName = serverName;
    	}
    }
  }
}
