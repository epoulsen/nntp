using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;


namespace Desktop.app
{
  using ru.rsdn;

  /// <summary>
  /// ���������� ��������� NNTP
  /// ����������� ��. RFCs 977, 1036, 822, 821, 793, 2980
  /// </summary>

  /// <summary>
  /// ��������������� ������ ��� ������ ��������� �� �������
  /// </summary>
  #region StateObject
  internal class StateObject 
  {
    public NNTPHandler workHandler = null;         // ���������� ������
    public const int BufferSize = 1024;            // ������ ��������� ������
    public byte[] buffer = new byte[BufferSize];   // �������� �����
    public StringBuilder sb = new StringBuilder(); // ���������� ������
    public NNTPManager manager = null;             // �������� ����������

    /// <summary>
    /// �����������
    /// </summary>
    /// <param name="manager"></param>
    public StateObject(NNTPManager manager)
    {
      this.manager = manager;
    }

    /// <summary>
    /// ��������������� ����� ������� ������
    /// </summary>
    public void ClearBuffer()
    {
      sb.Remove(0, sb.Length);
    }

    /// <summary>
    /// ��������������� ����� �������������� ���������� �������� � ������
    /// </summary>
    /// <param name="bytesRead"></param>
    public void AppendData(int bytesRead)
    {
      sb.Append(Encoding.GetEncoding(1251).GetString(buffer, 0, bytesRead));
    }
  }
  #endregion

  /// <summary>
  /// �������� NNTP ����������
  /// </summary>
  #region NNTPManager
  internal class NNTPManager : Socket
  {
    /// <summary>
    /// ������ �������� ������������
    /// </summary>
    public ArrayList handlers = new ArrayList();

    /// <summary>
    /// �����������
    /// </summary>
    public NNTPManager() : base(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
    {
    }

    /// <summary>
    /// �������� ������
    /// </summary>
    public void StartWork()
    {
      Bind(new IPEndPoint(IPAddress.Loopback, 119));
      Listen(100);
      BeginAccept(new AsyncCallback(AcceptCallback), this);
    }

    /// <summary>
    /// ����������� ��������� �������� ����������
    /// </summary>
    /// <param name="ar"></param>
    public void AcceptCallback(IAsyncResult ar)
    {
      // �������� �����, ������� ����� ������������ ���������� �������
      // � ������� ����������, ��������� � ���� �������
      //
      NNTPHandler handler = new NNTPHandler(EndAccept(ar));

      // ������� ��������������� ������
      //
      StateObject state = new StateObject(this);
      state.workHandler = handler;
      handlers.Add(handler);

      // ���������� �������� ����� ������ ����������
      //
      BeginAccept(new AsyncCallback(AcceptCallback), this);

      // ���������� ���������� ����� ������� ���������
      //
      handler.StartReading(state);
    }
  }
  #endregion

  /// <summary>
  /// ���������� NNTP ������ �������� ����������
  /// </summary>
  #region NNTPHandler
  internal class NNTPHandler
  {
    /// <summary>
    /// ������� ����� ��� ����� � ��������
    /// </summary>
    private Socket workSocket = null;

    /// <summary>
    /// ������ ��� Web-�������
    /// </summary>
    private Forum forum = new Forum();

    /// <summary>
    /// ������������� ������ ������������ ������ NNTP
    /// </summary>
    private Hashtable map = new Hashtable();

    #region ���������
    string strUser = string.Empty;
    string strPsw = string.Empty;

    string m_group = string.Empty;
    bool m_auth_ok = true;

    bool IsHTTPFormat = true;

    const string strNextPart0 = "----=_NextPart_000_0001_00000002.00000003";
    const string strNextPart1 = "----=_NextPart_001_0001_00000002.00000003";

