﻿//------------------------------------------------------------------------------
// <autogenerated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.4322.573
//
//     Changes to this file may cause incorrect behavior and will be lost if 
//     the code is regenerated.
// </autogenerated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 1.1.4322.573.
// 
namespace Rsdn.RsdnNntp.RsdnService {
    using System.Diagnostics;
    using System.Xml.Serialization;
    using System;
    using System.Web.Services.Protocols;
    using System.ComponentModel;
    using System.Web.Services;
    
    
    /// <remarks/>
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="ServiceSoap", Namespace="http://rsdn.ru/ws/")]
    public class Service : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        /// <remarks/>
        public Service() {
            string urlSetting = System.Configuration.ConfigurationSettings.AppSettings["RsdnDataProvider.RsdnService.Service"];
            if ((urlSetting != null)) {
                this.Url = string.Concat(urlSetting, "");
            }
            else {
                this.Url = "http://rsdn.ru/ws/service.asmx";
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://rsdn.ru/ws/PostMessage", RequestNamespace="http://rsdn.ru/ws/", ResponseNamespace="http://rsdn.ru/ws/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public post_result PostMessage(string login, string psw, int mid, string group, string subject, string message) {
            object[] results = this.Invoke("PostMessage", new object[] {
                        login,
                        psw,
                        mid,
                        group,
                        subject,
                        message});
            return ((post_result)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginPostMessage(string login, string psw, int mid, string group, string subject, string message, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("PostMessage", new object[] {
                        login,
                        psw,
                        mid,
                        group,
                        subject,
                        message}, callback, asyncState);
        }
        
        /// <remarks/>
        public post_result EndPostMessage(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((post_result)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://rsdn.ru/ws/GetUserInfo", RequestNamespace="http://rsdn.ru/ws/", ResponseNamespace="http://rsdn.ru/ws/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public UserInfo GetUserInfo(string login, string psw) {
            object[] results = this.Invoke("GetUserInfo", new object[] {
                        login,
                        psw});
            return ((UserInfo)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGetUserInfo(string login, string psw, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetUserInfo", new object[] {
                        login,
                        psw}, callback, asyncState);
        }
        
        /// <remarks/>
        public UserInfo EndGetUserInfo(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((UserInfo)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://rsdn.ru/ws/GetUserInfoByID", RequestNamespace="http://rsdn.ru/ws/", ResponseNamespace="http://rsdn.ru/ws/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public UserInfo GetUserInfoByID(string login, string psw, int id) {
            object[] results = this.Invoke("GetUserInfoByID", new object[] {
                        login,
                        psw,
                        id});
            return ((UserInfo)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGetUserInfoByID(string login, string psw, int id, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetUserInfoByID", new object[] {
                        login,
                        psw,
                        id}, callback, asyncState);
        }
        
        /// <remarks/>
        public UserInfo EndGetUserInfoByID(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((UserInfo)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://rsdn.ru/ws/GetGroupList", RequestNamespace="http://rsdn.ru/ws/", ResponseNamespace="http://rsdn.ru/ws/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public group_list GetGroupList(string login, string psw, System.DateTime dtc) {
            object[] results = this.Invoke("GetGroupList", new object[] {
                        login,
                        psw,
                        dtc});
            return ((group_list)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGetGroupList(string login, string psw, System.DateTime dtc, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetGroupList", new object[] {
                        login,
                        psw,
                        dtc}, callback, asyncState);
        }
        
        /// <remarks/>
        public group_list EndGetGroupList(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((group_list)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://rsdn.ru/ws/GroupInfo", RequestNamespace="http://rsdn.ru/ws/", ResponseNamespace="http://rsdn.ru/ws/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public group GroupInfo(string name, string login, string psw) {
            object[] results = this.Invoke("GroupInfo", new object[] {
                        name,
                        login,
                        psw});
            return ((group)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGroupInfo(string name, string login, string psw, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GroupInfo", new object[] {
                        name,
                        login,
                        psw}, callback, asyncState);
        }
        
        /// <remarks/>
        public group EndGroupInfo(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((group)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://rsdn.ru/ws/GetArticle", RequestNamespace="http://rsdn.ru/ws/", ResponseNamespace="http://rsdn.ru/ws/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public article GetArticle(string group, int number, string login, string psw) {
            object[] results = this.Invoke("GetArticle", new object[] {
                        group,
                        number,
                        login,
                        psw});
            return ((article)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGetArticle(string group, int number, string login, string psw, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetArticle", new object[] {
                        group,
                        number,
                        login,
                        psw}, callback, asyncState);
        }
        
        /// <remarks/>
        public article EndGetArticle(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((article)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://rsdn.ru/ws/GetFormattedArticle", RequestNamespace="http://rsdn.ru/ws/", ResponseNamespace="http://rsdn.ru/ws/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public article GetFormattedArticle(string group, int number, string login, string psw) {
            object[] results = this.Invoke("GetFormattedArticle", new object[] {
                        group,
                        number,
                        login,
                        psw});
            return ((article)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGetFormattedArticle(string group, int number, string login, string psw, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetFormattedArticle", new object[] {
                        group,
                        number,
                        login,
                        psw}, callback, asyncState);
        }
        
        /// <remarks/>
        public article EndGetFormattedArticle(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((article)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://rsdn.ru/ws/GetArticleByID", RequestNamespace="http://rsdn.ru/ws/", ResponseNamespace="http://rsdn.ru/ws/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public article GetArticleByID(int mid, string login, string psw) {
            object[] results = this.Invoke("GetArticleByID", new object[] {
                        mid,
                        login,
                        psw});
            return ((article)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGetArticleByID(int mid, string login, string psw, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetArticleByID", new object[] {
                        mid,
                        login,
                        psw}, callback, asyncState);
        }
        
        /// <remarks/>
        public article EndGetArticleByID(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((article)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://rsdn.ru/ws/GetFormattedArticleByID", RequestNamespace="http://rsdn.ru/ws/", ResponseNamespace="http://rsdn.ru/ws/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public article GetFormattedArticleByID(int mid, string login, string psw) {
            object[] results = this.Invoke("GetFormattedArticleByID", new object[] {
                        mid,
                        login,
                        psw});
            return ((article)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGetFormattedArticleByID(int mid, string login, string psw, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetFormattedArticleByID", new object[] {
                        mid,
                        login,
                        psw}, callback, asyncState);
        }
        
        /// <remarks/>
        public article EndGetFormattedArticleByID(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((article)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://rsdn.ru/ws/ArticleList", RequestNamespace="http://rsdn.ru/ws/", ResponseNamespace="http://rsdn.ru/ws/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public article_list ArticleList(string group, int first, int last, string login, string psw) {
            object[] results = this.Invoke("ArticleList", new object[] {
                        group,
                        first,
                        last,
                        login,
                        psw});
            return ((article_list)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginArticleList(string group, int first, int last, string login, string psw, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("ArticleList", new object[] {
                        group,
                        first,
                        last,
                        login,
                        psw}, callback, asyncState);
        }
        
        /// <remarks/>
        public article_list EndArticleList(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((article_list)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://rsdn.ru/ws/ArticleListFromDate", RequestNamespace="http://rsdn.ru/ws/", ResponseNamespace="http://rsdn.ru/ws/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public article_list ArticleListFromDate(string[] groups, System.DateTime startDate, string login, string psw) {
            object[] results = this.Invoke("ArticleListFromDate", new object[] {
                        groups,
                        startDate,
                        login,
                        psw});
            return ((article_list)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginArticleListFromDate(string[] groups, System.DateTime startDate, string login, string psw, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("ArticleListFromDate", new object[] {
                        groups,
                        startDate,
                        login,
                        psw}, callback, asyncState);
        }
        
        /// <remarks/>
        public article_list EndArticleListFromDate(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((article_list)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://rsdn.ru/ws/Authentication", RequestNamespace="http://rsdn.ru/ws/", ResponseNamespace="http://rsdn.ru/ws/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public auth_info Authentication(string login, string psw) {
            object[] results = this.Invoke("Authentication", new object[] {
                        login,
                        psw});
            return ((auth_info)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginAuthentication(string login, string psw, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("Authentication", new object[] {
                        login,
                        psw}, callback, asyncState);
        }
        
        /// <remarks/>
        public auth_info EndAuthentication(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((auth_info)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://rsdn.ru/ws/GetLink", RequestNamespace="http://rsdn.ru/ws/", ResponseNamespace="http://rsdn.ru/ws/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public link GetLink(string link, LinkType type, string login, string psw) {
            object[] results = this.Invoke("GetLink", new object[] {
                        link,
                        type,
                        login,
                        psw});
            return ((link)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGetLink(string link, LinkType type, string login, string psw, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetLink", new object[] {
                        link,
                        type,
                        login,
                        psw}, callback, asyncState);
        }
        
        /// <remarks/>
        public link EndGetLink(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((link)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://rsdn.ru/ws/PostFile", RequestNamespace="http://rsdn.ru/ws/", ResponseNamespace="http://rsdn.ru/ws/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string PostFile(string fileName, string contentType, [System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary")] System.Byte[] content, string login, string psw) {
            object[] results = this.Invoke("PostFile", new object[] {
                        fileName,
                        contentType,
                        content,
                        login,
                        psw});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginPostFile(string fileName, string contentType, System.Byte[] content, string login, string psw, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("PostFile", new object[] {
                        fileName,
                        contentType,
                        content,
                        login,
                        psw}, callback, asyncState);
        }
        
        /// <remarks/>
        public string EndPostFile(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://rsdn.ru/ws/")]
    public class post_result {
        
        /// <remarks/>
        public string error;
        
        /// <remarks/>
        public bool ok;
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://rsdn.ru/ws/")]
    public class link {
        
        /// <remarks/>
        public string error;
        
        /// <remarks/>
        public string url;
        
        /// <remarks/>
        public string name;
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://rsdn.ru/ws/")]
    public class auth_info {
        
        /// <remarks/>
        public string error;
        
        /// <remarks/>
        public bool ok;
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://rsdn.ru/ws/")]
    public class article_list {
        
        /// <remarks/>
        public string error;
        
        /// <remarks/>
        public string postfix;
        
        /// <remarks/>
        public System.DateTime date;
        
        /// <remarks/>
        public article[] articles;
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://rsdn.ru/ws/")]
    public class article {
        
        /// <remarks/>
        public string error;
        
        /// <remarks/>
        public string postfix;
        
        /// <remarks/>
        public string id;
        
        /// <remarks/>
        public string pid;
        
        /// <remarks/>
        public string gid;
        
        /// <remarks/>
        public int num;
        
        /// <remarks/>
        public string author;
        
        /// <remarks/>
        public string authorid;
        
        /// <remarks/>
        public System.DateTime date;
        
        /// <remarks/>
        public string subject;
        
        /// <remarks/>
        public string message;
        
        /// <remarks/>
        public string fmtmessage;
        
        /// <remarks/>
        public string userType;
        
        /// <remarks/>
        [System.ComponentModel.DefaultValueAttribute(0)]
        public int userColor = 0;
        
        /// <remarks/>
        public string homePage;
        
        /// <remarks/>
        public string group;
        
        /// <remarks/>
        public bool smile;
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://rsdn.ru/ws/")]
    public class group {
        
        /// <remarks/>
        public string error;
        
        /// <remarks/>
        public string name;
        
        /// <remarks/>
        public System.DateTime created;
        
        /// <remarks/>
        public int number;
        
        /// <remarks/>
        public int first;
        
        /// <remarks/>
        public int last;
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://rsdn.ru/ws/")]
    public class group_list {
        
        /// <remarks/>
        public System.DateTime date;
        
        /// <remarks/>
        public string error;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable=false)]
        public group[] groups;
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://rsdn.ru/ws/")]
    public class UserInfo {
        
        /// <remarks/>
        public UserRole UserRole;
        
        /// <remarks/>
        public int ID;
        
        /// <remarks/>
        public string Name;
        
        /// <remarks/>
        public string Password;
        
        /// <remarks/>
        public string Email;
        
        /// <remarks/>
        public bool Activated;
        
        /// <remarks/>
        public string Nick;
        
        /// <remarks/>
        public string PublicEmail;
        
        /// <remarks/>
        public string RealName;
        
        /// <remarks/>
        public string HomePage;
        
        /// <remarks/>
        public string Specialization;
        
        /// <remarks/>
        public bool SendMail;
        
        /// <remarks/>
        public MessageFormat MessageFormat;
        
        /// <remarks/>
        public ForwardType ForwardType;
        
        /// <remarks/>
        public bool StopMail;
        
        /// <remarks/>
        public string AdminMessage;
        
        /// <remarks/>
        public bool Flat;
        
        /// <remarks/>
        public bool DefaultFlat;
        
        /// <remarks/>
        public int PageCount;
        
        /// <remarks/>
        public string Picture;
        
        /// <remarks/>
        public string WhereFrom;
        
        /// <remarks/>
        public string Origin;
        
        /// <remarks/>
        public string IP;
        
        /// <remarks/>
        public int PersonRate;
        
        /// <remarks/>
        public int TotalRate;
        
        /// <remarks/>
        public bool IsCrawler;
        
        /// <remarks/>
        public System.DateTime BanDate;
        
        /// <remarks/>
        public string Title;
        
        /// <remarks/>
        public int TitleColor;
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://rsdn.ru/ws/")]
    public enum UserRole {
        
        /// <remarks/>
        Admin,
        
        /// <remarks/>
        Moderator,
        
        /// <remarks/>
        TeamMember,
        
        /// <remarks/>
        User,
        
        /// <remarks/>
        Expert,
        
        /// <remarks/>
        Anonym,
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://rsdn.ru/ws/")]
    [System.FlagsAttribute()]
    public enum MessageFormat {
        
        /// <remarks/>
        None = 1,
        
        /// <remarks/>
        Text = 2,
        
        /// <remarks/>
        Html = 4,
        
        /// <remarks/>
        TextHtml = 8,
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://rsdn.ru/ws/")]
    public enum ForwardType {
        
        /// <remarks/>
        None,
        
        /// <remarks/>
        Private,
        
        /// <remarks/>
        Public,
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://rsdn.ru/ws/")]
    public enum LinkType {
        
        /// <remarks/>
        MSDN,
    }
}
