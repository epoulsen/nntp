// $Id$

using System;
using System.Diagnostics;
using System.Configuration;
using Rsdn.Nntp;

namespace ForumTest
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	class Class1
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			// Show trace on display
			Trace.Listeners.Add(new TextWriterTraceListener(System.Console.Out));

			try
			{
        NntpSettings serverSettings =
					NntpSettings.Deseriazlize(ConfigurationSettings.AppSettings["settings.ConfigFile"]);

				Manager nntpManager = new Manager(serverSettings);
				nntpManager.Start();

				System.Console.ReadLine();

				nntpManager.Stop();
			}
			catch (Exception e)
			{
				Trace.Fail(e.ToString());
			}
		}
	}
}