    const string strHead =
      "<head>" +
      "<META http-equiv=\"Content-Type\" content=\"text/html; charset=windows-1251\">\r\n" +
      "<style>\r\n" +
      "body { " +
      "background-color: White; " +
      "font-family: 'courier','courier new'; " +
      "margin: 0; " +
      "}\r\n" +
      "a:link    { text-decoration: none; color: #505000; }\r\n" +
      "a:visited { text-decoration: none; color: #909090; }\r\n" +
      "a:hover   { text-decoration: underline; color: #B0B0B0; }\r\n" +
      "a.t:link    { text-decoration: none; color: #000000; }\r\n" +
      "a.t:visited { text-decoration: none; color: #000000; }\r\n" +
      "a.t:hover   { text-decoration: underline; color: #000000; }\r\n" +
      "a.m:link    { text-decoration: underline; color: #505000; }\r\n" +
      "a.m:visited { text-decoration: underline; color: #909090; }\r\n" +
      "a.m:hover   { text-decoration: underline; color: #B0B0B0; }\r\n" +
      "td.s " +
      "{ " +
      "color: #888888; " +
      "background-color:#E0F0E0; " +
      "font-family:verdana; " +
      "font-weight:bold; " +
      "padding: 0px 5px; " +
      "}\r\n" +
      "td.m { " +
      "font-family: verdana, tahoma, helvetica, arial; " +
      "background-color: #FFFFFF; " +
      "padding: 5 10 5 10; " +
      "}\r\n" +
      "td.i " +
      "{ " +
      "font-family: verdana, tahoma, helvetica, arial; " +
      "background-color: #FFFFF4; " +
      "padding: 0px 5px; " +
      "}\r\n" +
      "pre " +
      "{ " +
      "font-family: 'courier','courier new'; " +
      "font-size: 12px; " +
      "margin: 0; " +
      "padding: 10px; " +
      "}\r\n" +
      "tr.c, td.c " +
      "{ " +
      "font-family: 'courier','courier new'; " +
      "background-color: #EEEEEE; " +
      "margin: 0; " +
      "padding: 0 0; " +
      "}\r\n" +
      "div.m { " +
      "font-family: verdana, tahoma, helvetica, arial; " +
      "background-color: #FFFFFF; " +
      "padding: 5 10 5 10; " +
      "}\r\n" +
      "</style>\r\n" +
      "</head>\r\n";

    const string strBottom = "\r\n</body>\r\n</html>\r\n";
    #endregion

    #region ���� ��������

    /// <summary>
    /// ���� �������� - �����
    /// </summary>
    const string resp200  = "200 NNTPproxy news server ready (posting allowed)\r\n";
    const string resp205 = "205 goodbye\r\n";

    /// <summary>
    /// ���� �������� - ������ 
    /// </summary>
    const string error502 = "502 no permission\r\n";
    const string error503 = "503 program fault - command not performed\r\n";
    const string error412 = "412 No news group current selected\r\n";

    #endregion


    /// <summary>
    /// �����������
    /// </summary>
    /// <param name="socket"></param>
    public NNTPHandler(Socket socket)
    {
//      forum.Url = "http://localhost/RSDN/WS/Forum.asmx";
      workSocket = socket;

      // ��������� ������������� ������ ������������
      //
      map.Add("MODE", new CommandHandler(ModeHandler));
      map.Add("AUTHINFO", new CommandHandler(AuthInfoHandler));
      map.Add("XOVER", new CommandHandler(XOverHandler));
      map.Add("GROUP", new CommandHandler(GroupHandler));
      map.Add("LIST", new CommandHandler(ListHandler));
      map.Add("ARTICLE", new CommandHandler(ArticleHandler));
      map.Add("POST", new CommandHandler(PostHandler));
      map.Add("QUIT", new CommandHandler(QuitHandler));
    }

    #region ������ ������ �� ����������� ������ (����������� ������)

    /// <summary>
    /// �������� ����� ��������� �� �������
    /// </summary>
    /// <param name="state"></param>
    public void StartReading(StateObject state)
    {
      workSocket.Send(Encoding.GetEncoding(1251).GetBytes(resp200));
      workSocket.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
        new AsyncCallback(ReadCallback), state);
    }

