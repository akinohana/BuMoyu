using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AntiMoyuService;
using Newtonsoft.Json;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Net.Http;

namespace AntiMoyuClient
{
	static class Program
	{
		public static int NonWorkingTimeBegin = 0;
		public static Form1 form1;
		public static int RestTime = 0;

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{


			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Form1());
		}
	}
}
