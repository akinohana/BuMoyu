using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AntiMoyuService
{
	public class WinAPI
	{

		[StructLayout(LayoutKind.Sequential)]
		public struct LASTINPUTINFO
		{
			public static readonly int SizeOf = Marshal.SizeOf(typeof(LASTINPUTINFO));

			[MarshalAs(UnmanagedType.U4)]
			public UInt32 cbSize;
			[MarshalAs(UnmanagedType.U4)]
			public UInt32 dwTime;
		}

		[DllImport("user32.dll")]
		public static extern IntPtr GetForegroundWindow();


		[DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern int InternalGetWindowText(IntPtr hWnd, StringBuilder lpString,
			int nMaxCount);

		[DllImport("user32.dll")]
		public static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

	}
}