    /// <summary>
    /// ������� ������ ������, ���������� �������� �������� ������
    /// </summary>
    /// <param name="ar"></param>
    public void ReadCallback(IAsyncResult ar)
    {
      String content = String.Empty;

      // �������� ��������������� ������
      //
      StateObject state = (StateObject)ar.AsyncState;

      // ������ ������ �� ����������� ������
      //
      int bytesRead = workSocket.EndReceive(ar);

      if (bytesRead > 0) 
      {
        // ��������, ��� ��� �� ��� ������,
        // ������� ��������� ��� ����������� �������������
        //
        state.AppendData(bytesRead);

        // ��������� �� ������� <CR><LF>, ���� ��� ��� - ������ ������
        //
        content = state.sb.ToString();
        if (content.EndsWith("\r\n"))
        {
          // ���������� ������
          //
          string strResponce = DoRequest(content);

          // ���� QUIT - ��������� �����
          //
          if (strResponce == "QUIT")
          {
            state.manager.handlers.Remove(this);

            state.ClearBuffer();

            return;
          }
          else
          {
            // �������� ������� �����
            //
            workSocket.Send(Encoding.GetEncoding(1251).GetBytes(strResponce));
          }

          state.ClearBuffer();
        }

        // ������� ��������� ������ ������ (��� ������ �������)
        //
        workSocket.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
          new AsyncCallback(ReadCallback), state);
      }
    }

    #endregion

    #region ��������� ������ ��������� NNTP


    /// <summary>
    /// ��������������� ������� ����������� ���� ������
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    private string Error(string s)
    {
      if (s.Length > 0) 
      {
        if (s[0] == '2')
          return error502;
        return "503 " + s + "\r\n";
      }
  
      return "\r\n";
    }

    private string DoBar(string subj, string mid, string gid)
    {
      subj.Replace("&", "&amp;");
      subj.Replace("<", "&lt;");
      subj.Replace(">", "&gt;");
      subj.Replace("\"","&quot;");

      subj = "<a target='_blank' title='������� � �����' href='http://www.rsdn.ru/forum/?mid="+mid+"'>"+subj+"</b></a>";

      string bar =
        "<table width='100%' border='0' cellspacing='0'>" +
        "<tr><td class='s' width='99%'><font size=2>" + subj + "</font></td>" +
          "<td class='s' nowrap><font size=2>&nbsp;" +
            "<a target='_blank' href='http://www.rsdn.ru/forum/newmsg.aspx?gid=" + gid + "' title='�������� ����� ���������'><img align='absmiddle' src='images/new.gif' border=0 width='18px' height='14px'></img></a>&nbsp;" +
              "<a target='_blank' href='http://www.rsdn.ru/forum/newmsg.aspx?mid=" + mid + "' title='�������� �� ���������'><img align='absmiddle' src='images/replay.gif' border=0 width='18px' height='14px'></img></a>&nbsp;" +
                "&nbsp;&nbsp;<font size=1>������� </font>" +
                  "<a href='http://www.rsdn.ru/forum/rate.aspx?mid=" + mid + "&rate=0' title='� ��� �� �����'><img align='absmiddle' src='images/n0.gif' border=0 width='18px' height='14px'></img></a>" +
                    "<a href='http://www.rsdn.ru/forum/rate.aspx?mid=" + mid + "&rate=-1' title='������� ������'><img align='absmiddle' src='images/nx.gif' border=0 width='18px' height='14px'></img></a>" +
                      "<a href='http://www.rsdn.ru/forum/rate.aspx?mid=" + mid + "&rate=1' title='���������'><img align='absmiddle' src='images/n1.gif' border=0 width='18px' height='14px'></img></a>" +
                        "<a href='http://www.rsdn.ru/forum/rate.aspx?mid=" + mid + "&rate=2' title='���������'><img align='absmiddle' src='images/n2.gif' border=0 width='18px' height='14px'></img></a>" +
                          "<a href='http://www.rsdn.ru/forum/rate.aspx?mid=" + mid + "&rate=3' title='�����'><img align='absmiddle' src='images/n3.gif' border=0 width='18px' height='14px'></img></a>" +
                            "</font></td>" +
                              "</tr>" +
                                "</table>\r\n";
	
      return bar;
    }

