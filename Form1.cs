using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AntiMoyuService;
using Microsoft.CSharp.RuntimeBinder;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace AntiMoyuClient
{
	public partial class Form1 : Form
	{

		public static object lock_localconfig = new object();
		public static object lock_log = new object();


		public static LocalConfig localConfig = null;
		public static string _imagePath;
		public static string _logPath;
		public static string _configPath;

		class MoyuConfig
		{
			public List<string> NonMoyuWindowTitles = new List<string>();
			public int MaxNoActionTime = 120;

			public void AddNonMoyuWindowTitle(string windowName)
			{
				NonMoyuWindowTitles.Add(windowName);
			}

			public void SetMaxNoActionTime(int maxTime)
			{
				MaxNoActionTime = maxTime;
			}

			//public void Save()
			//{
			//	string appConfig = JsonConvert.SerializeObject(this);
			//	using (System.IO.StreamWriter sw = new System.IO.StreamWriter(_configPath, false))
			//	{
			//		sw.WriteLine(appConfig);
			//	}
			//}

			//public static MoyuConfig Load()
			//{
			//	if (System.IO.File.Exists(_configPath))
			//	{
			//		string appConfig = System.IO.File.ReadAllText(_configPath);
			//		return JsonConvert.DeserializeObject<MoyuConfig>(appConfig);
			//	}

			//	//TODO:在这里配置你的反摸鱼设置
			//	var moyuConfig = new MoyuConfig();
			//	moyuConfig.NonMoyuWindowTitles.Add("Visual Studio");
			//	moyuConfig.NonMoyuWindowTitles.Add("Google");
			//	moyuConfig.NonMoyuWindowTitles.Add("StackOverFlow");
			//	moyuConfig.NonMoyuWindowTitles.Add("Github");
			//	moyuConfig.NonMoyuWindowTitles.Add("EditPlus");
			//	moyuConfig.NonMoyuWindowTitles.Add("XShell");
			//	moyuConfig.NonMoyuWindowTitles.Add("XFtp");
			//	moyuConfig.NonMoyuWindowTitles.Add("Navicat");
			//	moyuConfig.MaxNoActionTime = 120;
			//	moyuConfig.Save();
			//	return moyuConfig;
			//}

		}

		class MoyuMgr
		{
			private static readonly HttpClient client = new HttpClient();
			public MoyuConfig moyuConfig;

			public MoyuMgr()
			{
			}

			public bool IsUsingRightApplication()
			{
				var hWnd = WinAPI.GetForegroundWindow();
				StringBuilder windowTitleBuilder = new StringBuilder(255);
				WinAPI.InternalGetWindowText(hWnd, windowTitleBuilder, windowTitleBuilder.Capacity);
				var currentWindowTitle = windowTitleBuilder.ToString();
				Log("Current Window:" + currentWindowTitle);

				return moyuConfig.NonMoyuWindowTitles.Any((nonMoyuTitle) => currentWindowTitle.Contains(nonMoyuTitle));
			}

			public bool IsWorkingOnPC()
			{
				WinAPI.LASTINPUTINFO lastInputInfo = new WinAPI.LASTINPUTINFO();
				lastInputInfo.cbSize = (uint)Marshal.SizeOf(lastInputInfo);
				WinAPI.GetLastInputInfo(ref lastInputInfo);

				var idieTime = TimeSpan.FromMilliseconds(Environment.TickCount - lastInputInfo.dwTime);

				if (idieTime.TotalSeconds > moyuConfig.MaxNoActionTime)
					return false;

				return true;
			}

			public void AntiMoyuAction()
			{
				Console.Beep(1000, 500);

				//MailMessage mail = new MailMessage("you@yourcompany.com", "user@hotmail.com");
				//SmtpClient client = new SmtpClient();
				//client.Port = 25;
				//client.DeliveryMethod = SmtpDeliveryMethod.Network;
				//client.UseDefaultCredentials = false;
				//client.Host = "smtp.gmail.com";
				//mail.Subject = "this is a test email.";
				//mail.Body = "this is my test email body";
				//client.Send(mail);
			}


			public async Task UpdateConfig()
			{
				var configString = await GetConfigFromRemoteByToken();

				int x = 0;
			}


			public class RemoteJSONConfig
			{
				public List<string> legalWindowTitles { get; set; }
				public int MaxIdleTime { get; set; }
			}

			public async Task<string> GetConfigFromRemoteByToken()
			{

				LocalConfig _localConfig = null;
				if (localConfig != null)
				{
					lock (lock_localconfig)//防止多线程冲突
					{
						_localConfig = Util.DeepCopy<LocalConfig>(localConfig);

					}
				}

				if (_localConfig != null)
				{

					var username = _localConfig.username;
					var token = Util.CreateMD5(_localConfig.username + _localConfig.password);
					token = token.ToLower();

					var postData = new Dictionary<string, string>
					{
						{ "username", username },
						{ "token", token }
					};

					var content = new FormUrlEncodedContent(postData);

					var response = await client.PostAsync("https://bumoyu.com/loadConfig.php", content);

					var responseString = await response.Content.ReadAsStringAsync();
					if (responseString.Length > 0)
					{
						if (responseString == "用户名或密码错误")
						{
							Program.form1.UpdateStatus(responseString);
							Log(responseString);
						}
						else
						{
							


							try
							{
								var remoteConfig = JsonConvert.DeserializeObject<RemoteJSONConfig>(responseString);

								MoyuConfig moyuConfigNew = new MoyuConfig();
								moyuConfigNew.MaxNoActionTime = remoteConfig.MaxIdleTime;
								foreach (var remoteConfigLegalWindowTitle in remoteConfig.legalWindowTitles)
								{
									moyuConfigNew.NonMoyuWindowTitles.Add(remoteConfigLegalWindowTitle);
								}
								
								moyuConfig = moyuConfigNew;
							}
							catch (RuntimeBinderException)
							{
								Program.form1.UpdateStatus("错误的JSON格式");
								Log("错误的JSON格式");
							}


							//Program.form1.UpdateStatus(responseString);
						}
					}
					return responseString;
				}
				else
				{
					Program.form1.UpdateStatus("无返回数据");
					Log("无返回数据");
				}

				return "";
			}
		}

		public static void Worker()
		{

			Form1 form1;

			MoyuMgr moyuMgr = new MoyuMgr();
			try
			{

				for (; ; )
				{


					if (Program.RestTime > Environment.TickCount)
					{
						var restText = "正在休息，还剩" + ((Program.RestTime - Environment.TickCount) / 1000 / 60).ToString() + "分钟";
						Program.form1.UpdateStatus(restText);
						Log(restText);
						Thread.Sleep(2000);
						continue;
					}

					moyuMgr.UpdateConfig();


					


					Log("Service Alive.");



					if (moyuMgr.moyuConfig == null  || moyuMgr.moyuConfig?.NonMoyuWindowTitles.Count == 0)
					{

						var restText = "软件设置不正确或尚未加载，监控没有生效（可在网站上设置）";
						Program.form1.UpdateStatus(restText);
						Log(restText);

						Thread.Sleep(2000);
						continue;
					}

					if (!moyuMgr.IsUsingRightApplication() || !moyuMgr.IsWorkingOnPC())
					{
						if (Program.NonWorkingTimeBegin == 0)
							Program.NonWorkingTimeBegin = Environment.TickCount;

						int NotWorkingInSecond = (Environment.TickCount - Program.NonWorkingTimeBegin) / 1000;
						var restText = "检测到没有在工作，已经连续" + NotWorkingInSecond + "秒";
						Program.form1.UpdateStatus(restText);
						Log(restText);
						if (NotWorkingInSecond > 120)
						{
							moyuMgr.AntiMoyuAction();
						}

					}
					else
					{
						Program.NonWorkingTimeBegin = 0;
						var restText = "正在工作，请保持专注";
						Program.form1.UpdateStatus(restText);
						Log(restText);
					}

					Thread.Sleep(5000);
				}

			}
			catch (Exception ex)
			{
				Log("Error :" + ex.Message + ex.StackTrace);

				throw;
			}
		}
		static public void Log(string Message)
		{

			lock (lock_log)
			{
				using (System.IO.StreamWriter sw = new System.IO.StreamWriter(_logPath, true))
				{
					sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + Message);
				}
			}
		}

		void Login()
		{

			lock (lock_localconfig) //防止多线程冲突
			{
				localConfig = new LocalConfig();

				localConfig.username = username.Text;
				localConfig.password = password.Text;
				localConfig.autoStart = checkbox_autostart.Checked;
				localConfig.Save();
			}



		}
		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			_imagePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			_logPath = _imagePath + "\\log.txt";
			_configPath = _imagePath + "\\setting.txt";

			Log("Start.");


			Program.form1 = this;
			var localConfig = LocalConfig.Load();
			if (localConfig != null)
			{
				username.Text = localConfig.username;
				password.Text = localConfig.password;

				if (localConfig.autoStart)
					checkbox_autostart.Checked = true;
				else
				{
					checkbox_autostart.Checked = false;
				}

				Login();
			}



			

			Thread thread = new Thread(new ThreadStart(Worker));
			thread.Start();
		}


		public void UpdateStatus(string status)
		{
			MethodInvoker inv = delegate { this.label_status.Text = status; };
		
			this.Invoke(inv);
		}


		[Serializable]
		public class LocalConfig
		{
			public string username;
			public string password;
			public bool autoStart;

			public void Save()
			{
				string appConfig = JsonConvert.SerializeObject(this);
				using (System.IO.StreamWriter sw = new System.IO.StreamWriter(_configPath, false))
				{
					sw.WriteLine(appConfig);
				}
			}

			public static LocalConfig Load()
			{
				if (System.IO.File.Exists(_configPath))
				{
					string appConfig = System.IO.File.ReadAllText(_configPath);
					var localConfig = JsonConvert.DeserializeObject<LocalConfig>(appConfig);

					return localConfig;
				}

				
				return null;
			}

		}

		private void button1_Click(object sender, EventArgs e)
		{
			Login();


		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			Environment.Exit(0);
		}

		private void Form1_Resize(object sender, EventArgs e)
		{
			if (this.WindowState == FormWindowState.Minimized)
			{
				Hide();
			}
		}

		private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
		{
			Show();
			this.WindowState = FormWindowState.Normal;
		}

		private void checkBox1_CheckedChanged(object sender, EventArgs e)
		{
			
			RegistryKey rk = Registry.CurrentUser.OpenSubKey
				("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);


			if (checkbox_autostart.Checked)
			{
				rk.SetValue("不摸鱼", Application.ExecutablePath);
				if (localConfig != null)
				{
					localConfig.autoStart = true;
					localConfig?.Save();
				}

			}
			else
			{
				rk.DeleteValue("不摸鱼", false);
				if (localConfig != null)
				{
					localConfig.autoStart = false;
					localConfig.Save();
				}
			}
				
		}

		private void button_rest_Click(object sender, EventArgs e)
		{
			var restTime = numericUpDownRest.Value;
			Program.RestTime = Environment.TickCount + ((int)restTime* 60  * 1000);


		}
	}
}
