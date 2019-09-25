using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;
using System.IO;
using LitJson;

public class WindowActive : MonoBehaviour
{

    [DllImport("User32.dll", EntryPoint = "FindWindow")]
    extern static IntPtr FindWindow(string lpClassName, string lpWindowName);

    [DllImport("User32.dll")]
    extern static bool SetForegroundWindow(IntPtr hWnd);

    [DllImport("User32.dll")]
    extern static bool ShowWindow(IntPtr hWnd, short State);

    public float Wait = 1;//延迟执行
    public float Rate = 2;//更新频率
                          // public string Title = "My_";//窗口标题
    public bool KeepForeground = true;//保持最前
                                      //  public bool KeepShowWindow = true;//保持显示
    IntPtr hWnd;
    [DllImport("user32.dll ")]
    public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
    static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
    static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);
    static readonly IntPtr HWND_TOP = new IntPtr(0);
    static readonly IntPtr HWND_BOTTOM = new IntPtr(1);
    const UInt32 SWP_NOSIZE = 0x0001;
    const UInt32 SWP_NOMOVE = 0x0002;
    const UInt32 SWP_NOZORDER = 0x0004;
    const UInt32 SWP_NOREDRAW = 0x0008;
    const UInt32 SWP_NOACTIVATE = 0x0010;
    const UInt32 SWP_FRAMECHANGED = 0x0020;
    const UInt32 SWP_SHOWWINDOW = 0x0040;
    const UInt32 SWP_HIDEWINDOW = 0x0080;
    const UInt32 SWP_NOCOPYBITS = 0x0100;
    const UInt32 SWP_NOOWNERZORDER = 0x0200;
    const UInt32 SWP_NOSENDCHANGING = 0x0400;
    const UInt32 TOPMOST_FLAGS = SWP_NOMOVE | SWP_NOSIZE;

    void Start()
    {
        //XmlDocument xml = new XmlDocument();
        //xml.Load(path);
        //isCursor = bool.Parse(xml.GetElementsByTagName("isCursor")[0].InnerText);
        hWnd = C.GetProcessWnd();
        InvokeRepeating("Active", Wait, Rate);
    }

    /// <summary>
    /// 激活窗口
    /// </summary>
    void Active()
    {
        if (KeepForeground)
        {
            try
            {
                ShowWindow(hWnd, 1);
                SetForegroundWindow(hWnd);
                SetWindowPos(hWnd, HWND_TOPMOST, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE | SWP_SHOWWINDOW);
            }
            catch { }
        }
        //  }
    }
}