    private string DoInfo(string usernick, string uid, DateTime dt)
    {
      usernick.Replace("&", "&amp;");
      usernick.Replace("<", "&lt;");
      usernick.Replace(">", "&gt;");
      usernick.Replace("\"","&quot;");

      if (usernick == "")
        usernick = "<font color='#C00000'>������</font>";
      else 
        usernick = "<a target='_blank' href='http://www.rsdn.ru/users/profile.aspx?uid="+uid+"'><b>"+usernick+"</b></a>";

      string info =
        "<table width='100%' border='0' cellspacing='0'>" +
        "<tr>" +
          "<td class='i' nowrap><font size=2><b>��:&nbsp;</b></font></td>" +
            "<td class='i' nowrap width='100%'><font size=2>" + usernick + "</font></td>" +
              "<td class='i' rowspan=2><font size=2>" + "</font></td>" +
                "</tr>" +
                  "<tr>" +
                    "<td class='i' nowrap><font size=2><b>����:&nbsp;</b></font></td>" +
                      "<td class='i' width='100%'><font size=2>" + dt +
                        "</font></td></tr>" +
                        "</table>";

      return info;
    }


    /// <summary>
    /// ���������� �������� �������
    /// </summary>
    /// <param name="strCommand"></param>
    /// <returns></returns>
    public string DoRequest(string strCommand)
    {
      // �������� ������� �� ������� ����������
      //
      string str = string.Empty;
      string [] strings = strCommand.TrimEnd(new char[]{'\r', '\n'}).Split(new char[]{' ','\t'});
      str = strings[0];

      // ���� ���������� � ������������� �������
      //
      string ret = error503;
      string strUpper = str.ToUpper();
      object obj = map[strUpper];
      if (obj != null)
      {
        // ����� ���������� - ��������� �������
        //
        ret = ((CommandHandler)obj)(strings);
        if (ret.Length == 0)
          ret = error503;
      }

      return ret;
    }

    /// <summary>
    /// ���������� ������ NNTP
    /// ������ ������� ������� - �������
    /// ��������� �������� - ��������� �������
    /// </summary>
    private delegate string CommandHandler(string [] strings);

    /// <summary>
    /// ���������� ������� MODE
    /// </summary>
    /// <param name="strings"></param>
    /// <returns></returns>
    private string ModeHandler(string [] strings)
    {
      // ��� ������ ������������ ������� ��������� � ������ ��������
      //
      return resp200;
    }

    /// <summary>
    /// ���������� ������� AUTHINFO - �����������
    /// </summary>
    /// <param name="strings"></param>
    /// <returns></returns>
    private string AuthInfoHandler(string [] strings)
    {
      if (strings[1].ToUpper() == "USER")
      {
        strUser = strings[2];
        strPsw  = "";
        return "381 More authentication information required\r\n";
      }
      else 
      {
        if (strings[1].ToUpper() == "PASS") 
        {
          strPsw = strings[2];

          auth_info ai = forum.Authentication(strUser, strPsw);
          if (ai.error != null && ai.error.Length > 0)
            return Error(ai.error);

          m_auth_ok = ai.ok;
          return m_auth_ok ? "281 Authentication accepted\r\n" :
                             "482 Authentication rejected\r\n";
        }
      }

      return "\r\n";
    }

    /// <summary>
    /// ��������������� ������� ����������� ������ � Base64
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    private string Encode(string str, bool addPrefix)
    {
      if (str.Length == 0)
        return str;

      System.Text.Encoder encoder = System.Text.Encoding.GetEncoding(1251).GetEncoder();

      // ��������� ������ �� Unicode � Windows-1251
      //
      char [] chars = str.ToCharArray();
      byte [] bytes = new byte[encoder.GetByteCount(chars, 0, str.Length, true)];
      encoder.GetBytes(chars, 0, str.Length, bytes, 0, true);

      // ������������ � Base64 � ��������� ��������� ��� ������� �������� 1251
      //
      if (addPrefix)
        return "=?windows-1251?B?" + System.Convert.ToBase64String(bytes) + "?=";
      else
        return System.Convert.ToBase64String(bytes);
    }

    /// <summary>
    /// ���������� ������� XOVER
    /// </summary>
    /// <param name="strings"></param>
    /// <returns></returns>
    private string XOverHandler(string [] strings)
    {
      // ����������� �� ���� ��� ��� ������ ��������
      //
      if (!m_auth_ok)
        return error502;

      // �� ������� ������
      //
      if (m_group == "")
        return error412;

      // �������� ������ ������ � ������
      //
      string [] strNumbers = strings[1].Split('-');
      article_list al = forum.ArticleList(m_group,
                                          Convert.ToInt32(strNumbers[0]),
                                          Convert.ToInt32(strNumbers[1]),
                                          strUser, strPsw);
      if (al.error != null && al.error.Length > 0)
        return Error(al.error);

      // ��������� ����� ��� �������
      //
      string ret = "224 Overview information follows\r\n";
      foreach(article art in al.articles)
      {
        if (art.pid.Length > 0)
        {
          art.pid = "<" + art.pid + al.postfix + ">";
        }

    		ret +=  art.num + "\t" +
                Encode(art.subject, true) + "\t" +
				        Encode(art.author, true) + "\t" +
                art.date + "\t" +
				        "<" + art.id + al.postfix + ">\t" +
				        art.pid + "\t" +
                "1000" + "\t" +
                "10" + "\t" +
                "" + "\r\n";
      }

      return ret + ".\r\n";
    }

    /// <summary>
    /// ���������� ������� GROUP
    /// </summary>
    /// <param name="strings"></param>
    /// <returns></returns>
    private string GroupHandler(string [] strings)
    {
      if (!m_auth_ok)
        return  error502;

      m_group = strings[1];
      group gr = forum.GroupInfo(m_group, strUser, strPsw);
      if (gr.error != null && gr.error.Length > 0)
        return Error(gr.error);

      return "211 " + (gr.last - gr.first) + ' ' + 
             gr.first + ' ' + gr.last + ' ' + gr.name + "\r\n";
    }

    /// <summary>
    /// ���������� ������� LIST
    /// </summary>
    /// <param name="strings"></param>
    /// <returns></returns>
    private string ListHandler(string [] strings)
    {
      group_list gl = forum.GroupList(strUser, strPsw);
      if (gl.error != null && gl.error.Length > 0)
        return Error(gl.error);

    	string ret = "215 list of newsgroups follows\r\n";
      foreach(group gr in gl.groups)
      {
        ret += gr.name + ' ' + gr.last + ' ' + gr.first + " y\r\n";
      }

      return ret + ".\r\n";
    }

    /// <summary>
    /// ���������� ������� ARTICLE
    /// </summary>
    /// <param name="strings"></param>
    /// <returns></returns>
    private string ArticleHandler(string [] strings)
    {
      // �� ������ �����������
      //
    	if (!m_auth_ok)
		     return  error502;

      // �� ������ ������� ������
      if (m_group == "")
        return error412;

      string ret;
      if (IsHTTPFormat) {
        article ar = forum.GetFormattedArticle(m_group, Convert.ToInt32(strings[1]), strUser, strPsw);
        if (ar.error != null && ar.error.Length > 0)
	         return Error(ar.error);

        string bar = DoBar(ar.subject, ar.id, ar.gid);

        string html = "<html>" + strHead + "<body>\r\n" +
                   bar +
     	             DoInfo(ar.author, ar.authorid, ar.date) +
		               "<div class=m><font size=2>\r\n" +
                   ar.message +
		               "</font></div>" + bar +
            	     strBottom;
/*
    		link ln;
		    int beg = 0;
		    while ((beg = html.Find("[msdn]", beg)) != -1) {
		      int end = html.Find("[/msdn]", beg);
		      if (end == -1)
            break;

	  	    CString link = html.Mid(beg + 6, end - beg - 6);
		  	  ln = Forum::Forum(m_data.m_strURL).GetLink(link, "MSDN", m_user, m_psw);
			    if (ln.error.GetLength())
			      break;
			    html.Replace(
			      "[msdn]" + link + "[/msdn]",
			      "<a target=_blank class=m href='" + ln.url + "'>" + ln.name + "</a>"
			    );

          beg = end + 7;
		    }
*/

        ret =
	         "220 " + strings[1].Trim() + " <" + ar.id + ar.postfix + ">\r\n" +
	         "From: " + Encode(ar.author, true) + "\r\n" +
	         "Newsgroups: " + m_group + "\r\n" +
	         "Subject: " + Encode(ar.subject, true) + "\r\n" +
	         "Date: " + ar.date + "\r\n" +
	         "Message-ID: <" + ar.id + ar.postfix + ">\r\n" +
	         "Content-Type: multipart/related;\r\n" +
	         "\tboundary=\"" + strNextPart0 + "\";\r\n" +
	         "\ttype=\"multipart/alternative\"\r\n" +
	         "\r\n" +
	         "--" + strNextPart0 + "\r\n" +
	         "Content-Type: multipart/alternative;\r\n" +
	         "\tboundary=\"----=_NextPart_001_0001_00000002.00000003\"\r\n" +
	         "\r\n" +
	         "--" + strNextPart1 + "\r\n" +
	         "Content-Type: text/plain;\r\n" +
	         "\tcharset=\"windows-1251\"\r\n" +
	         "Content-Transfer-Encoding: quoted-printable\r\n" +
	         "\r\n";

		       html = html.Replace("src='images/", "src='http://www.rsdn.ru/forum/images/");
        	 html = Encode(html, false);

		       ret += 
			       "--" + strNextPart1 + "\r\n" +
			       "Content-Type: text/html;\r\n" +
			       "\tcharset=\"windows-1251\"\r\n" +
			       "Content-Transfer-Encoding: base64\r\n" +
			       "\r\n" + html + "\r\n\r\n";
      }
      else
      {
        article ar = forum.GetArticle(m_group, Convert.ToInt32(strings[1]), strUser, strPsw);
        if (ar.error != null && ar.error.Length > 0)
	        return Error(ar.error);

        ret =
	        "220 " + strings[1].Trim() + " <" + ar.id + ar.postfix + ">\r\n" +
	        "From: " + Encode(ar.author, true) + "\r\n" +
	        "Newsgroups: " + m_group + "\r\n" +
	        "Subject: " + Encode(ar.subject, true) + "\r\n" +
	        "Date: " + ar.date + "\r\n" +
	        "Message-ID: <" + ar.id + ar.postfix + ">\r\n" +
	        "Content-Type: multipart/related;\r\n" +
	        "\tboundary=\"" + strNextPart0 + "\";\r\n" +
	        "\ttype=\"multipart/alternative\"\r\n" +
	        "\r\n" +
	        "--" + strNextPart0 + "\r\n" +
	        "Content-Type: multipart/alternative;\r\n" +
	        "\tboundary=\"----=_NextPart_001_0001_00000002.00000003\"\r\n" +
	        "\r\n" +
	        "--" + strNextPart1 + "\r\n" +
	        "Content-Type: text/plain;\r\n" +
	        "\tcharset=\"windows-1251\"\r\n" +
	        "Content-Transfer-Encoding: quoted-printable\r\n" +
	        "\r\n" +
	        "" + ar.message + "\r\n\r\n";
      }

	    return ret +
		    "\r\n" +
		    "--" + strNextPart1 + "--\r\n" +
		    "--" + strNextPart0 + "--\r\n" +
		    ".\r\n";
    }

    /// <summary>
    /// ���������� ������� POST
    /// </summary>
    /// <param name="strings"></param>
    /// <returns></returns>
    private string PostHandler(string [] strings)
    {
      // �� ������ �����������
      //
      if (!m_auth_ok)
        return  error502;

      workSocket.Send(Encoding.GetEncoding(1251).GetBytes("340 send article to be posted. End with <CR-LF>.<CR-LF>\r\n"));

      string message = string.Empty;
      const int bufLen = 1024;
      byte [] buf = new byte[bufLen];
      int iRecv;
      do
      {
        iRecv = workSocket.Receive(buf, 0, bufLen, SocketFlags.None);
        if (iRecv == -1)
          return "441 article send failed\r\n";

        message += System.Text.Encoding.GetEncoding(1251).GetString(buf);
      } while (iRecv == bufLen && !message.EndsWith("\r\n.\r\n"));

      // ���������� � ������
      //
      post_result post = forum.PostMIMEMessage(strUser, strPsw, message);
      if (post.error != null && post.error.Length > 0)
        return Error(post.error);

      return "240 article sent ok\r\n";
    }

    /// <summary>
    /// ���������� ������� QUIT
    /// </summary>
    /// <param name="strings"></param>
    /// <returns></returns>
    private string QuitHandler(string [] strings)
    {
      workSocket.Send(Encoding.GetEncoding(1251).GetBytes(resp205));
      workSocket.Shutdown(SocketShutdown.Both);
      workSocket.Close();
      workSocket = null;

      // ��� ��������� ������ �������� - <CR><LF> ����� �� �����
      return "QUIT";
    }

    #endregion
  }
  #endregion
}
